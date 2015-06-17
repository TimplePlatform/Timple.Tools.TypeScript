using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Timple.Tools.TypeScript.Translation
{
  public partial class TypeScriptClassTemplate
  {
    public Type TheType { get; set; }
  }

  public partial class TypeScriptInterfaceTemplate
  {
    public Type TheType { get; set; }
  }

  public partial class TypeScriptEnumTemplate
  {
    public Type TheType { get; set; }
  }

  public partial class TypeScriptControllersTemplate
  {
    public ApiService Service { get; set; }
  }
}
