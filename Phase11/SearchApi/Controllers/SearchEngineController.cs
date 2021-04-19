using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SearchApi.Models;
using System;

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

        [HttpPost] 
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Document> AddDocument([FromBody] Document document)
        {
            if (document is null)
                return BadRequest(new ArgumentNullException());
            Engine.PostDocument(document);
            return Ok();
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<Document> GetDocument(string id)
        {
            return Ok(Engine.GetDocument(id));
        }
        
    }
}