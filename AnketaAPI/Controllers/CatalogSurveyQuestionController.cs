using AnketaAPI.DataAccessObject;
using AnketaAPI.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AnketaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogSurveyQuestionController : ControllerBase
    {
        private readonly IRepository<CatalogSurveyQuestion> _catalogSurveyQuestionRepository;
        public CatalogSurveyQuestionController(IRepository<CatalogSurveyQuestion> catalogSurveyQuestionRepository)
        {
            _catalogSurveyQuestionRepository = catalogSurveyQuestionRepository;     
        }
        //// GET: api/<CatalogSurveyQuestionController>
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET api/<CatalogSurveyQuestionController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/<CatalogSurveyQuestionController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/<CatalogSurveyQuestionController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        // DELETE api/CatalogSurveyQuestion/5?catalogSurveyId=6
        [HttpDelete("{questionId}")]
        public IActionResult Delete(int questionId, [FromQuery] int catalogSurveyId)
        {
            var result = _catalogSurveyQuestionRepository.Delete(questionId, catalogSurveyId);
            if (result)
                return Ok();
            return NotFound();
        }
    }
}
