using Microsoft.Extensions.Logging;
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

        public RenewRentalNotificationService(ILogger<RenewRentalNotificationService> logger)
        {
            _logger = logger;
        }

        public int Run()
        {

            throw new NotImplementedException();
        }
    }
}
