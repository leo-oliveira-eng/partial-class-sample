﻿using BaseEntity.Domain.UnitOfWork;
using Messages.Core;
using Messages.Core.Extensions;
using PartialClassSample.Api.Messages.RequestMessage;
using PartialClassSample.Api.Messages.ResponseMessage;
using PartialClassSample.Api.Models;
using PartialClassSample.Api.Models.Repositories;
using PartialClassSample.Api.Services.Contracts;
using PartialClassSample.Api.Services.Mappers;
using System;
using System.Threading.Tasks;

namespace PartialClassSample.Api.Services
{
    public class RegisterService : IRegisterService
    {
        IRegisterRepository RegisterRepository { get; }

        IUnitOfWork Uow { get; }
        public RegisterService(IRegisterRepository registerRepository, IUnitOfWork uow)
        {
            RegisterRepository = registerRepository ?? throw new ArgumentNullException(nameof(registerRepository));
            Uow = uow ?? throw new ArgumentNullException(nameof(uow));
        }

        public async Task<Response<RegisterResponseMessage>> CreateRegisterAsync(CreateRegisterRequestMessage requestMessage)
        {
            var response = Response<RegisterResponseMessage>.Create();

            if (requestMessage is null)
                return response.WithBusinessError(nameof(requestMessage), "Request message is invalid");

            var createRegisterResponse = Register.Create(requestMessage.LastName, requestMessage.FirstName, requestMessage.Email,
                requestMessage.Password, requestMessage.PasswordConfirmation);

            if (createRegisterResponse.HasError)
                return response.WithMessages(createRegisterResponse.Messages);

            await RegisterRepository.AddAsync(createRegisterResponse);

            if (!await Uow.CommitAsync())
                return response.WithCriticalError("Failed to save register");

            return createRegisterResponse.Data.Value.ToRegisterResponseMessage();
        }

        public async Task<Response<RegisterResponseMessage>> AuthenticateUserAsync(string email, string password)
        {
            var response = Response<RegisterResponseMessage>.Create();

            if (string.IsNullOrEmpty(password))
                return response.WithBusinessError(nameof(password), $"{nameof(password)} is required");

            var register = await RegisterRepository.FindAsync(email);

            if (!register.HasValue)
                return response.WithBusinessError(nameof(email), $"Email {email} not found");

            if (!register.Value.PasswordIsValid(password))
                return response.WithBusinessError("Authentication failed");

            return register.Value.ToRegisterResponseMessage();
        }
    }
}
