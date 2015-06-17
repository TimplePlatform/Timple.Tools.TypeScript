using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Timple.Tools.TypeScript.Translation
{
  public static class TypeScriptExtensions
  {
    public static String ToTypeScriptName(this Type tp) {
      String tpName = "any";

      if (tp.IsArray) {
        return tp.GetElementType().ToTypeScriptName() + "[]";
      }

      if (tp == typeof(String))
        tpName = "string";
      else if (tp == typeof(int) || tp == typeof(long) || tp == typeof(double) || tp == typeof(Int16) || tp == typeof(Int32) || tp == typeof(Int64))
        tpName = "number";
      else if (tp == typeof(bool) || tp == typeof(Boolean))
        tpName = "boolean";
      else if (tp == typeof(void))
        tpName = "void";
      else if (tp == typeof(Guid))
        tpName = "string";
      else if (tp == typeof(DateTime))
        tpName = "Date";
      else if (tp.IsGenericType && tp.GetGenericTypeDefinition() == typeof(Nullable<>))
        tpName = tp.GetGenericArguments().First().ToTypeScriptName();
      else if (tp.IsGenericType && typeof(IEnumerable<>).MakeGenericType(tp.GenericTypeArguments).IsAssignableFrom(tp)) {
        tpName = "Array<" + String.Join(",", tp.GenericTypeArguments.Select(x => x.ToTypeScriptName())) + ">";
      } else //if (tp.IsClass)
        tpName = tp.FullName;

      return tpName;
    }

    public static PropertyInfo[] GetValidTypeScriptProperties(this Type tp) {
      return tp.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
    }

    public static String GetTypeScriptExtends(this Type tp) {
      if (tp.BaseType == null || tp.BaseType == typeof(object))
        return String.Empty;

      return "extends " + tp.BaseType.FullName;
    }

    public static String GetTypeScriptImplements(this Type tp) {
      var inters = tp.GetInterfaces();
      if (inters == null || inters.Length == 0)
        return String.Empty;

      return "implements " + String.Join(", ", inters.Select(x => x.FullName));
    }

    public static List<Tuple<String, long>> GetValidTypeScriptEnumValues(this Type tp) {
      var names = Enum.GetNames(tp);
      var values = Enum.GetValues(tp);

      var ret = new List<Tuple<String, long>>(names.Length);
      for (var i = 0; i < names.Length; i++) {
        ret.Add(new Tuple<string, long>(names[i], (long)values.GetValue(i)));
      }
      return ret;
    }

    public static String GetTypeScriptParameters(this ApiServiceCall call) {
      return String.Join(", ", call.Parameters.Select(x => x.Parameter.Name + ": " + x.Type.ToTypeScriptName()));
    }

    public static String GetBodyParameter(this ApiServiceCall call) {
      var p = call.Parameters.FirstOrDefault(x => x.FromBody != null);
      if (p != null)
        return p.Parameter.Name;

      return "null";
    }

    public static String GenerateRouteConcat(this ApiServiceCall call) {
      String[] subs = (call.Route.Template ?? String.Empty).Split('{');

      var parts = subs.Select(x => x.Length > 0 && x.Last() == '}' ? x.Substring(0, x.Length - 1) : "\"" + x + "\"");

      return String.Join(" + ", parts);
    }

    public static String GenerateJQueryCall(this ApiServiceCall call, String routeVar, String argVar) {
      String jquery;
      bool hasArg = false;

      if (call.HttpMethod.HttpMethods.Contains(HttpMethod.Get))
        jquery = "jQuery.getJSON";
      else if (call.HttpMethod.HttpMethods.Contains(HttpMethod.Post)) {
        jquery = "jQuery.postJSON";
        hasArg = true;
      } else if (call.HttpMethod.HttpMethods.Contains(HttpMethod.Delete))
        jquery = "jQuery.deleteJSON";
      else if (call.HttpMethod.HttpMethods.Contains(HttpMethod.Put)) {
        jquery = "jQuery.putJSON";
        hasArg = true;
      } else
        throw new ArgumentException("HttpMethod");

      jquery = jquery + "(" + routeVar;
      if (hasArg)
        jquery += ", " + argVar;
      jquery += ")";

      return jquery;
    }
  }
}
