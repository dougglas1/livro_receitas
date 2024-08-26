using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyRecipeBook.Application.Services.Cryptography;
using MyRecipeBook.Application.Services.User;
using MyRecipeBook.Application.UseCases.User.Register;

namespace MyRecipeBook.Application
{
    public static class DependencyInjectionExtension
    {
        public static void AddApplication(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            AddPasswordEncripter(serviceCollection, configuration);
            AddMapper(serviceCollection);
            AddUseCases(serviceCollection);
        }

        private static void AddMapper(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IUserMapper, UserMapper>();
        }

        private static void AddUseCases(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();
        }

        private static void AddPasswordEncripter(IServiceCollection serviceColletion, IConfiguration configuration)
        {
            var additionalKey = configuration.GetValue<string>("Settings:Password:AdditionalKey");

            // Não utilizado Interface por ser apenas utilizado dentro do próprio Application, diferente de UseCase que seria usado na API.
            serviceColletion.AddScoped(option => new PasswordEncripter(additionalKey!));
        }
    }
}
