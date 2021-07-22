using RoundTable.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoundTable.Repositories
{
    public interface IStatusRepository
    {
        List<Status> GetAllStatus();
        Status GetStatusById(int id);
    }
}
