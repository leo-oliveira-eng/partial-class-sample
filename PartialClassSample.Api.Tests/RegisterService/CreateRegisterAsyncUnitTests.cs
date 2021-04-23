using FluentAssertions;
using Messages.Core.Enums;
using Moq;
using PartialClassSample.Api.Messages.ResponseMessage;
using System.Threading.Tasks;
using Xunit;

namespace PartialClassSample.Api.Tests.RegisterService
{
    public class CreateRegisterAsyncUnitTests : RegisterServiceUnitTests
    {
        [Fact]
        public async Task CreateRegisterAsync_ShouldReturnResponseWithBusinessError_RequestMessageIsNull()
        {
            _registerRepository.Setup(_ => _.AddAsync(It.IsAny<Models.Register>()));

            _uow.Setup(_ => _.CommitAsync());

            var response = await RegisterService.CreateRegisterAsync(null);

            response.Should().NotBeNull();
            response.HasError.Should().BeTrue();
            response.Messages.Should().HaveCount(1);
            response.Messages.Should().Contain(message => message.Property.Equals("requestMessage"));
            response.Data.HasValue.Should().BeFalse();
            response.Data.Value.Should().BeNull();
            _registerRepository.Verify(_ => _.AddAsync(It.IsAny<Models.Register>()), Times.Never);
            _uow.Verify(_ => _.CommitAsync(), Times.Never);
        }

        [Fact]
        public async Task CreateRegisterAsync_ShouldReturnResponseWithBusinessError_FirstNameIsEmpty()
        {
            _registerRepository.Setup(_ => _.AddAsync(It.IsAny<Models.Register>()));
            _uow.Setup(_ => _.CommitAsync());
            var requestMessage = CreateRegisterRequestMessageFake(firstName: string.Empty);

            var response = await RegisterService.CreateRegisterAsync(requestMessage);

            response.Should().NotBeNull();
            response.HasError.Should().BeTrue();
            response.Messages.Should().HaveCount(1);
            response.Messages.Should().Contain(message => message.Property.Equals("firstName"));
            response.Data.HasValue.Should().BeFalse();
            response.Data.Value.Should().BeNull();
            _registerRepository.Verify(_ => _.AddAsync(It.IsAny<Models.Register>()), Times.Never);
            _uow.Verify(_ => _.CommitAsync(), Times.Never);
        }

        [Fact]
        public async Task CreateRegisterAsync_ShouldReturnResponseWithCriticalError_CommitFailed()
        {
            _registerRepository.Setup(_ => _.AddAsync(It.IsAny<Models.Register>())).Verifiable();
            _uow.Setup(_ => _.CommitAsync()).ReturnsAsync(false).Verifiable();
            var requestMessage = CreateRegisterRequestMessageFake();

            var response = await RegisterService.CreateRegisterAsync(requestMessage);

            response.Should().NotBeNull();
            response.HasError.Should().BeTrue();
            response.Messages.Should().HaveCount(1);
            response.Messages.Should().Contain(message => message.Type.Equals(MessageType.CriticalError));
            response.Data.HasValue.Should().BeFalse();
            response.Data.Value.Should().BeNull();
            _registerRepository.Verify();
            _uow.Verify();
        }

        [Fact]
        public async Task CreateRegisterAsync_ShouldReturnSuccess()
        {
            _registerRepository.Setup(_ => _.AddAsync(It.IsAny<Models.Register>())).Verifiable();
            _uow.Setup(_ => _.CommitAsync()).ReturnsAsync(true).Verifiable();
            var requestMessage = CreateRegisterRequestMessageFake();

            var response = await RegisterService.CreateRegisterAsync(requestMessage);

            response.Should().NotBeNull();
            response.HasError.Should().BeFalse();
            response.Messages.Should().HaveCount(0);
            response.Data.HasValue.Should().BeTrue();
            response.Data.Value.Should().BeOfType(typeof(RegisterResponseMessage));
            _registerRepository.Verify();
            _uow.Verify();
        }
    }
}
