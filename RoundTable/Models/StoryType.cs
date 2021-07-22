using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace RoundTable.Models
{
    public class StoryType
    {
        public int Id { get; set; }
        [DisplayName("Story Type")]
        public string Name { get; set; }
    }
}
