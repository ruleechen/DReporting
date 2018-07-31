using DReporting.Models;
using System.Linq;

namespace DReporting.Core
{
    public interface ITemplateMgr
    {
        IQueryable<TemplateModel> QueryTemplates();

        TemplateModel GetDefaultTemplate();

        TemplateModel GetTemplate(string templateId, bool loadReport);

        void DeleteTemplate(string templateId);

        TemplateModel SaveTemplate(TemplateModel model);
    }
}
