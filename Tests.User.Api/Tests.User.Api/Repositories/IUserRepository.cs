using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tests.User.Api.Dto;

namespace Tests.User.Api.Repositories
{
    public interface IUserRepository
    {
        Task<Models.User?> GetUserByIdAsync(int id);

        Task<Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<Models.User>> CreateUserAsync(UserDto userDto);

        Task<Models.User> UpdateUserAsync(UserDto userDto);

        Task<Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<Models.User>> DeleteUser(int id);
    
        Task<int> SaveChangesAsyncUser();
    }
}