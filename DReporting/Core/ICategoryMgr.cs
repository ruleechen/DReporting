using DReporting.Models;
using System.Linq;

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
