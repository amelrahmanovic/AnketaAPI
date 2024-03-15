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
    public class UserController : ControllerBase
    {
        private readonly IRepository<User> _userRepository;
        private MapperConfiguration config;
        private Mapper mapper;

        public UserController(IRepository<User> userRepository)
        {
            _userRepository = userRepository;

            config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
            mapper = new Mapper(config);
        }
        // GET: api/User
        [HttpGet]
        public IEnumerable<UserVM> Get()
        {
            return mapper.Map<List<UserVM>>(_userRepository.GetAll());
        }

        // GET api/User/example@example.com
        [HttpGet("{email}")]
        public ActionResult<UserVM> Get(string email)
        {
            return mapper.Map<UserVM>(_userRepository.GetAll().SingleOrDefault(x => x.Email == email));
        }

        //// POST api/<UserController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/<UserController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<UserController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
