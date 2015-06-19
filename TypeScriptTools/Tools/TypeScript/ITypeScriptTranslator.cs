using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Timple.Tools.TypeScript
{
  public interface ITypeScriptTranslator
  {
    StreamWriter Writer { get; }
    String Translate(Type tp);
  }
}
