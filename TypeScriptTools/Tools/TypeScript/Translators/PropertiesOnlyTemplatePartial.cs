using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Timple.Tools.TypeScript.Translators {
  public partial class PropertiesOnlyTemplate {
    public PropertiesOnlyTemplate(String typeName, Type theType, ITypeScriptTranslator translator, bool skipImplements = false) {
      TheType = theType;
      Translator = translator;
      TypeName = typeName;
      SkipImplements = skipImplements;
    }

    public Type TheType { get; set; }
    public ITypeScriptTranslator Translator { get; set; }
    public String TypeName { get; set; }
    public bool SkipImplements { get; set; }
    public String CallingTypeName {
      get {
        var last = Translator.TranslatingStack.Skip(Translator.TranslatingStack.Count - 1).FirstOrDefault();
        return last != null ? last.FullName : String.Empty;
      }
    }

    public string ToCamelCase(string str) {
      if (string.IsNullOrEmpty(str))
        return str;

      if (!char.IsUpper(str[0]) || (str.Length > 1 && char.IsUpper(str[1]) && char.IsUpper(str[0])))
        return str;

      string camelCase = char.ToLower(str[0], CultureInfo.InvariantCulture).ToString(CultureInfo.InvariantCulture);
      if (str.Length > 1)
        camelCase += str.Substring(1);

      return camelCase;
    }

    public string Extends {
      get {
        var inters = TheType.GetInterfaces().ToList();
        if (TheType.BaseType != null && TheType.BaseType != typeof(object)) {
          inters.Add(TheType.BaseType);
        }

        if (inters.Count == 0)
          return string.Empty;

        return "extends " + string.Join(", ", inters.Select(x => Translator.Translate(x)).Where(x => !string.IsNullOrEmpty(x)));
      }
    }
  }
}
