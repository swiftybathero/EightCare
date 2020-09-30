using FluentAssertions;
using Xunit;

namespace EightCare.UnitTests
{
    public class DomainTests
    {
        [Fact]
        public void SampleTest()
        {
            // Arrange
            const bool ExpectedValue = true;

            // Act // Assert
            ExpectedValue.Should().BeTrue();
        }
    }
}
