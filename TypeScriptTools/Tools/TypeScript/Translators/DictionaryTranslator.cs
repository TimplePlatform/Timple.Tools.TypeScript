using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Timple.Tools.TypeScript.Translators
{
  public class DictionaryTranslator : ITypeScriptPipelineTranslator
  {
    public void Prepare(TypeScriptPipelineTranslator translator) {
      translator.AddTailTranslator(this);
    }

    public bool Accepts(Type tp) {
      return tp.IsGenericType && tp.GetGenericArguments().Length == 2
        && (typeof(IDictionary<,>).MakeGenericType(tp.GetGenericArguments()).IsAssignableFrom(tp));
    }

    public string Translate(Type tp, TypeScriptPipelineTranslator translator) {
      return "any";
    }
  }
}
