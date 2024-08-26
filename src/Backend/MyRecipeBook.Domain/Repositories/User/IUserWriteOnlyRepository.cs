using MyRecipeBook.Domain.Entities;

namespace MyRecipeBook.Domain.Repositories.User
{
    public interface IUserWriteOnlyRepository
    {
        public Task AddAsync(UserEntity user);
    }
}
