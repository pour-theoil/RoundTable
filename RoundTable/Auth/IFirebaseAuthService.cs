using System.Threading.Tasks;
using RoundTable.Auth.Models;

namespace RoundTable.Auth
{
    public interface IFirebaseAuthService
    {
        Task<FirebaseUser> Login(Credentials credentials);
        Task<FirebaseUser> Register(Registration registration);
    }
}