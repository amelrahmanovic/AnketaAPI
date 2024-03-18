using AnketaAPI.Models;
using AnketaAPI.Repository;
using AnketaAPI.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AnketaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly IRepository<Question> _questionRepository;
        private readonly IRepository<CatalogSurveyQuestion> _catalogSurveyQuestionRepository;
        private readonly IRepository<QuestionAnswer> _questionAnswerRepository;
        private MapperConfiguration config;
        private Mapper mapper;

        public QuestionController(IRepository<Question> questionRepository, IRepository<CatalogSurveyQuestion> catalogSurveyQuestionRepository, IRepository<QuestionAnswer> questionAnswerRepository)
        {
            _questionRepository = questionRepository;
            _catalogSurveyQuestionRepository = catalogSurveyQuestionRepository;
            _questionAnswerRepository = questionAnswerRepository;

            config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
            mapper = new Mapper(config);
        }
        //// GET: api/<QuestionController>
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET api/Question/5
        [Authorize]
        [HttpGet("{id}")]
        public List<QuestionVM> Get(int id)
        {
            IEnumerable<CatalogSurveyQuestion> catalogSurveyQuestions = _catalogSurveyQuestionRepository.GetById_Custom(id);
            List<CatalogSurveyQuestionVM> catalogSurveyQuestionVMs = mapper.Map<List<CatalogSurveyQuestionVM>>(catalogSurveyQuestions);
            List<int> ids = new List<int>();
            foreach (var item in catalogSurveyQuestionVMs)
            {
                ids.Add(item.QuestionId);
            }
            List<Question> questions = _questionRepository.GetById_Custom2(ids);
            List<QuestionVM> questionVM = mapper.Map<List<QuestionVM>>(questions);
            foreach (var item in questionVM)
            {
                item.QuestionAnswer = mapper.Map<List<QuestionAnswerVM>>(_questionAnswerRepository.GetById_Custom(item.Id));
            }

            return questionVM;
        }

        // POST api/Question
        [HttpPost]
        public ActionResult Post([FromBody] QuestionNewVM questionNewVM)
        {
            Question questionSaved = _questionRepository.Add_Custom (new Question() { Id = 0, Name = questionNewVM.name });
            _catalogSurveyQuestionRepository.Add(new CatalogSurveyQuestion() { QuestionId = questionSaved.Id, CatalogSurveyId = questionNewVM.catalogSurveyId });
            return Ok();
        }

        //// PUT api/<QuestionController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<QuestionController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
