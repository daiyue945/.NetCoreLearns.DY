using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserMgr.Domain;
using UserMgr.Domain.Entities;
using UserMgr.Infrastracture;

namespace UserMgr.WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CRUDController : ControllerBase
    {
        private readonly UserDbContext _userDbContext;
        private readonly IUserRepository _userRepository;
        public CRUDController(UserDbContext userDbContext, IUserRepository userRepository)
        {
            this._userDbContext = userDbContext;
            this._userRepository = userRepository;
        }
        [HttpPost]
        [UintOfWork(typeof(UserDbContext))]
        public async Task<ActionResult> AddNewUser(AddUserRequest addUserRequest)
        {
            var user = await _userRepository.FindOneAsync(addUserRequest.phoneNumber);
            if (user != null)
            {
                return BadRequest("手机号已存在");
            }
            var userNew = new User(addUserRequest.phoneNumber);
            userNew.ChangePassWord(addUserRequest.password);
            _userDbContext.Users.Add(userNew);
            return Ok("创建完成");
        }
    }
}
