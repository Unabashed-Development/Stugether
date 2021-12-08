using NUnit.Framework;

namespace Gateway.Test
{
    public class Gateway_FiddleHelper_GetConnectionStringSql
    {
        [Test]
        public void GetConnectionStringSql_ValidString_ReturnsValue()
        {
            // Arrange
            const string connectionString = "StudentMatcherDB";

            // Act
            string result = FiddleHelper.GetConnectionStringSql(connectionString);

            // Assert
            Assert.IsNotEmpty(result);
        }
    }
}