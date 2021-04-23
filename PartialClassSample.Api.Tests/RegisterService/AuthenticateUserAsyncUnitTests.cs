using FluentAssertions;
using Messages.Core;
using Messages.Core.Enums;
using Moq;
using PartialClassSample.Api.Messages.ResponseMessage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PartialClassSample.Api.Tests.RegisterService
{
    public class AuthenticateUserAsyncUnitTests : RegisterServiceUnitTests
    {
        protected override void BeforeCreateService()
        {
            _uow.Setup(_ => _.CommitAsync());
        }

        [Fact]
        public async Task AuthenticateAsync_ShouldReturnBusinessError_PasswordIsEmpty()
        {
            _registerRepository.Setup(_ => _.FindAsync(It.IsAny<string>()));
            var email = "none@nothing.com";

            var response = await RegisterService.AuthenticateUserAsync(email, string.Empty);

            response.Should().NotBeNull();
            response.HasError.Should().BeTrue();
            response.Messages.Should().HaveCount(1);
            response.Messages.Should().Contain(message => message.Property.Equals("password"));
            response.Data.HasValue.Should().BeFalse();
            response.Data.Value.Should().BeNull();
            _registerRepository.Verify(_ => _.AddAsync(It.IsAny<Models.Register>()), Times.Never);
            _uow.Verify(_ => _.CommitAsync(), Times.Never);
        }

        [Fact]
        public async Task AuthenticateAsync_ShouldReturnBusinessError_RegisterNotFound()
        {
            _registerRepository.Setup(_ => _.FindAsync(It.IsAny<string>()))
                .ReturnsAsync(Maybe<Models.Register>.Create())
                .Verifiable();

            var email = "none@nothing.com";

            var response = await RegisterService.AuthenticateUserAsync(email, "1234");

            response.Should().NotBeNull();
            response.HasError.Should().BeTrue();
            response.Messages.Should().HaveCount(1);
            response.Messages.Should().Contain(message => message.Property.Equals("email"));
            response.Data.HasValue.Should().BeFalse();
            response.Data.Value.Should().BeNull();
            _registerRepository.Verify();
            _uow.Verify(_ => _.CommitAsync(), Times.Never);
        }

        [Fact]
        public async Task AuthenticateAsync_ShouldReturnBusinessError_InvalidPassword()
        {
            _registerRepository.Setup(_ => _.FindAsync(It.IsAny<string>()))
                .ReturnsAsync(Maybe<Models.Register>.Create(RegisterFake()))
                .Verifiable();

            var email = "none@nothing.com";

            var response = await RegisterService.AuthenticateUserAsync(email, "abc123");

            response.Should().NotBeNull();
            response.HasError.Should().BeTrue();
            response.Messages.Should().HaveCount(1);
            response.Messages.Should().Contain(message => message.Type.Equals(MessageType.BusinessError));
            response.Data.HasValue.Should().BeFalse();
            response.Data.Value.Should().BeNull();
            _registerRepository.Verify();
            _uow.Verify(_ => _.CommitAsync(), Times.Never);
        }

        [Fact]
        public async Task AuthenticateAsync_ShouldReturnSuccess()
        {
            _registerRepository.Setup(_ => _.FindAsync(It.IsAny<string>()))
                .ReturnsAsync(Maybe<Models.Register>.Create(RegisterFake()))
                .Verifiable();

            var email = "none@nothing.com";

            var response = await RegisterService.AuthenticateUserAsync(email, "1234");

            response.Should().NotBeNull();
            response.HasError.Should().BeFalse();
            response.Messages.Should().HaveCount(0);
            response.Data.HasValue.Should().BeTrue();
            response.Data.Value.Should().BeOfType(typeof(RegisterResponseMessage));
            _registerRepository.Verify();
            _uow.Verify(_ => _.CommitAsync(), Times.Never);
        }
    }
}
