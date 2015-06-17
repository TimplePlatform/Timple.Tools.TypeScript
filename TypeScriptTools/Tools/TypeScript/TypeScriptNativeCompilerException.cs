using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Timple.Tools.TypeScript
{
  public class TypeScriptNativeCompilerException : Exception
  {
    public List<String> Errors { get; private set; }
    
    public TypeScriptNativeCompilerException(List<String> errors) {
      this.Errors = errors;
    }
  }
}
