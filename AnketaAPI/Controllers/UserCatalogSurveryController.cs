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

        // GET api/UserCatalogSurvery/5
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
            foreach (var item in catalogSurveyQuestionVMs)
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
                            userAnswer.UserAnswer = "None";
                            item.WrongAnswers++;
                        }
                    }
                }
            }

            return catalogSurveyQuestionVMs;
        }

        // GET api/UserCatalogSurvery/user/5
        [HttpGet("user/{userid}")]
        public IEnumerable<UserTestsVM> Get2(int userid)
        {
            IEnumerable<UserCatalogSurvery> result = _userCatalogSurvery.GetById_Custom4(userid);
            List<UserTestsVM> UserTestsVMs = mapper.Map<List<UserTestsVM>>(result); //CatalogSurvey===>catalogSurveyId, catalogSurveyName, catalogSurveyCreated

            foreach (var item in UserTestsVMs)//questionId, questionName
            {
                item.TestDone = "";
                IEnumerable<CatalogSurveyQuestion> catalogSurveyQuestions = _catalogSurveyQuestion.GetById_Custom(item.CatalogSurveyId);
                List<UserQuestionAnswerVM> userQuestionAnswerVM = mapper.Map<List<UserQuestionAnswerVM>>(catalogSurveyQuestions);

                foreach (var item2 in userQuestionAnswerVM)
                {
                    item2.QuestionAnswerUserVM = mapper.Map<List<QuestionAnswerUserVM>>(_questionAnswer.GetById_Custom(item2.QuestionId));
                    foreach (var item3 in item2.QuestionAnswerUserVM)
                    {
                        item3.UserAnswer = "None";
                    }
                }
                item.userQuestionAnswerVMs = userQuestionAnswerVM;
            }


            return UserTestsVMs;
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

        [HttpPost("user")]
        public IActionResult Post2([FromBody] List<UserTestsVM> UserTestsVMs)
        {
            foreach (var item in UserTestsVMs)
            {
                if (item.TestDone == "true")
                {
                    foreach (var item2 in item.userQuestionAnswerVMs)
                    {
                        foreach (var item3 in item2.QuestionAnswerUserVM)
                        {
                            if(item3.UserAnswer=="true")
                                _userAnswer.Add(new UserAnswer() { Answer = true, QuestionAnswerId = item3.Id, UserId = (int)item.UserId });
                            if (item3.UserAnswer=="false")
                                _userAnswer.Add(new UserAnswer() { Answer = false, QuestionAnswerId = item3.Id, UserId = (int)item.UserId });
                            //if (item3.UserAnswer=="None")
                                        
                        }
                    }
                    //disable test
                    _userCatalogSurvery.Delete(new UserCatalogSurvery() { CatalogSurveyId = item.CatalogSurveyId, UserId = (int)item.UserId });
                }
            }
            return Ok();
        }

        //// PUT api/<UserCatalogSurveryController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        // DELETE api/UserCatalogSurvery/5?catalogSurveyId=6
        [HttpDelete("{userId}")]
        public IActionResult Delete(int userId, [FromQuery] int catalogSurveyId)
        {
            var result = _userCatalogSurvery.Delete(userId, catalogSurveyId);
            return result ? Ok() : NotFound();
        }
    }
}
