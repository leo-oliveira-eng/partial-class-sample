using Messages.Core;
using Messages.Core.Extensions;
using PartialClassSample.Api.Models.Extensions;
using System;
using System.Diagnostics.CodeAnalysis;

namespace PartialClassSample.Api.Models
{
    public partial class Register
    {
        [Obsolete("Only for Entity Framework", true), ExcludeFromCodeCoverage]
        private Register() { }

        private Register(string lastName, string firstName, string email, string passWord)
        {
            LastName = lastName;
            FirstName = firstName;
            Email = email;
            PassWord = passWord;
        }

        public static Response<Register> Create(string lastName, string firstName, string email, string passWord, string passWordConfirmation)
        {
            var response = Response<Register>.Create();

            var dataIsValidResponse = IsValidForUser(lastName, firstName, email, passWord, passWordConfirmation);

            if (dataIsValidResponse.HasError)
                return response.WithMessages(dataIsValidResponse.Messages);

            var encryptedPassword = passWord.Encrypt();

            return new Register(lastName, firstName, email, encryptedPassword);
        }

        private static Response IsValidForUser(string lastName, string firstName, string email, string passWord, string passWordConfirmation)
        {
            var response = Response.Create();

            if (string.IsNullOrEmpty(lastName))
                response.WithBusinessError(nameof(lastName), $"{nameof(lastName)} is invalid or missing");

            if (string.IsNullOrEmpty(firstName))
                response.WithBusinessError(nameof(firstName), $"{nameof(firstName)} is invalid or missing");

            if (string.IsNullOrEmpty(email))
                response.WithBusinessError(nameof(email), $"{nameof(email)} is invalid or missing");

            if (string.IsNullOrEmpty(passWord))
                response.WithBusinessError(nameof(passWord), $"{nameof(passWord)} is invalid or missing");

            if (string.IsNullOrEmpty(passWordConfirmation))
                response.WithBusinessError(nameof(passWordConfirmation), $"{nameof(passWordConfirmation)} is invalid or missing");

            if (passWord != passWordConfirmation)
                response.WithBusinessError(nameof(passWordConfirmation), "password don't match");

            return response;
        }
    }
}
