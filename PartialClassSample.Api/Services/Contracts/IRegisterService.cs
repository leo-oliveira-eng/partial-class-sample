using Messages.Core;
using PartialClassSample.Api.Messages.RequestMessage;
using PartialClassSample.Api.Messages.ResponseMessage;
using System.Threading.Tasks;

namespace PartialClassSample.Api.Services.Contracts
{
    public interface IRegisterService
    {
        Task<Response<RegisterResponseMessage>> CreateRegisterAsync(CreateRegisterRequestMessage requestMessage);

        Task<Response<RegisterResponseMessage>> AuthenticateUserAsync(AuthenticateUserRequestMessage requestMessage);
    }
}
