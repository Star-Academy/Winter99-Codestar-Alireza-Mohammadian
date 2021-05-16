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
        ISearchEngine engine;

        public SearchEngineController(ISearchEngine engine)
        {
            this.engine = engine;
        }

        [HttpGet]
        [Route("Search")]
        [Route("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<string>> Search([FromQuery] string input)
        {
            var query = new Query(input);
            return engine.Search(query.normals, query.pluses, query.minuses);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Document> AddDocument([FromBody] Document document)
        {
            if (document is null)
                return BadRequest(new ArgumentNullException());
            engine.PostDocument(document);
            return CreatedAtAction(nameof(GetDocument), new { id = document.documentId }, document);
        }

        [HttpGet("{id}", Name=nameof(GetDocument))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Document> GetDocument(string id)
        {
            var document = engine.GetDocument(id);
            if(document is null)
                return NotFound(new FileNotFoundException());
            return Ok(document);
        }
    }
}