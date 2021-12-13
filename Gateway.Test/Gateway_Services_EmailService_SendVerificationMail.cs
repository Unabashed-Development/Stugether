using Gateway.Services;
using NUnit.Framework;
using System;

namespace Gateway.Test
{
    public class Gateway_Services_EmailService_SendVerificationMail
    {
        [Test]
        public void SendVerificationMail_HasSuccessfullySent_ThrowsNoException()
        {
            // Arrange
            const string email = "ThisAccountDoesExist@wafoe.nl";
            const string verificationCode = "123456";

            // Act
            static void HasSuccessfullySent()
            {
                try
                {
                    EmailService.SendVerificationMail(email, verificationCode);
                }
                catch (Exception)
                {
                    throw;
                }
            }
            
            // Assert
            Assert.DoesNotThrow(() => HasSuccessfullySent());
        }
    }
}