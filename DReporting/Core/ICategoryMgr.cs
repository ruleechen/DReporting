using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DReporting.Models;

namespace DReporting.Core
{
    public interface ICategoryMgr
    {
        IQueryable<CategoryModel> QueryCategories();

        CategoryModel GetCategory(string categoryId);

        void DeleteCategory(string categoryId);

        CategoryModel SaveCategory(CategoryModel model);
    }
}
