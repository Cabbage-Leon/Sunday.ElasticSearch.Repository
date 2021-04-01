using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sunday.ElasticSearch.Repository.Api.Data;

namespace Sunday.ElasticSearch.Repository.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<User>> GetUserListAsync(List<string> userIds)
        {
            return await _userRepository.GetUserList(userIds);
        }
    }
}