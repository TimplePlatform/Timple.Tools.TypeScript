using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Timple.Tools.TypeScript
{
  public interface ITypeScriptPipelineTranslator
  {
    void Prepare(TypeScriptPipelineTranslator translator);

    bool Accepts(Type tp);

    String Translate(Type tp, TypeScriptPipelineTranslator translator);
  }
}
