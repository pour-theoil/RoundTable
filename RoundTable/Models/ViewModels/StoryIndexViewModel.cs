using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoundTable.Models.ViewModels
{
    public class StoryIndexViewModel
    {
        public List<Story> Stories { get; set; }
        public List<Status> Statuses { get; set; }
    }
}
