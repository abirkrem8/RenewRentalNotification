using AutoMapper;
using Microsoft.Extensions.Logging;
using RenewRentalNotification.Logic.FindMoveOutTenants;
using RenewRentalNotification.Logic.SendEmailToTenant;
using RenewRentalNotification.Logic.SendMoveOutListToManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenewRentalNotification
{
    public interface IRenewRentalNotificationService
    {
        int Run();
    }
    public class RenewRentalNotificationService : IRenewRentalNotificationService
    {
        private ILogger<RenewRentalNotificationService> _logger;
        private FindMoveOutTenantsHandler _findMoveOutTenantsHandler;
        private SendEmailToTenantHandler _sendEmailToTenantHandler;
        private SendMoveOutListToManagementHandler _sendMoveOutListToManagementHandler;
        private IMapper _mapper;

        public RenewRentalNotificationService(ILogger<RenewRentalNotificationService> logger, FindMoveOutTenantsHandler findMoveOutTenantsHandler,
            IMapper mapper, SendEmailToTenantHandler sendEmailToTenantHandler, SendMoveOutListToManagementHandler sendMoveOutListToManagementHandler)
        {
            _logger = logger;
            _mapper = mapper;
            _findMoveOutTenantsHandler = findMoveOutTenantsHandler;
            _sendEmailToTenantHandler = sendEmailToTenantHandler;
            _sendMoveOutListToManagementHandler = sendMoveOutListToManagementHandler;
        }

        public int Run()
        {
            // First comb through the DB and look at the records to see which Tenants will be moving out in the next 7-8 weeks.
            _logger.LogInformation("Calling database to find Tenants moving out in the next 7-8 weeks.");

            var findTenantsResult = _findMoveOutTenantsHandler.Handle(new FindMoveOutTenantsItem());
            if (findTenantsResult.FindMoveOutTenantsResultStatus != FindMoveOutTenantsResultStatus.Success)
            {
                // handle error
            }

            List<SendEmailToTenantItem> emailList = _mapper.Map<List<SendEmailToTenantItem>>(findTenantsResult.MoveOutTenantsResultItems);
            foreach (var email in emailList)
            {
                _sendEmailToTenantHandler.Handle(email);
            }

            SendMoveOutListToManagementItem sendMoveOutListToManagementItem = new SendMoveOutListToManagementItem()
            {
                MoveOutList = _mapper.Map<List<SendMoveOutListToManagementListItem>>(findTenantsResult.MoveOutTenantsResultItems)
            };
            _sendMoveOutListToManagementHandler.Handle(sendMoveOutListToManagementItem);




            return 0;
        }
    }
}
