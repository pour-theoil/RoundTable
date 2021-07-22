using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoundTable.Models.ViewModels
{
    public class SourceModelView
    {
        public Source Source { get; set; }
        public List<Category> categories { get; set; }
    }
}
