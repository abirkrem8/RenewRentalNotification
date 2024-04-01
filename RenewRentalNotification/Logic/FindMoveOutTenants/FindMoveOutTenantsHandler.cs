using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RenewRentalNotification.Logic.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenewRentalNotification.Logic.FindMoveOutTenants
{
    public class FindMoveOutTenantsHandler
    {
        private ILogger<FindMoveOutTenantsHandler> _logger;
        private ApplicationDbContext _dbContext;
        private IMapper _mapper;

        public FindMoveOutTenantsHandler(ILogger<FindMoveOutTenantsHandler> logger, ApplicationDbContext dbContext,
            IMapper mapper)
        {
            _logger = logger;
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public FindMoveOutTenantsResult Handle(FindMoveOutTenantsItem FindMoveOutTenantsItem)
        {
            FindMoveOutTenantsResult result = new FindMoveOutTenantsResult();

            FindMoveOutTenantsValidator validator = new FindMoveOutTenantsValidator();
            var validationResult = validator.Validate(FindMoveOutTenantsItem);

            if (!validationResult.IsValid)
            {
                // There was an error in validation, quit now
                // log the error
                result.FindMoveOutTenantsResultStatus = FindMoveOutTenantsResultStatus.ValidationError;
                return result;
            }

            // Successful validation, do the handling
            // Use Dapper to search for tenant assignments meeting criteria
            DateTime startSearchDate = DateTime.Now.AddMonths(2).Date;
            DateTime endSearchDate = DateTime.Now.AddMonths(2).AddDays(7).Date;
            var filteredTenantAssignments = _dbContext.TenantAssignments.Where(t => startSearchDate <= t.ExpectedMoveOutDate.Date 
                                        && t.ExpectedMoveOutDate < endSearchDate)
                                        .Include(x => x.RentalProperty)
                                        .Include(y=> y.Tenant).ToList();

            // now that we have the tenants that will be moving out soon, let's put it in the result object
            result.MoveOutTenantsResultItems = _mapper.Map<List<FindMoveOutTenantsResultItem>>(filteredTenantAssignments);
            result.FindMoveOutTenantsResultStatus = FindMoveOutTenantsResultStatus.Success;
            return result;
        }
    }
}
