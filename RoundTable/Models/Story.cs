using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoundTable.Models
{
    public class Story
    {
        public int Id { get; set; }
        public string Slug { get; set; }
        public int StoryTypeId { get; set; }
        public StoryType StoryType { get; set; }
        public int NationalId { get; set; }
        public NationalOutlet NationalOutlet { get; set; }
        public string Summary { get; set; }
        public int StatusId { get; set; }
        public Status Status { get; set; }
        public int ReporterId { get; set; }
        public string StoryURl { get; set; }
        public DateTime LastStatusUpdate { get; set; }
        public List<Source> Sources { get; set; }
    }
}
