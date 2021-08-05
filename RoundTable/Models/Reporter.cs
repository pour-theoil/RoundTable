using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RoundTable.Models
{
    public class Reporter
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [MaxLength(30)]
        public string Organization { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        public string Phone { get; set; }
        [Required]
        public string FirebaseId { get; set; }
        [DisplayName("Name")]
        public string Displayname => FirstName + " " + LastName;
        public string ImageLocation { get; set; }
    }
}
