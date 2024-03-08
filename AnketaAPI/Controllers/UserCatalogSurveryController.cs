using AnketaAPI.Models;
using AnketaAPI.Repository;
using AnketaAPI.ViewModels;
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
        public UserCatalogSurveryController(IRepository<UserCatalogSurvery> userCatalogSurvery, IRepository<User> userRepository)
        {
            _userCatalogSurvery = userCatalogSurvery;
            _userRepository = userRepository;
        }
        //// GET: api/<UserCatalogSurveryController>
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET api/<UserCatalogSurveryController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

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

        //// DELETE api/<UserCatalogSurveryController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
