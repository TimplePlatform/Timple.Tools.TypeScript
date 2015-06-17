using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace Timple.Tools.TypeScript
{
  public class ApiControllerClientBuilder
  {
    private readonly TypeScriptTranslator translator;

    public ApiControllerClientBuilder(TypeScriptTranslator translator) {
      this.translator = translator;
    }

    public void GenerateForAssemblies(Assembly[] assemblies) {
      foreach (var ass in assemblies)
        TranslateAssembly(ass);
    }

    private void TranslateAssembly(Assembly assembly) {
      var types = assembly.GetTypes();

      foreach (var tp in types) {
        if (typeof(ApiController).IsAssignableFrom(tp))
          continue;

        translator.TranslateController(tp);
      }
    }

  }
}
