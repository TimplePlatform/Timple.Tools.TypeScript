using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Timple.Tools.TypeScript.Translators
{
  public partial class PropertiesOnlyTemplate
  {
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

    public String Extends {
      get {
        if (TheType.BaseType != null && TheType.BaseType != typeof(object)) {
          var basetp = Translator.Translate(TheType.BaseType);
          if (String.IsNullOrEmpty(basetp))
            return String.Empty;
          return "extends " + basetp;
        }

        return String.Empty;
      }
    }

    public String Implements {
      get {
        if (SkipImplements)
          return String.Empty;

        var inters = TheType.GetInterfaces();
        if (inters.Length == 0)
          return String.Empty;
        var validInters = inters.Select(x => Translator.Translate(x)).Where(x => !String.IsNullOrEmpty(x));
        if (validInters.Count() == 0)
          return String.Empty;

        return "implements " + String.Join(", ", validInters);
      }
    }
  }
}
