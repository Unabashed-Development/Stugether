using NUnit.Framework;
using ViewModel.Helpers;

namespace ViewModel.Test
{
    public class ViewModel_Helpers_AccountHelper_GenerateVerificationCode
    {
        [Test]
        public void GenerateVerificationCode_1000Times_Returns6CharacterString()
        {
            // Arrange
            const string randomEmail = "RandomEmailUsedForSeed@test.com";
            bool allAre6Characters = true;

            // Act
            for (int i = 0; i < 1000; i++)
            {
                string verificationCode = AccountHelper.GenerateVerificationCode(randomEmail);
                if (!(verificationCode.Length == 6))
                {
                    allAre6Characters = false;
                }
            }

            // Assert
            Assert.IsTrue(allAre6Characters);
        }
    }
}