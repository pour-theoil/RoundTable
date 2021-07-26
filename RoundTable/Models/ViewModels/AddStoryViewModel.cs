using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoundTable.Models.ViewModels
{
    public class AddStoryViewModel
    {
        public Story story { get; set; }
        public List<Category> categories { get; set; }
        public List<StoryType> storyTypes { get; set; }
        public List<Status> status { get; set; }
        public List<NationalOutlet> nationalOutlets { get; set; }
        public int[] SelectedValues { get; set; }

    }
}
