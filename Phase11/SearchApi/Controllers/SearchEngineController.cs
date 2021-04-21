using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SearchApi.Models;
using System;
using System.IO;

namespace SearchApi.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class SearchEngineController : ControllerBase
    {
        ISearchEngine Engine;

        public SearchEngineController(ISearchEngine engine)
        {
            Engine = engine;
        }

        [HttpGet]
        [Route("Search")]
        [Route("")]
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
            return CreatedAtAction(nameof(GetDocument), new { id = document.DocumentId }, document);
        }

        [HttpGet("{id}", Name=nameof(GetDocument))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Document> GetDocument(string id)
        {
            var document = Engine.GetDocument(id);
            if(document is null)
                return NotFound(new FileNotFoundException());
            return Ok(document);
        }
    }
}