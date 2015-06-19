using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Timple.Tools.TypeScript.Translators
{
  public class NumericTranslator : ITypeScriptPipelineTranslator
  {
    public bool Accepts(Type tp) {
      return tp == typeof(long) || tp == typeof(int) || tp == typeof(short) || tp == typeof(double) || tp == typeof(float);
    }

    public string Translate(Type tp, TypeScriptPipelineTranslator translator) {
      return "number";
    }

    public void Prepare(TypeScriptPipelineTranslator translator) {
      translator.AddTranslator(this);
    }
  }
}
