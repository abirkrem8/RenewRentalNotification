using RenewRentalNotification.Logic.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenewRentalNotification.Logic.SendEmailToTenant
{
    public class SendEmailToTenantResult
    {
        public SendEmailToTenantResultStatus SendEmailToTenantResultStatus { get; set; }
        public List<Error> SendEmailToTenantResultErrors { get; set; }

    }



    public enum SendEmailToTenantResultStatus
    {
        Success,
        ValidationError
    }
}
