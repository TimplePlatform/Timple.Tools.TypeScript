using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web.Http;

namespace Timple.Tools.TypeScript {
  public class ApiServiceCallParameter {
    public ApiServiceCallParameter(ApiServiceCall call, ParameterInfo info) {
      this.ServiceCall = call;
      this.Parameter = info;
      this.FromBody = info.GetCustomAttribute<FromBodyAttribute>();
      this.FromUri = info.GetCustomAttribute<FromUriAttribute>();

      String route = call.Route.Template ?? String.Empty;

      if (FromUri != null) {
        if (!route.Contains($"{{{Parameter.Name}}}")) {
          FromQueryString = FromUri;
          FromUri = null;
        }
      } else if (FromBody == null) {
        if (route.Contains(Parameter.Name))
          FromUri = new FromUriAttribute();
        else
          FromBody = new FromBodyAttribute();
      }

    }

    public ParameterInfo Parameter { get; private set; }
    public ApiServiceCall ServiceCall { get; private set; }
    public FromBodyAttribute FromBody { get; private set; }
    public FromUriAttribute FromUri { get; private set; }
    public FromUriAttribute FromQueryString { get; private set; }
    public Type Type {
      get {
        return Parameter.ParameterType;
      }
    }
  }
}
