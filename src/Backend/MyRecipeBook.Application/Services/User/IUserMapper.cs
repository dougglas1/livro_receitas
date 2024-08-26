using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Domain.Entities;

namespace MyRecipeBook.Application.Services.User
{
    public interface IUserMapper
    {
        UserEntity ConvertRequestToUser(RequestRegisterUserJson request);
    }
}
