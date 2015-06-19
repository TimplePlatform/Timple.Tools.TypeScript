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
  public class ApiControllerClientBuilder : IDisposable
  {
    private readonly ITypeScriptTranslator translator;
    private readonly StreamWriter stream;

    public ApiControllerClientBuilder(ITypeScriptTranslator translator) {
      this.translator = translator;
    }

    public ApiControllerClientBuilder(String filePath) {
      stream = new StreamWriter(File.Create(filePath));
      translator = new TypeScriptPipelineTranslator(stream);
    }

    public void GenerateForAssemblies(Assembly[] assemblies) {
      foreach (var ass in assemblies)
        TranslateAssembly(ass);
    }

    private void TranslateAssembly(Assembly assembly) {
      var types = assembly.GetTypes();

      foreach (var tp in types) {
        if (!tp.IsSubclassOf(typeof(ApiController)))
          continue;

        translator.Translate(tp);
      }
    }


    public void Dispose() {
      if (stream != null)
        stream.Dispose();
    }
  }
}
