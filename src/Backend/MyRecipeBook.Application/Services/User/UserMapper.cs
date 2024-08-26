using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Domain.Entities;

namespace MyRecipeBook.Application.Services.User
{
    public class UserMapper : IUserMapper
    {
        public UserEntity ConvertRequestToUser(RequestRegisterUserJson request)
        {
            return new UserEntity
            {
                Name = request.Name,
                Email = request.Email
            };
        }
    }
}
