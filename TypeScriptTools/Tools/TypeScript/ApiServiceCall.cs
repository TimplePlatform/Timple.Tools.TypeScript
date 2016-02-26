using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Description;

namespace Timple.Tools.TypeScript {
  public class ApiServiceCall {
    public ApiServiceCall(ApiService svc, MethodInfo callMethod) {
      this.Service = svc;
      this.Parameters = new List<ApiServiceCallParameter>();
      this.CallMethod = callMethod;
      this.Route = callMethod.GetCustomAttribute<RouteAttribute>();
      this.HttpMethod = (IActionHttpMethodProvider)callMethod.GetCustomAttributes().Where(x => typeof(IActionHttpMethodProvider).IsAssignableFrom(x.GetType())).FirstOrDefault();
      this.ReturningType = callMethod.ReturnType;

      var rtattr = callMethod.GetCustomAttribute<ResponseTypeAttribute>();
      if (null != rtattr)
        ReturningType = rtattr.ResponseType;

      DiscoverParameters();
    }

    private void DiscoverParameters() {
      var parms = CallMethod.GetParameters();


      foreach (var p in parms) {
        if (p.ParameterType.Namespace == "System.Net.Http")
          continue;

        ApiServiceCallParameter callParam = new ApiServiceCallParameter(this, p);
        Parameters.Add(callParam);

      }
    }

    public MethodInfo CallMethod { get; private set; }
    public ApiService Service { get; private set; }
    public IActionHttpMethodProvider HttpMethod { get; private set; }
    public RouteAttribute Route { get; private set; }
    public Type ReturningType { get; private set; }
    public List<ApiServiceCallParameter> Parameters { get; private set; }
  }
}
