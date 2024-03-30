using Microsoft.AspNetCore.Mvc;
using Notes.Backend.Domain.Dtos;
using Notes.Backend.Domain.Interfaces.Services;
using Skreet2k.Common.Models;

namespace Notes.Backend.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _service;

        public UsersController(IUserService usersService)
        {
            _service = usersService;
        }

        [HttpGet]
        public async Task<ActionResult<ResultList<UserDto>>> GetAllUsers(CancellationToken cancellationToken)
        {
            var result = await _service.GetAllUsersAsync(cancellationToken);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Result<UserDto>>> GetUserById(int id, CancellationToken cancellationToken)
        {
            var result = await _service.GetUserByIdAsync(id, cancellationToken);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<Result<UserDto>>> CreateUser([FromBody] UserUpsertDto userDto, CancellationToken cancellationToken)
        {
            var result = await _service.CreateUserAsync(userDto, cancellationToken);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Result<UserDto>>> UpdateUser(int id, [FromBody] UserUpsertDto userDto, CancellationToken cancellationToken)
        {
            var result = await _service.UpdateUserAsync(id, userDto, cancellationToken);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Result>> DeleteUser(int id, CancellationToken cancellationToken)
        {
            var result = await _service.RemoveUserAsync(id, cancellationToken);
            return Ok(result);
        }

        [HttpGet("{name}/{password}")]
        public async Task<ActionResult<Result<UserDto>>> GetUserByUserNameAndPassword(string name, string password, CancellationToken cancellationToken)
        {
            var result = await _service.GetUserByPasswordAndUserName(name, password, cancellationToken);
            return Ok(result);
        }
    }
}
