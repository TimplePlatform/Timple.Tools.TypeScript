using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace TypeScriptTools_Test.Sample
{
  [RoutePrefix("api")]
  public class SampleController : ApiController
  {

    [Route("")]
    [HttpGet]
    public Class[] EmptyHttpGet() {
      return null;
    }

    [Route("")]
    [HttpPost]
    public void EmptyHttpPost(Class classParameter) {

    }

    [Route("parameter/{parameter}")]
    [HttpGet]
    public String UriParameterHttpGet(String parameter) {
      return null;
    }

    [Route("parameter/{parameter}/other")]
    [HttpPost]
    public String UriAndBodyParameterHttpPost(String parameter, [FromBody] IInterface bodyParam) {
      return null;
    }

    [Route("IInterface")]
    [HttpGet]
    public IInterface ReturnIInterface() {
      return null;
    }

    [Route("BaseClass")]
    [HttpGet]
    public BaseClass ReturnBaseClass() {
      return null;
    }

    
  }
}
