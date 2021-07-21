using RoundTable.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoundTable.Repositories
{
    public interface ICategoryRepository
    {
        List<Category> GetAllCategory();
        Category GetCategoryById(int id);
    }
}
