
using System.Collections.Generic;
using System.Web.Http;


namespace OrnekWebApi.Controllers
{
    public class FaturaController : ApiController
    {
        [HttpGet]
        [Authorize]
        public IEnumerable<string> Get()
        {
            var firmaList = new List<string>()
            {
                "Firma 1",
                "Firma 2",
                "Firma 3",
            };
            return firmaList;
        }   
    }
}
