using FluentValidation;
using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Exceptions;

namespace MyRecipeBook.Application.UseCases.User.Register
{
    public class RegisterUserValidator : AbstractValidator<RequestRegisterUserJson>
    {
        public RegisterUserValidator()
        {
            ValidarNome();
            ValidarEmail();
            ValidarSenhaComLimiteCaracteres();
        }

        private void ValidarNome()
        {
            RuleFor(user => user.Name)
                .NotEmpty()
                .WithMessage(ResourceMessagesException.Nome_Obrigatorio);
        }

        private void ValidarEmail()
        {
            RuleFor(user => user.Email)
                .NotEmpty()
                .WithMessage(ResourceMessagesException.Email_Obrigatorio)
                .DependentRules(ValidarEmailValido);
        }

        private void ValidarEmailValido()
        {
            RuleFor(user => user.Email)
                .EmailAddress()
                .WithMessage(ResourceMessagesException.Email_Invalido);
        }

        private void ValidarSenhaComLimiteCaracteres()
        {
            RuleFor(user => user.Password.Length)
                .GreaterThan(6)
                .WithMessage(ResourceMessagesException.Senha_Limite_Caracteres);
        }
    }
}
