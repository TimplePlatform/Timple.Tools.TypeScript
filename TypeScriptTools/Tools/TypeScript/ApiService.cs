using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Http;
using System.Reflection;

namespace Timple.Tools.TypeScript
{
  public class ApiService
  {
    public ApiService(Type controllerType) {
      Calls = new List<ApiServiceCall>();
      this.ControllerType = controllerType;
      RoutePrefix = controllerType.GetCustomAttribute<RoutePrefixAttribute>();
      DiscoverRoutes();
    }

    private void DiscoverRoutes() {
      var methods = ControllerType.GetMethods();

      foreach (var mt in methods) {
        var routeAttr = mt.GetCustomAttribute<RouteAttribute>();
        if (null == routeAttr)
          continue;

        ApiServiceCall call = new ApiServiceCall(this,mt);
        Calls.Add(call);
      }
    }

    public Type ControllerType { get; private set; }
    public RoutePrefixAttribute RoutePrefix { get; private set; }
    public List<ApiServiceCall> Calls { get; private set; }
  }
}
