using FluentAssertions;
using PartialClassSample.Api.Models.Extensions;
using PartialClassSample.Api.Tests.Shared;
using Xunit;
using Model = PartialClassSample.Api.Models;

namespace PartialClassSample.Api.Tests.Register
{
    public class CreateUnitTests : BaseMock
    {
        readonly string _firstName = "Any First Name";
        readonly string _lastName = "Any Last Name";
        readonly string _email = "any@anything.com";
        readonly string _password = "1234";
        readonly string _confirmationPassword = "1234";

        [Fact]
        public void Create_ShouldReturnResponseWithValidRegister()
        {
            var response = Model.Register.Create(_lastName, _firstName, _email, _password, _confirmationPassword);

            response.Should().NotBeNull();
            response.HasError.Should().BeFalse();
            response.Messages.Should().BeEmpty();
            response.Data.HasValue.Should().BeTrue();
            response.Data.Value.Should().BeOfType(typeof(Model.Register));
            response.Data.Value.Email.Should().Be(_email);
            response.Data.Value.PassWord.Should().NotBe(_password);
            response.Data.Value.PassWord.Should().Be(_password.Encrypt());
        }

        [Fact]
        public void Create_ShouldReturnErrorResponse_LastNameIsEmpty()
        {
            var response = Model.Register.Create(string.Empty, _firstName, _email, _password, _confirmationPassword);

            response.Should().NotBeNull();
            response.HasError.Should().BeTrue();
            response.Messages.Should().HaveCount(1);
            response.Messages.Should().Contain(message => message.Property.Equals("lastName"));
            response.Data.HasValue.Should().BeFalse();
            response.Data.Value.Should().BeNull();
        }

        [Fact]
        public void Create_ShouldReturnErrorResponse_FirstNameIsEmpty()
        {
            var response = Model.Register.Create(_lastName, string.Empty, _email, _password, _confirmationPassword);

            response.Should().NotBeNull();
            response.HasError.Should().BeTrue();
            response.Messages.Should().HaveCount(1);
            response.Messages.Should().Contain(message => message.Property.Equals("firstName"));
            response.Data.HasValue.Should().BeFalse();
            response.Data.Value.Should().BeNull();
        }

        [Fact]
        public void Create_ShouldReturnErrorResponse_emailIsEmpty()
        {
            var response = Model.Register.Create(_lastName, _firstName, string.Empty, _password, _confirmationPassword);

            response.Should().NotBeNull();
            response.HasError.Should().BeTrue();
            response.Messages.Should().HaveCount(1);
            response.Messages.Should().Contain(message => message.Property.Equals("email"));
            response.Data.HasValue.Should().BeFalse();
            response.Data.Value.Should().BeNull();
        }

        [Fact]
        public void Create_ShouldReturnErrorResponse_passwordIsEmpty()
        {
            var response = Model.Register.Create(_lastName, _firstName, _email, string.Empty, _confirmationPassword);

            response.Should().NotBeNull();
            response.HasError.Should().BeTrue();
            response.Messages.Should().HaveCount(2);
            response.Messages.Should().Contain(message => message.Property.Equals("passWord"));
            response.Data.HasValue.Should().BeFalse();
            response.Data.Value.Should().BeNull();
        }

        [Fact]
        public void Create_ShouldReturnErrorResponse_confirmationPasswordIsEmpty()
        {
            var response = Model.Register.Create(_lastName, _firstName, _email, _password, string.Empty);

            response.Should().NotBeNull();
            response.HasError.Should().BeTrue();
            response.Messages.Should().HaveCount(2);
            response.Messages.Should().Contain(message => message.Property.Equals("passWordConfirmation"));
            response.Data.HasValue.Should().BeFalse();
            response.Data.Value.Should().BeNull();
        }

        [Fact]
        public void Create_ShouldReturnErrorResponse_passwordDoNotMatch()
        {
            var response = Model.Register.Create(_lastName, _firstName, _email, _password, "abc123");

            response.Should().NotBeNull();
            response.HasError.Should().BeTrue();
            response.Messages.Should().HaveCount(1);
            response.Messages.Should().Contain(message => message.Property.Equals("passWordConfirmation"));
            response.Data.HasValue.Should().BeFalse();
            response.Data.Value.Should().BeNull();
        }
    }
}
