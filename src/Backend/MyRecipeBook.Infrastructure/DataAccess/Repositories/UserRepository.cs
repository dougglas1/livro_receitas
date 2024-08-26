using Microsoft.EntityFrameworkCore;
using MyRecipeBook.Domain.Entities;
using MyRecipeBook.Domain.Repositories.User;

namespace MyRecipeBook.Infrastructure.DataAccess.Repositories
{
    public class UserRepository : IUserWriteOnlyRepository, IUserReadOnlyRepository
    {
        private readonly MyRecipeBookDbContext _context;

        public UserRepository(MyRecipeBookDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(UserEntity user)
        {
            await _context.Users.AddAsync(user);
        }

        public async Task<bool> ExistActiveUserWithEmailAsync(string email)
        {
            return await _context.Users.AnyAsync(u => u.Email == email && u.Active);
        }
    }
}
