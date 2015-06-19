using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Timple.Tools.TypeScript.Translators
{
  public class ArrayTranslator : ITypeScriptPipelineTranslator
  {
    public void Prepare(TypeScriptPipelineTranslator translator) {
      translator.AddTranslator(this);
    }

    public bool Accepts(Type tp) {
      return tp.IsArray;
    }

    public string Translate(Type tp, TypeScriptPipelineTranslator translator) {
      String eleType = translator.Translate(tp.GetElementType());
      return eleType + "[]";
    }
  }
}
