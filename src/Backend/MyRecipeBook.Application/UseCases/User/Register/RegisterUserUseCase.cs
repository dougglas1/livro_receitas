using MyRecipeBook.Application.Services.Cryptography;
using MyRecipeBook.Application.Services.User;
using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Communication.Responses;
using MyRecipeBook.Domain.Repositories;
using MyRecipeBook.Domain.Repositories.User;
using MyRecipeBook.Exceptions;
using MyRecipeBook.Exceptions.ExceptionsBase;

namespace MyRecipeBook.Application.UseCases.User.Register
{
    public class RegisterUserUseCase : IRegisterUserUseCase
    {
        private readonly IUserWriteOnlyRepository _userWriteOnlyRepository;
        private readonly IUserReadOnlyRepository _userReadOnlyRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserMapper _userMapper;
        private readonly PasswordEncripter _passwordEncripter;

        public RegisterUserUseCase(
            IUserWriteOnlyRepository userWriteOnlyRepository,
            IUserReadOnlyRepository userReadOnlyRepository,
            IUnitOfWork unitOfWork,
            IUserMapper userMapper,
            PasswordEncripter passwordEncripter)
        {
            _userWriteOnlyRepository = userWriteOnlyRepository;
            _userReadOnlyRepository = userReadOnlyRepository;
            _unitOfWork = unitOfWork;
            _userMapper = userMapper;
            _passwordEncripter = passwordEncripter;
        }

        public async Task<ResponseRegisteredUserJson> ExecuteAsync(RequestRegisterUserJson request)
        {
            await ValidateAsync(request);

            var user = _userMapper.ConvertRequestToUser(request);
            user.AddPassword(_passwordEncripter.Encrypt(request.Password));
            
            await _userWriteOnlyRepository.AddAsync(user);
            
            await _unitOfWork.Commit();
            
            return new ResponseRegisteredUserJson
            {
                Name = request.Name,
            };
        }

        private async Task ValidateAsync(RequestRegisterUserJson request)
        {
            var validator = new RegisterUserValidator();
            var result = validator.Validate(request);

            var emailExist = await _userReadOnlyRepository.ExistActiveUserWithEmailAsync(request.Email);
            if (emailExist)
            {
                result.Errors.Add(new FluentValidation.Results.ValidationFailure(string.Empty, ResourceMessagesException.Email_Ja_Existente));
            }

            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();

                throw new ErrorOnValidationException(errorMessages);
            }
        }
    }
}
