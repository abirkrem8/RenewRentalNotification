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
            RuleFor(x => x).NotNull().NotEmpty();
            RuleFor(x => x.FullAddress).NotNull().NotEmpty();
            RuleFor(x => x.FirstName).NotNull().NotEmpty();
            RuleFor(x => x.LastName).NotNull().NotEmpty();
            RuleFor(x => x.EmailAddress).NotNull().NotEmpty();
            RuleFor(x => x.ExpectedMoveOutDate).NotNull().NotEmpty();
        }
    }
}
