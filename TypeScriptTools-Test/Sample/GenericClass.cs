using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypeScriptTools_Test.Sample
{
  public class GenericClass<TValue>
  {
    public List<TValue> Values { get; set; }
    public TValue SingleValue { get; set; }
  }
}
