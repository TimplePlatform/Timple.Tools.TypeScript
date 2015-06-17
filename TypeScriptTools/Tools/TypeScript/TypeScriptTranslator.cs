using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Timple.Tools.TypeScript.Translation;

namespace Timple.Tools.TypeScript
{
  public class TypeScriptTranslator
  {
    private readonly HashSet<String> translatedTypes;
    private readonly StreamWriter writer;

    public TypeScriptTranslator(StreamWriter writer) {
      translatedTypes = new HashSet<string>();
      this.writer = writer;
      writer.Write(new TypeScriptFileHeaderTemplate().TransformText());
      writer.Flush();
    }

    public void TranslateController(Type controller) {
      ApiService apiService = new ApiService(controller);

      foreach (var call in apiService.Calls) {
        TranslateType(call.ReturningType);
        foreach (var p in call.Parameters)
          TranslateType(p.Type);
      }

      TypeScriptControllersTemplate template = new TypeScriptControllersTemplate();
      template.Service = apiService;
      writer.Write(template.TransformText());
      writer.Flush();
    }

    public void TranslateType(Type tp) {
      if (tp.IsArray) {
        TranslateType(tp.GetElementType());
        return;
      }

      if (translatedTypes.Contains(tp.FullName)
          || !(tp.IsClass || tp.IsInterface || tp.IsEnum)
          || tp.Namespace.StartsWith("System"))
        return;

      translatedTypes.Add(tp.FullName);

      if (tp.BaseType != null && tp.BaseType != typeof(object))
        TranslateType(tp.BaseType);

      foreach (var i in tp.GetInterfaces())
        TranslateType(i);

      foreach (var p in tp.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance))
        TranslateType(p.PropertyType);

      if (tp.IsClass) {
        TypeScriptClassTemplate template = new TypeScriptClassTemplate();
        template.TheType = tp;
        writer.Write(template.TransformText());
      } else if (tp.IsEnum) {
        TypeScriptEnumTemplate template = new TypeScriptEnumTemplate();
        template.TheType = tp;
        writer.Write(template.TransformText());
      } else {
        TypeScriptInterfaceTemplate template = new TypeScriptInterfaceTemplate();
        template.TheType = tp;
        writer.Write(template.TransformText());
      }

      writer.Flush();

    }

  }
}
