using BaseEntity.Domain.UnitOfWork;
using Moq;
using PartialClassSample.Api.Models.Repositories;
using PartialClassSample.Api.Tests.Shared;

namespace PartialClassSample.Api.Tests.RegisterService
{
    public class RegisterServiceUnitTests : BaseMock
    {
        protected readonly Mock<IUnitOfWork> _uow = new Mock<IUnitOfWork>();

        protected readonly Mock<IRegisterRepository> _registerRepository = new Mock<IRegisterRepository>();

        protected Services.RegisterService RegisterService { get; set; }

        public RegisterServiceUnitTests()
        {
            BeforeCreateService();

            RegisterService = new Services.RegisterService(_registerRepository.Object, _uow.Object);
        }

        protected virtual void BeforeCreateService() { }
    }
}
