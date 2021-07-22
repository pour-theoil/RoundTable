using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace RoundTable.Models
{
    public class Story
    {
        public int Id { get; set; }
        public string Slug { get; set; }
        [DisplayName("Type")]
        public int StoryTypeId { get; set; }
        public StoryType StoryType { get; set; }
        [DisplayName("National")]
        public int NationalId { get; set; }
        public NationalOutlet NationalOutlet { get; set; }
        public string Summary { get; set; }
        [DisplayName("Status")]
        public int StatusId { get; set; }
        [DisplayName("Category")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public Status Status { get; set; }
        public int ReporterId { get; set; }
        public string StoryURl { get; set; }
        public DateTime LastStatusUpdate { get; set; }
        public List<Source> Sources { get; set; }
    }
}
