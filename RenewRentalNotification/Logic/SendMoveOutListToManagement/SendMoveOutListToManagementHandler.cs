using CsvHelper;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace RenewRentalNotification.Logic.SendMoveOutListToManagement
{
    public class SendMoveOutListToManagementHandler
    {
        private ILogger<SendMoveOutListToManagementHandler> _logger;
        private IMemoryCache _memoryCache;

        public SendMoveOutListToManagementHandler(ILogger<SendMoveOutListToManagementHandler> logger,
            IMemoryCache memoryCache)
        {
            _logger = logger;
            _memoryCache = memoryCache;
        }

        public SendMoveOutListToManagementResult Handle(SendMoveOutListToManagementItem sendMoveOutListToManagementItem)
        {
            SendMoveOutListToManagementResult result = new SendMoveOutListToManagementResult();

            SendMoveOutListToManagementValidator validator = new SendMoveOutListToManagementValidator();
            var validationResult = validator.Validate(sendMoveOutListToManagementItem);

            if (!validationResult.IsValid)
            {
                // There was an error in validation, quit now
                // log the error
                return result;
            }

            // Successful validation, do the handling
            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = _memoryCache.Get<int>("SMTPPort"),
                Credentials = new NetworkCredential(_memoryCache.Get<string>("SMTPEmailAddress"), _memoryCache.Get<string>("SMTPEmailPassword")),
                EnableSsl = true,
            };


            var mailMessage = new MailMessage
            {
                From = new MailAddress(_memoryCache.Get<string>("ManagementEmailAddress")),
                Subject = string.Format(_memoryCache.Get<string>("EmailToManagementSubject"), DateTime.Now.ToShortDateString()),
                Body = BuildEmailBody(sendMoveOutListToManagementItem),
                IsBodyHtml = true,
            };

            if (sendMoveOutListToManagementItem.MoveOutList.Any())
            {
                string attachmentFileLocation = BuildAttachment(sendMoveOutListToManagementItem);
                Attachment attachment = new Attachment(attachmentFileLocation);
                mailMessage.Attachments.Add(attachment);
            }

            //mailMessage.To.Add(_memoryCache.Get<string>("ManagementEmailAddress"));
            mailMessage.To.Add(_memoryCache.Get<string>("CCEmailAddress"));

            smtpClient.Send(mailMessage);


            return result;
        }

        private string BuildEmailBody(SendMoveOutListToManagementItem sendEmailToTenantItem)
        {
            DateTime startSearchDate = DateTime.Now.AddDays(_memoryCache.Get<int>("DaysToLookAhead")).Date;
            DateTime endSearchDate = startSearchDate.AddDays(7);

            string filePath = sendEmailToTenantItem.MoveOutList.Any() ?
                _memoryCache.Get<string>("EmailToManagementBody") :
                _memoryCache.Get<string>("EmailToManagementBodyNoMoveOuts");
            string body = string.Format(File.ReadAllText(filePath), startSearchDate.ToShortDateString(), endSearchDate.ToShortDateString());

            return body;
        }

        private string BuildAttachment(SendMoveOutListToManagementItem sendEmailToTenantItem)
        {
            string fileName = string.Format("RPM_MoveOutTenants_{0}.csv", DateTime.Now.ToString("yyyy_MM_dd"));
            string fileLocation = Path.Combine(_memoryCache.Get<string>("AttachmentFile"), fileName);

            using (var writer = new StreamWriter(fileLocation))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(sendEmailToTenantItem.MoveOutList);
            }

            return fileLocation;
        }
    }
}
