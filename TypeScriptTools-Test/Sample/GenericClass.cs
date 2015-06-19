using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypeScriptTools_Test.Sample
{
  public class GenericClass<T>
  {
    public List<T> Values { get; set; }
    public T SingleValue { get; set; }
  }
}
