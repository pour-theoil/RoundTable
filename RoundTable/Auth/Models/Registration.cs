using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RoundTable.Auth.Models
{
    public class Registration
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
 
        [Required]
        [Compare(nameof(Password))]
        [DisplayName("Confirm Password")]
        public string ConfirmPassword { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string Organization { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
