using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Reflection;
using Timple.Tools.TypeScript;
using TypeScriptTools_Test.Sample;

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
        TypeScriptTranslator translator = new TypeScriptTranslator(sw);
        translator.TranslateType(typeof(Class));
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
        TypeScriptTranslator translator = new TypeScriptTranslator(sw);
        translator.TranslateController(typeof(SampleController));
        sw.Flush();
      }
    }


    [TestMethod]
    public void CompileScriptController() {
      TypeScriptNativeCompiler compiler = new TypeScriptNativeCompiler(Path.GetDirectoryName(tsFile));
      compiler.Compile(tsFile);
    }

  }
}
