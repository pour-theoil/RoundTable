using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using RoundTable.Auth.Models;
using RoundTable.Repositories;
using RoundTable.Models;
using Microsoft.Extensions.Configuration;
using RoundTable.Auth;

namespace RoundTable.Auth
{
    public class AccountController : Controller
    {
        private readonly IFirebaseAuthService _firebaseAuthService;
        private readonly IReporterRepository _reporterRepository;

        public AccountController(IFirebaseAuthService firebaseAuthService, IReporterRepository reporterRepository)
        {
            _reporterRepository = reporterRepository;
            _firebaseAuthService = firebaseAuthService;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(Credentials credentials)
        {
            if (!ModelState.IsValid)
            {
                return View(credentials);
            }

            var fbUser = await _firebaseAuthService.Login(credentials);
            if (fbUser == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid email or password.");
                return View(credentials);
            }

            var reporter = _reporterRepository.GetByFirebaseUserId(fbUser.FirebaseUserId);
            if (reporter == null)
            {
                ModelState.AddModelError(string.Empty, "Unable to Login.");
                return View(credentials);
            }

            await LoginToApp(reporter);

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(Registration registration)
        {
            if (!ModelState.IsValid)
            {
                return View(registration);
            }

            var fbUser = await _firebaseAuthService.Register(registration);

            if (fbUser == null)
            {
                ModelState.AddModelError(string.Empty, "Unable to register, do you already have an account?");
                return View(registration);
            }

            var newReporter = new Reporter
            {
                Email = fbUser.Email,
                FirebaseId = fbUser.FirebaseUserId,
                FirstName = registration.FirstName,
                LastName = registration.LastName,
                Organization = registration.Organization,
                Phone = registration.Phone,
            };
            _reporterRepository.Add(newReporter);

            await LoginToApp(newReporter);

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        private async Task LoginToApp(Reporter reporter)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, reporter.Id.ToString()),
                new Claim(ClaimTypes.Email, reporter.Email),
            };

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity));
        }
    }
}
