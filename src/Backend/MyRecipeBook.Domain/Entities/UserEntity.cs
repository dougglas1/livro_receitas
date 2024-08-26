﻿namespace MyRecipeBook.Domain.Entities
{
    public class UserEntity : EntityBase
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        public UserEntity AddPassword(string password)
        {
            Password = password;
            return this;
        }
    }
}
