using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Timple.Tools.TypeScript.Translators
{
  public class AsyncTypesTranslator : ITypeScriptPipelineTranslator
  {
    public void Prepare(TypeScriptPipelineTranslator translator) {
      translator.AddTailTranslator(this);
    }

    public bool Accepts(Type tp) {
      return (tp.IsGenericType && tp.GetGenericTypeDefinition() == typeof(Task<>))
        || (!tp.IsGenericType && tp == typeof(Task));
    }

    public string Translate(Type tp, TypeScriptPipelineTranslator translator) {
      if (tp.IsGenericType)
        return translator.Translate(tp.GetGenericArguments().First());

      return "any";
    }
  }
}
