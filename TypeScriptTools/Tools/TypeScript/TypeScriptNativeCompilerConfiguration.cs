using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Timple.Tools.TypeScript
{
  public class TypeScriptNativeCompilerConfiguration
  {
    public String NativeBasePath { get; set; }
    public String CodePage { get; set; }
    public bool RemoveComments { get; set; }
    public String TargetVersion { get; set; }

    public String BuildArguments() {
      StringBuilder buf = new StringBuilder();

      if (!String.IsNullOrEmpty(CodePage)) {
        buf.Append(" --codepage ");
        buf.Append(CodePage);
      }

      if (RemoveComments) {
        buf.Append(" --removeComments");
      }

      if (!String.IsNullOrEmpty(TargetVersion)) {
        buf.Append(" --target ");
        buf.Append(TargetVersion);
      }

      return buf.ToString();
    }
  }
}
