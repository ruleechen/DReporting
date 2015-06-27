using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DReporting.Models;

namespace DReporting.Core
{
    public interface IReportStorage
    {
        IQueryable<ReportModel> QueryReports();

        IQueryable<CategoryModel> QueryCategories();

        ReportModel GetDefaultReport();

        ReportModel GetReport(string reportId);

        CategoryModel GetCategory(string categoryId);

        void DeleteReport(string reportId);

        void DeleteCategory(string categoryId);

        ReportModel SaveReport(ReportModel model);

        CategoryModel SaveCategory(CategoryModel model);
    }
}
