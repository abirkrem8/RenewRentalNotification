using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
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
        private IMemoryCache _memoryCache;

        public FindMoveOutTenantsHandler(ILogger<FindMoveOutTenantsHandler> logger, ApplicationDbContext dbContext,
            IMapper mapper, IMemoryCache memoryCache)
        {
            _logger = logger;
            _dbContext = dbContext;
            _mapper = mapper;
            _memoryCache = memoryCache;
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
            DateTime startSearchDate = DateTime.Now.AddDays(_memoryCache.Get<int>("DaysToLookAhead")).Date;
            DateTime endSearchDate = startSearchDate.AddDays(7);
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
