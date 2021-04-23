using FluentAssertions;
using PartialClassSample.Api.Tests.Shared;
using Xunit;

namespace PartialClassSample.Api.Tests.Register
{
    public class PasswordIsValidUnitTests : BaseMock
    {
        [Fact]
        public void PasswordIsValis_ShouldReturnTrue()
        {
            var register = RegisterFake(passWord: "abc123");

            var response = register.PasswordIsValid("abc123");

            response.Should().BeTrue();
        }

        [Fact]
        public void PasswordIsValid_ShouldReturnFalse()
        {
            var register = RegisterFake(passWord: "abc123");

            var response = register.PasswordIsValid("123456");

            response.Should().BeFalse();
        }
    }
}
