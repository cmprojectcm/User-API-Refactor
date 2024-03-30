using AutoMapper;
using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using Tests.User.Api.Controllers;
using Tests.User.Api.Dto;
using Tests.User.Api.Repositories;

namespace Tests.User.Api.Test
{
    
    public class UserControllerTests
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;


        public UserControllerTests(){
            
            _mapper = A.Fake<IMapper>();
            _userRepository = A.Fake<IUserRepository>();
        }

        [Fact]
        public async Task Should_Return_User_When_Valid_Id_Passed(){
            var userDto = new UserDto()
            {
                Id=1,
                FirstName="Test",
                LastName="test 2",
                Age="20"
            };

            UserController userController = new UserController(_mapper,_userRepository);
            await _userRepository.CreateUserAsync(userDto);
            IActionResult result = await userController.GetUser(userDto.Id);
            OkObjectResult ok = result as OkObjectResult;           
            Assert.Equal(200, ok.StatusCode);
            Assert.NotNull(result);
        } 
                
        [Fact]
        public async Task Should_Return_Valid_When_User_Created()
        {
            UserDto userDto = new UserDto()
            {
                Id=1,
                FirstName="Test",
                LastName="test 2",
                Age="20"
            };

            UserController userController = new UserController(_mapper,_userRepository);
            var result = await userController.CreateUser(userDto);
            // var test = GetType().GetProperty("StatusCode").GetValue(result);
            OkResult? ok = result as OkResult;
            Assert.NotNull(ok);
            Assert.Equal(200, ok.StatusCode);
        }

        [Fact]
        public async Task Should_Return_Valid_When_User_Updated()
        {
            UserDto userToAdd = new UserDto
            {
                FirstName = "Test",
                LastName = "User",
                Age = "20"
            };
            UserDto userToUpdate = new UserDto
            {
                Id=1,
                FirstName = "Test Update",
                LastName = "User Update",
                Age = "29"
            };
            await _userRepository.CreateUserAsync(userToAdd);

            UserController userController = new UserController(_mapper,_userRepository);
            IActionResult result = await userController.Update(userToUpdate);

            OkObjectResult ok = result as OkObjectResult;

            Assert.NotNull(ok);
            Assert.Equal(200, ok.StatusCode);
        }
        [Fact]
        public async Task Should_Return_Valid_When_User_Removed()
        {
            UserDto user = new UserDto
            {
                Id=1,
                FirstName = "Test",
                LastName = "User",
                Age = "20"
            };
            await _userRepository.CreateUserAsync(user);
            UserController userController = new UserController(_mapper,_userRepository);
            IActionResult result = await userController.Delete(user.Id);
            
            OkResult ok = result as OkResult;
            Assert.NotNull(ok);
            Assert.Equal(200, ok.StatusCode);
        }
    }
}