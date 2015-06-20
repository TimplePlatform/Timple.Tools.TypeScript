using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Timple.Tools.TypeScript.Translators
{
  public class HttpObjectsTranslator : ITypeScriptPipelineTranslator
  {
    public void Prepare(TypeScriptPipelineTranslator translator) {
      translator.AddTailTranslator(this);
    }

    public bool Accepts(Type tp) {
      return (tp == typeof(HttpResponseMessage));
    }

    public string Translate(Type tp, TypeScriptPipelineTranslator translator) {
      return "any";
    }
  }
}
