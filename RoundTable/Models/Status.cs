using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace RoundTable.Models
{
    public class Status
    {
        public int Id
        {
            get; set;
        }
        [DisplayName("Status")]
        public string Name
        {
            get; set;
        }
    }
}
