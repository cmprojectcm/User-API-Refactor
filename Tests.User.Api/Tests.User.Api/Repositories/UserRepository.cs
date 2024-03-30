using AutoMapper;
using Tests.User.Api.Dto;

namespace Tests.User.Api.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DatabaseContext _dbContext;
        private readonly IMapper _mapper;
        public UserRepository(DatabaseContext dbContext, IMapper mapper){
            _dbContext = dbContext;
            _mapper =mapper;
        }

        public async Task<Models.User?> GetUserByIdAsync(int id){
            return await _dbContext.Set<Models.User>().FindAsync(id);
        }

        public async Task<Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<Models.User>> CreateUserAsync(UserDto userDto){
            return await _dbContext.Users.AddAsync(_mapper.Map<Models.User>(userDto));
        }

        public async Task<Models.User?> UpdateUserAsync(UserDto userDto){
            var user = await _dbContext.FindAsync<Models.User>(userDto.Id);
            _mapper.Map(userDto, user);
            return user;
        }

        public async Task<Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<Models.User>> DeleteUser(int id){
            var user = await _dbContext.FindAsync<Models.User>(id);
            
            return _dbContext.Users.Remove(user);
        }

        public async Task<int> SaveChangesAsyncUser() {
            return await _dbContext.SaveChangesAsync();
        }
    }
}