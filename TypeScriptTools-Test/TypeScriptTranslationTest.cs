using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Reflection;
using Timple.Tools.TypeScript;
using TypeScriptTools_Test.Sample;
using System.Threading;

namespace TypeScriptTools_Test
{
  [TestClass]
  public class TypeScriptTranslationTest
  {
    private readonly String tsFile;

    public TypeScriptTranslationTest() {
      tsFile = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "test.ts");
    }

    [TestMethod]
    public void TranslateType() {
      using (var sw = new StreamWriter(File.Create(tsFile))) {
        ITypeScriptTranslator translator = new TypeScriptPipelineTranslator(sw);
        translator.Translate(typeof(SampleController));
        sw.Flush();
      }
    }

    [TestMethod]
    public void CompileScriptTypes() {
      TypeScriptNativeCompiler compiler = new TypeScriptNativeCompiler(Path.GetDirectoryName(tsFile));
      compiler.Compile(tsFile);
    }

    [TestMethod]
    public void TranslateController() {
      using (var sw = new StreamWriter(File.Create(tsFile))) {
        ITypeScriptTranslator translator = new TypeScriptPipelineTranslator(sw);
        translator.Translate(typeof(SampleController));
        sw.Flush();
      }
    }


    [TestMethod]
    public void CompileScriptController() {
      TypeScriptNativeCompiler compiler = new TypeScriptNativeCompiler(Path.GetDirectoryName(tsFile));
      compiler.Compile(tsFile);
    }

    [TestMethod]
    public void CompileAssemblyControllers() {
      using (ApiControllerClientBuilder builder = new ApiControllerClientBuilder(Path.Combine(Path.GetDirectoryName(tsFile), "assemblies.ts"))) {
        builder.GenerateForAssemblies(AppDomain.CurrentDomain.GetAssemblies());
      }
    }

    [TestMethod]
    public void DirectoryAutoCompiler() {
      using (TypeScriptDirectoryAutoCompiler compiler = new TypeScriptDirectoryAutoCompiler(Path.GetDirectoryName(tsFile))) {
        var newTsFile = Path.Combine(Path.GetDirectoryName(tsFile), "new-ts.ts");
        File.WriteAllLines(newTsFile, new string[]{
          "class NewClass {",
          "  private name: string;",
          "  public get Name(){",
          "    return this.name;",
          "  }",
          "}"
        });

        Thread.Sleep(5000);

        var newJsFile = Path.Combine(Path.GetDirectoryName(newTsFile), Path.GetFileNameWithoutExtension(newTsFile) + ".js");
        if (!File.Exists(newJsFile))
          throw new FileNotFoundException(newJsFile);
      }
    }

  }
}
