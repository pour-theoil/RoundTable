using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace RoundTable.Models
{
    public class NationalOutlet
    {
        public int Id
        {
            get; set;
        }
        [DisplayName("National")]
        public string Name
        {
            get; set;
        }
    }
}
