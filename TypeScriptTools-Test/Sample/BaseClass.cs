using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypeScriptTools_Test.Sample
{
  public abstract class BaseClass : IInterface
  {
    public string OnlyGet { get { return null; } }

    public string OnlySet { set { } }

    public string GetAndSet { get; set; }
  }
}
