using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Timple.Tools.TypeScript.Translators
{
  public class ApiControllerTranslator : ITypeScriptPipelineTranslator
  {
    private bool headerIncluded;
    public ApiControllerTranslator() {

    }

    public bool Accepts(Type tp) {
      return typeof(ApiController).IsAssignableFrom(tp);
    }

    public string Translate(Type tp, TypeScriptPipelineTranslator translator) {
      if (!headerIncluded) {
        headerIncluded = true;
        translator.Writer.Write(new TypeScriptControllersFileHeaderTemplate().TransformText());
      }

      ApiService svc = new ApiService(tp);
      ApiControllerTemplate template = new ApiControllerTemplate();
      template.Service = svc;
      template.Translator = translator;
      translator.Writer.Write(template.TransformText());
      return tp.FullName;
    }

    public void Prepare(TypeScriptPipelineTranslator translator) {
      translator.AddTranslator(this);
    }
  }
}
