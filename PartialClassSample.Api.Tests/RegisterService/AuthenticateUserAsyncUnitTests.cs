using FluentAssertions;
using Messages.Core;
using Messages.Core.Enums;
using Moq;
using PartialClassSample.Api.Messages.RequestMessage;
using PartialClassSample.Api.Messages.ResponseMessage;
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
            var requestMessage = AuthenticateUserRequestMessageFake(password: string.Empty);

            var response = await RegisterService.AuthenticateUserAsync(requestMessage);

            response.Should().NotBeNull();
            response.HasError.Should().BeTrue();
            response.Messages.Should().HaveCount(1);
            response.Messages.Should().Contain(message => message.Property.Equals("Password"));
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

            var requestMessage = AuthenticateUserRequestMessageFake();

            var response = await RegisterService.AuthenticateUserAsync(requestMessage);

            response.Should().NotBeNull();
            response.HasError.Should().BeTrue();
            response.Messages.Should().HaveCount(1);
            response.Messages.Should().Contain(message => message.Property.Equals("Email"));
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

            var requestMessage = AuthenticateUserRequestMessageFake(password: "abc123");

            var response = await RegisterService.AuthenticateUserAsync(requestMessage);

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

            var requestMessage = AuthenticateUserRequestMessageFake();

            var response = await RegisterService.AuthenticateUserAsync(requestMessage);

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
