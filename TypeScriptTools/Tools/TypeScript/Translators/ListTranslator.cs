using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Timple.Tools.TypeScript.Translators
{
  public class ListTranslator : ITypeScriptPipelineTranslator
  {
    public void Prepare(TypeScriptPipelineTranslator translator) {
      translator.AddTranslator(this);
    }

    public bool Accepts(Type tp) {
      return tp.IsGenericType && tp.GetGenericArguments().Length == 1
        && (typeof(IEnumerable<>).MakeGenericType(tp.GetGenericArguments().First()).IsAssignableFrom(tp));
    }

    public string Translate(Type tp, TypeScriptPipelineTranslator translator) {
      String listType = translator.Translate(tp.GetGenericArguments().First());
      return "Array<" + listType + ">";
    }
  }
}
