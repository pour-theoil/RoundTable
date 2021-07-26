using RoundTable.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoundTable.Models.ViewModels
{
    public class DashboardViewModel
    {
        public List<StatusPieChart> Statuses { get; set; }
    }
}
