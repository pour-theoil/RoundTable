using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RoundTable.Models
{
    public class Source
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

        public string JobTitle { get; set; }

        public int ReporterId { get; set; }
        [DisplayName("Name")]
        public string Displayname => FirstName + " " + LastName;
        public List<Category> Categories { get; set; }
        public string ImageLocation { get; set; }
    }
}
