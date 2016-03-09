using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Timple.Tools.TypeScript.Translators {
  public partial class ApiControllerTemplate {
    public ApiService Service { get; set; }
    public Type ControllerType {
      get {
        return Service.ControllerType;
      }
    }
    public ITypeScriptTranslator Translator { get; set; }

    public String GenerateCallParameters(ApiServiceCall call) {
      return String.Join(", ", call.Parameters.Select(x => x.Parameter.Name + ": " + Translator.Translate(x.Type)));
    }

    public String GenerateCallReturn(ApiServiceCall call) {
      //if (call.ReturningType == typeof(void))
      //  return String.Empty;
      return ": Promise<" + Translator.Translate(call.ReturningType) + ">";
    }

    public String GenerateCallRouteConcat(ApiServiceCall call) {

      if (String.IsNullOrEmpty(call.Route.Template))
        return "\"" + String.Empty + "\"";

      StringBuilder r = new StringBuilder();

      r.Append('"');

      foreach (var c in call.Route.Template) {
        if (c == '{') {
          r.Append('"');
          r.Append(" + ");
          continue;
        }

        if (c == '}') {
          r.Append(" + ");
          r.Append('"');
          continue;
        }

        r.Append(c);

      }

      r.Append('"');
      return r.ToString();
    }

    public String GetCallBodyParameter(ApiServiceCall call) {
      var p = call.Parameters.FirstOrDefault(x => x.FromBody != null);
      if (p != null)
        return p.Parameter.Name;

      return "null";
    }

    public string GetCallConfig(ApiServiceCall call) {
      var ps = call.Parameters.Where(x => x.FromQueryString != null);
      if (ps.Count() == 0)
        return "{}";
      return "{params: {" + string.Join(",", ps.Select(x => $"{ x.Parameter.Name}:{ x.Parameter.Name}")) + "} }";
    }

    public String GenerateRESTCall(ApiServiceCall call, String routeVar, String argVar, string argCfg) {
      bool hasArg = call.HttpMethod.HttpMethods.Any(x => x == HttpMethod.Post || x == HttpMethod.Put);
      String restCall = $"Timple.http_{ call.HttpMethod.HttpMethods.First().Method.ToLower()}";

      restCall += "<" + Translator.Translate(call.ReturningType) + ">";
      restCall += "(this.$http," + routeVar;
      if (hasArg)
        restCall += ", " + argVar;
      restCall += "," + argCfg + ");";
      return restCall;
    }
  }
}
