using PartialClassSample.Api.Messages.ResponseMessage;
using PartialClassSample.Api.Models;

namespace PartialClassSample.Api.Services.Mappers
{
    public static class RegisterMapper
    {
        public static RegisterResponseMessage ToRegisterResponseMessage(this Register register)
            => new RegisterResponseMessage
            {
                Id = register.Id,
                Name = $"{register.LastName}, {register.FirstName}",
                Email = register.Email
            };
    }
}
