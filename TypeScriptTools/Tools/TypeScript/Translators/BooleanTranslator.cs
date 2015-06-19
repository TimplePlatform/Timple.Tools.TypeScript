using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Timple.Tools.TypeScript.Translators
{
  public class BooleanTranslator : ITypeScriptPipelineTranslator
  {
    public bool Accepts(Type tp) {
      return tp == typeof(bool);
    }

    public string Translate(Type tp, TypeScriptPipelineTranslator translator) {
      return "boolean";
    }

    public void Prepare(TypeScriptPipelineTranslator translator) {
      translator.AddTranslator(this);
    }
  }
}
