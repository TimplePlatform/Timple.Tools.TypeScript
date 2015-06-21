using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Timple.Tools.TypeScript.Translators
{
  public class NullableTranslator : ITypeScriptPipelineTranslator
  {
    public void Prepare(TypeScriptPipelineTranslator translator) {
      translator.AddTailTranslator(this);
    }

    public bool Accepts(Type tp) {
      return tp.IsGenericType && (typeof(Nullable<>)).IsAssignableFrom(tp.GetGenericTypeDefinition());
    }

    public string Translate(Type tp, TypeScriptPipelineTranslator translator) {
      return translator.Translate(tp.GetGenericArguments().First());
    }
  }
}
