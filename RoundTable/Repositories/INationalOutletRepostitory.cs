using RoundTable.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoundTable.Repositories
{
    public interface INationalOutletRepostitory
    {
        List<NationalOutlet> GetAllNationalOutlet();
        NationalOutlet GetNationalOutletById(int id);
    }
}
