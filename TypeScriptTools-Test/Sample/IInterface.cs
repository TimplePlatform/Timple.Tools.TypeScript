using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypeScriptTools_Test.Sample
{
  public interface IInterface
  {
    String OnlyGet { get; }
    String OnlySet { set; }
    String GetAndSet { get; set; }
  }
}
