using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace RenewRentalNotification.Logic.SendEmailToTenant
{
    public class SendEmailToTenantHandler
    {
        private ILogger<SendEmailToTenantHandler> _logger;

        public SendEmailToTenantHandler(ILogger<SendEmailToTenantHandler> logger)
        {
            _logger = logger;
        }

        public SendEmailToTenantResult Handle(SendEmailToTenantItem sendEmailToTenantItem)
        {
            SendEmailToTenantResult result = new SendEmailToTenantResult();

            SendEmailToTenantValidator validator = new SendEmailToTenantValidator();
            var validationResult = validator.Validate(sendEmailToTenantItem);

            if (!validationResult.IsValid)
            {
                // There was an error in validation, quit now
                // log the error
                return result;
            }

            // Successful validation, do the handling
            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                EnableSsl = true,
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress("email"),
                Subject = "subject",
                Body = File.ReadAllText("..\\..\\Resources\\EmailToTenant.html"),
                IsBodyHtml = true,
            };
            mailMessage.To.Add(sendEmailToTenantItem.EmailAddress);

            smtpClient.Send(mailMessage);

            return result;
        }
    }
}
