using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DReporting.Models;

namespace DReporting.Core
{
    public interface ITemplateMgr
    {
        IQueryable<TemplateModel> QueryTemplates();

        TemplateModel GetDefaultTemplate();

        TemplateModel GetTemplate(string templateId);

        void DeleteTemplate(string templateId);

        TemplateModel SaveTemplate(TemplateModel model);
    }
}
