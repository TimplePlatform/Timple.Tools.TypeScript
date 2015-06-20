using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Timple.Tools.TypeScript.Translators
{
  public class StringTranslator : ITypeScriptPipelineTranslator
  {
    public bool Accepts(Type tp) {
      return tp == typeof(string) || tp == typeof(decimal);
    }

    public string Translate(Type tp, TypeScriptPipelineTranslator translator) {
      return "string";
    }

    public void Prepare(TypeScriptPipelineTranslator translator) {
      translator.AddTranslator(this);
    }
  }
}
