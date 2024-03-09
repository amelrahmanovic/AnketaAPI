using AnketaAPI.Models;
using AnketaAPI.Repository;
using AnketaAPI.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AnketaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserCatalogSurveryController : ControllerBase
    {
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<UserCatalogSurvery> _userCatalogSurvery;
        private readonly IRepository<CatalogSurveyQuestion> _catalogSurveyQuestion;
        private readonly IRepository<QuestionAnswer> _questionAnswer;
        private readonly IRepository<UserAnswer> _userAnswer;
        private MapperConfiguration config;
        private Mapper mapper;

        public UserCatalogSurveryController(
            IRepository<UserCatalogSurvery> userCatalogSurvery, 
            IRepository<User> userRepository, 
            IRepository<CatalogSurveyQuestion> catalogSurveyQuestion,
            IRepository<QuestionAnswer> questionAnswer,
            IRepository<UserAnswer> userAnswer
            )
        {
            _userCatalogSurvery = userCatalogSurvery;
            _userRepository = userRepository;
            _catalogSurveyQuestion = catalogSurveyQuestion;
            _questionAnswer = questionAnswer;
            _userAnswer = userAnswer;

            config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
            mapper = new Mapper(config);
        }
        //// GET: api/<UserCatalogSurveryController>
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET api/UserCatalogSurveryController/5
        [HttpGet("{id}")]
        public IEnumerable<UserCatalogSurveryVM> Get(int id)
        {
            List<UserCatalogSurvery> userCatalogSurverys = (List<UserCatalogSurvery>)_userCatalogSurvery.GetById_Custom(id);
            List<UserCatalogSurveryVM> catalogSurveyQuestionVMs = mapper.Map<List<UserCatalogSurveryVM>>(userCatalogSurverys);

            IEnumerable<CatalogSurveyQuestion> catalogSurveyQuestions = _catalogSurveyQuestion.GetById_Custom(id);
            List<UserQuestionAnswerVM> userQuestionAnswerVM = mapper.Map<List<UserQuestionAnswerVM>>(catalogSurveyQuestions);
            foreach (var item in userQuestionAnswerVM)
            {
                item.QuestionAnswerUserVM = mapper.Map<List<QuestionAnswerUserVM>>(_questionAnswer.GetById_Custom(item.QuestionId));
            }

            foreach (var item in catalogSurveyQuestionVMs)
            {
                item.UserQuestionAnswerVM = userQuestionAnswerVM;
            }
            foreach(var item in catalogSurveyQuestionVMs)
            {
                foreach (var userQuestionAnswerVMx in item.UserQuestionAnswerVM)
                {
                    foreach (var userAnswer in userQuestionAnswerVMx.QuestionAnswerUserVM)
                    {
                        UserAnswer? userAnswerResult = _userAnswer.GetById_Custom3(item.Id, userAnswer.Id);
                        if (userAnswerResult != null)
                        {
                            if (userAnswerResult.Answer)
                                userAnswer.UserAnswer = "true";
                            else
                                userAnswer.UserAnswer = "false";

                            if (userAnswerResult.Answer == userAnswer.IsCorrect)
                                item.SuccessfulAnswers++;
                            else
                                item.WrongAnswers++;
                        }
                        else
                        {
                            userAnswer.UserAnswer = "";
                            item.WrongAnswers++;
                        }
                    }
                }
            }

            return catalogSurveyQuestionVMs;
        }

        // POST api/<UserCatalogSurveryController>
        [HttpPost]
        public IActionResult Post(UserCatalogSurveryNewVM userCatalogSurveryNewVM)
        {
            User user = _userRepository.Add_Custom(new Models.User() { Email = userCatalogSurveryNewVM.Email });
            if (user.Id!=0)
            {
                bool result = _userCatalogSurvery.Add(new UserCatalogSurvery() { UserId = user.Id, CatalogSurveyId=userCatalogSurveryNewVM.CatalogSurveyId, Finished=false });
                return result ? Ok() : NotFound();
            }
            return NotFound();
        }

        //// PUT api/<UserCatalogSurveryController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        // DELETE api/UserCatalogSurveryController/5?catalogSurveyId=6
        [HttpDelete("{userId}")]
        public IActionResult Delete(int userId, [FromQuery] int catalogSurveyId)
        {
            var result = _userCatalogSurvery.Delete(userId, catalogSurveyId);
            return result ? Ok() : NotFound();
        }
    }
}
