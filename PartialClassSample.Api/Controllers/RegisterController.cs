using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using PartialClassSample.Api.Messages.RequestMessage;
using PartialClassSample.Api.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MVC = Microsoft.AspNetCore.Mvc;

namespace PartialClassSample.Api.Controllers
{
    [ApiController, MVC.Route("api/[controller]")]
    public class RegisterController : Default.BaseController
    {
        IRegisterService RegisterService { get; }

        public RegisterController(IRegisterService registerService)
        {
            RegisterService = registerService ?? throw new ArgumentNullException(nameof(registerService));
        }

        [HttpPost, MVC.Route("")]
        public async Task<IActionResult> CreateRegisterAsync([FromBody] CreateRegisterRequestMessage requestMessage)
            => await WithResponseAsync(() => RegisterService.CreateRegisterAsync(requestMessage));

        [HttpPost, MVC.Route("Login")]
        public async Task<IActionResult> CreateRegisterAsync([FromBody] AuthenticateUserRequestMessage requestMessage)
            => await WithResponseAsync(() => RegisterService.AuthenticateUserAsync(requestMessage));
    }
}
