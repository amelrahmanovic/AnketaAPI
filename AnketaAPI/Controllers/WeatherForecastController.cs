using AnketaAPI.DataAccessObject;
using AnketaAPI.Models;
using AnketaAPI.Repository;
using Microsoft.AspNetCore.Mvc;

namespace AnketaAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IRepository<User> _userRepository;

        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };
        IConfiguration config;

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IConfiguration config, IRepository<User> userRepository)
        {
            _logger = logger;
            this.config = config;
            _userRepository = userRepository;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            UserDAO userDAO = new UserDAO(config);
            //var x = userDAO.Add(new Models.User() { Email = "amelrahmanovic@hotmail.com" });
            //var x = _userRepository.Add(new Models.User() { Email = "amelrahmanovic@hotmail.com2" });

            CatalogSurveyDAO catalogSurveyDAO = new CatalogSurveyDAO(config);
            //var x2 = catalogSurveyDAO.Add(new Models.CatalogSurvey() { Name = "Test" });

            QuestionDAO questionDAO = new QuestionDAO(config);
            //var x3 = questionDAO.Add(new Models.Question() { Name = "Test question" });

            UserCatalogSurveryDAO userCatalogSurveryDAO = new UserCatalogSurveryDAO(config);
            //userCatalogSurveryDAO.Add(new Models.UserCatalogSurvery() { UserId = userDAO.Get(1).Id, CatalogSurveyId = catalogSurveyDAO.Get(1).Id });

            QuestionAnswerDAO questionAnswerDAO = new QuestionAnswerDAO(config);
            //questionAnswerDAO.Add(new Models.QuestionAnswer() { QuestionId = 1, IsCorrect = true, Name = "Jabuka je voce?" });

            UserAnswerDAO userAnswerDAO = new UserAnswerDAO(config);
            //userAnswerDAO.Add(new UserAnswer() { Answer = true, UserId = 1, QuestionAnswerId = 1 });


            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}