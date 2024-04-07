using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenewRentalNotification.Logic.FindMoveOutTenants
{
    public class FindMoveOutTenantsValidator : AbstractValidator<FindMoveOutTenantsItem>
    {
        public FindMoveOutTenantsValidator()
        {
            RuleFor(x => x).NotNull();
        }
    }
}
