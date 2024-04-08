using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace RenewRentalNotification.Logic.SendEmailToTenant
{
    public class SendEmailToTenantHandler
    {
        private ILogger<SendEmailToTenantHandler> _logger;
        private IMemoryCache _memoryCache;

        public SendEmailToTenantHandler(ILogger<SendEmailToTenantHandler> logger, IMemoryCache memoryCache)
        {
            _logger = logger;
            _memoryCache = memoryCache;
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
                _logger.LogError(String.Format("Error validating input object for Send Email To Tenant Handler : {0}", validationResult.Errors[0]));
                result.SendEmailToTenantResultStatus = SendEmailToTenantResultStatus.ValidationError;
                return result;
            }


            // Successful validation, do the handling
            try
            {
                var smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = _memoryCache.Get<int>("SMTPPort"),
                    Credentials = new NetworkCredential(_memoryCache.Get<string>("SMTPEmailAddress"), _memoryCache.Get<string>("SMTPEmailPassword")),
                    EnableSsl = true,
                };

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(_memoryCache.Get<string>("ManagementEmailAddress")),
                    Subject = String.Format(_memoryCache.Get<string>("EmailToTenantSubject"), sendEmailToTenantItem.FullAddress),
                    Body = BuildEmailBody(_memoryCache.Get<string>("EmailToTenantBody"), sendEmailToTenantItem),
                    IsBodyHtml = true,
                };

                //mailMessage.To.Add(sendEmailToTenantItem.EmailAddress);
                mailMessage.To.Add(_memoryCache.Get<string>("CCEmailAddress"));

                smtpClient.Send(mailMessage);

            } catch (Exception ex)
            {
                // ERROR WHILE BUILDING/SENDING EMAIL
                _logger.LogError(String.Format("Exception occurred while attempting to send email to tenant. EX : {0}", ex.Message));
                result.SendEmailToTenantResultStatus = SendEmailToTenantResultStatus.EmailError;
                return result;
            }

            _logger.LogInformation(String.Format("Email Successfully Sent to {0}", sendEmailToTenantItem.EmailAddress));
            return result;
        }

        private string BuildEmailBody(string filePath, SendEmailToTenantItem sendEmailToTenantItem)
        {
            DateTime renewalDeadline = sendEmailToTenantItem.ExpectedMoveOutDate.AddMonths(-1);

            _logger.LogInformation(Directory.GetCurrentDirectory());
            string fileContents = File.ReadAllText(filePath);
            string emailBody = String.Format(fileContents, sendEmailToTenantItem.FirstName,
                sendEmailToTenantItem.LastName, sendEmailToTenantItem.FullAddress,
                sendEmailToTenantItem.ExpectedMoveOutDate.ToShortDateString(), renewalDeadline.ToShortDateString());


            return emailBody;
        }
    }
}
