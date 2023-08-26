using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Template.Model.Models.Templates.CommandModel;

public class GetTemplateRequest
{
   public Guid TemplateId { get; set; }
    public string UserName{ get; set; }
}
