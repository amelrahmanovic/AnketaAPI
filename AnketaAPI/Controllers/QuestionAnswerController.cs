using AnketaAPI.Models;
using AnketaAPI.Repository;
using AnketaAPI.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AnketaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionAnswerController : ControllerBase
    {
        private readonly IRepository<QuestionAnswer> _questionAnswerRepository;
        private MapperConfiguration config;
        private Mapper mapper;

        public QuestionAnswerController(IRepository<QuestionAnswer> questionAnswerRepository)
        {
            _questionAnswerRepository= questionAnswerRepository;

            config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
            mapper = new Mapper(config);
        }
        //// GET: api/<QuestionAnswerController>
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET api/QuestionAnswer/5
        [HttpGet("{id}")]
        public IEnumerable<QuestionAnswer> Get(int id)
        {
            return _questionAnswerRepository.GetById_Custom(id);
        }

        // POST api/QuestionAnswer
        [Authorize]
        [HttpPost]
        public IActionResult Post(QuestionAnswerNewVM questionAnswerNewVM)
        {
            var questionAnswer = mapper.Map<QuestionAnswer>(questionAnswerNewVM);

            _questionAnswerRepository.Add(questionAnswer);

            return Ok();
        }
        //// POST api/QuestionAnswer
        //[HttpPost]
        //public void Post(QuestionAnswerNewVM questionAnswerNewVM)
        //{
        //    var questionAnswer = mapper.Map<QuestionAnswer>(questionAnswerNewVM);

        //    _questionAnswerRepository.Add(questionAnswer);
        //}

        //// PUT api/<QuestionAnswerController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        // DELETE api/<QuestionAnswerController>/5
        [Authorize]
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            return _questionAnswerRepository.Delete(id, 0) == true ? Ok() : NotFound(); 
        }
    }
}
