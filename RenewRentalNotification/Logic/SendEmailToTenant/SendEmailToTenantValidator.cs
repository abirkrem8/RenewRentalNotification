using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenewRentalNotification.Logic.SendEmailToTenant
{
    public class SendEmailToTenantValidator : AbstractValidator<SendEmailToTenantItem>
    {
        public SendEmailToTenantValidator()
        {

        }
    }
}
