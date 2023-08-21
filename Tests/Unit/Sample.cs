using Xunit;
using YourAppNamespace;

namespace YourTestNamespace
{
    public class YourTestClass
    {
        [Fact]
        public void TestMethod1()
        {
            // Arrange
            int expected = 42;

            // Act
            int result = 42;

            // Assert
            Assert.Equal(expected, result);
        }
    }
}
