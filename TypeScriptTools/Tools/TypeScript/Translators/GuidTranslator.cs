using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Timple.Tools.TypeScript.Translators
{
  public class GuidTranslator : ITypeScriptPipelineTranslator
  {
    public void Prepare(TypeScriptPipelineTranslator translator) {
      translator.AddTranslator(this);
    }

    public bool Accepts(Type tp) {
      return typeof(Guid) == tp;
    }

    public string Translate(Type tp, TypeScriptPipelineTranslator translator) {
      return "string";
    }
  }
}
