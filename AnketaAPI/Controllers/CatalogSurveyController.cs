using AnketaAPI.DataAccessObject;
using AnketaAPI.Models;
using AnketaAPI.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AnketaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogSurveyController : ControllerBase
    {
        private readonly IRepository<CatalogSurvey> _catalogSurveyRepository;
        private MapperConfiguration config;
        private Mapper mapper;

        public CatalogSurveyController(IRepository<CatalogSurvey> catalogSurveyRepository)
        {
            _catalogSurveyRepository = catalogSurveyRepository;

            config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
            mapper = new Mapper(config);
        }
        // GET: api/CatalogSurvey
        [HttpGet]
        public IEnumerable<CatalogSurveyVM> Get()
        {
            IEnumerable<CatalogSurvey> catalogSurveys = _catalogSurveyRepository.GetAll();
            var catalogSurveyVM = mapper.Map<List<CatalogSurveyVM>>(catalogSurveys);

            return catalogSurveyVM;
        }

        // GET api/<CatalogSurveyController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST api/CatalogSurvey
        [HttpPost]
        public ActionResult Post([FromBody] SurveyNewVM surveyNewVM)
        {
            var result = _catalogSurveyRepository.Add(new CatalogSurvey() { Name= surveyNewVM.name, Id=0, Created = DateTime.Now});
            if (result)
                return Created();
            return BadRequest();
        }

        // PUT api/<CatalogSurveyController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //DELETE api/CatalogSurvey/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var result = _catalogSurveyRepository.Delete(_catalogSurveyRepository.GetById(id));
            if (result)
                return Ok();
            return NotFound();
        }
    }
}
