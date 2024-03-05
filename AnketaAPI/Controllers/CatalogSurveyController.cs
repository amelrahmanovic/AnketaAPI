using AnketaAPI.DataAccessObject;
using AnketaAPI.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AnketaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogSurveyController : ControllerBase
    {
        private readonly IRepository<CatalogSurvey> _catalogSurveyRepository;
        public CatalogSurveyController(IRepository<CatalogSurvey> catalogSurveyRepository)
        {
            _catalogSurveyRepository = catalogSurveyRepository;
        }
        // GET: api/<CatalogSurveyController>
        [HttpGet]
        public IEnumerable<CatalogSurvey> Get()
        {
            return _catalogSurveyRepository.GetAll();
        }

        // GET api/<CatalogSurveyController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST api/<CatalogSurveyController>
        [HttpPost]
        public ActionResult Post(CatalogSurvey catalogSurvey)
        {
            var result = _catalogSurveyRepository.Add(catalogSurvey);
            if (result)
                return Created();
            return BadRequest();
        }

        // PUT api/<CatalogSurveyController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        // DELETE api/<CatalogSurveyController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
