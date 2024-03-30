using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tests.User.Api.Dto;
using Tests.User.Api.Models;
using Tests.User.Api.Repositories;

namespace Tests.User.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IMapper _mapper;

        private readonly IUserRepository _userRepository;
        public UserController (IMapper mapper, IUserRepository userRepository){
            _mapper=mapper;
            _userRepository = userRepository;
        }
        /// <summary>
        ///     Gets a user
        /// </summary>
        /// <param name="id">ID of the user</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetUser(int id )
        {
            var user = _userRepository.GetUserByIdAsync(id);
            var userDto = _mapper.Map<UserDto>(user.Result);
            if (userDto != null){
                return Ok(userDto);
            } else {
                return NotFound("Specified User Not Found");
            }
        }


        /// <summary>
        ///     Create a new user
        /// </summary>
        /// <param name="firstName">First name of the user</param>
        /// <param name="lastName">Last name of the user</param>
        /// <param name="age">Age of the user (must be a number)</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateUser(UserDto userDto)
        {
            await _userRepository.CreateUserAsync(userDto);
            await _userRepository.SaveChangesAsyncUser();
            return Ok();
        }

        /// <summary>
        ///     Updates a user
        /// </summary>
        /// <param name="id">ID of the user</param>
        /// <param name="firstName">First name of the user</param>
        /// <param name="lastName">Last name of the user</param>
        /// <param name="age">Age of the user (must be a number)</param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Update(UserDto userDto)
        {
            var user = _userRepository.UpdateUserAsync(userDto);
            
            if (user.Result != null){
                await _userRepository.SaveChangesAsyncUser();
                return Ok(_mapper.Map<UserDto>(user.Result));
            } else {
                return NotFound("Specified User Not found");
            }
        }

        /// <summary>
        ///     Delets a user
        /// </summary>
        /// <param name="id">ID of the user</param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var user = _userRepository.DeleteUser(id);
            
            if (user.Result!=null){
                await _userRepository.SaveChangesAsyncUser();
                return Ok();
            } else {
                return NotFound("Specified User Not found");
            }
            
        }
    }
}
