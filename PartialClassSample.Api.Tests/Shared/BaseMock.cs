using FizzWare.NBuilder;
using PartialClassSample.Api.Messages.RequestMessage;
using PartialClassSample.Api.Models.Extensions;
using Model = PartialClassSample.Api.Models;

namespace PartialClassSample.Api.Tests.Shared
{
    public class BaseMock
    {
        public Model.Register RegisterFake(string lastName = null, string firstName = null, string email = null, string passWord = null)
            => Builder<Model.Register>.CreateNew()
                .With(_ => _.Id, 1)
                .With(_ => _.LastName, lastName ?? "None")
                .With(_ => _.FirstName, firstName ?? "No Name")
                .With(_ => _.Email, email ?? "any@nothing.com")
                .With(_ => _.PassWord, passWord?.Encrypt() ?? "1234".Encrypt())
                .Build();

        public CreateRegisterRequestMessage CreateRegisterRequestMessageFake(string lastName = null, string firstName = null, string email = null, string passWord = null, 
            string confirmationPassword = null)
            => Builder<CreateRegisterRequestMessage>.CreateNew()
                .With(_ => _.LastName, lastName ?? "None")
                .With(_ => _.FirstName, firstName ?? "No Name")
                .With(_ => _.Email, email ?? "any@nothing.com")
                .With(_ => _.Password, passWord ?? "1234")
                .With(_ => _.PasswordConfirmation, confirmationPassword ?? "1234")
                .Build();

        public AuthenticateUserRequestMessage AuthenticateUserRequestMessageFake(string email = null, string password = null)
            => new AuthenticateUserRequestMessage
            {
                Email = email ?? "any@nothing.com",
                Password = password ?? "1234"
            };
    }
}
