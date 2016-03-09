using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Timple.Tools.TypeScript.Translators {
  public class ComparableTranslator : DefaultPipelineTranslator, ITypeScriptPipelineTranslator {

    public override bool Accepts(Type tp) {
      return tp.IsGenericType && tp.GetGenericTypeDefinition() == typeof(IComparable<>);
    }


    public override string Translate(Type tp, TypeScriptPipelineTranslator translator) {
      return base.Translate(typeof(IComparable), translator);
    }
  }
}
