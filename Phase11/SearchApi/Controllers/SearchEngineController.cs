using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SearchApi.Models;

namespace SearchApi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class SearchEngineController : ControllerBase
    {
        ISearchEngine Engine;

        public SearchEngineController(ISearchEngine engine)
        {
            Engine = engine;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<string>> Search([FromQuery] string input)
        {
            var query = new Query(input);

            return Engine.Search(query.Normals, query.Pluses, query.Minuses);
        }
        
    }
}