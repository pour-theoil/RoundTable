using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoundTable.Models.ViewModels
{
    public class SourceViewModel
    {
        public Source Source { get; set; }
        public List<Category> Categories { get; set; }
        public int[] SelectedValues { get; set; }
    }
}
