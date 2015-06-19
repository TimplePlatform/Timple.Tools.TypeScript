using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypeScriptTools_Test.Sample
{
  public abstract class BaseClass : IInterface
  {
    public Version VersionField { get; set; }

    public string OnlyGet { get { return null; } }

    public string OnlySet { set { } }

    public string GetAndSet { get; set; }

    public List<object[]> ListOfObjectArray { get; set; }

    public GenericClass<String> GenericClassString { get; set; }
    public GenericClass<int> GenericClassInt { get; set; }
    public GenericClass<IInterface> GenericClassInterface { get; set; }
  }
}
