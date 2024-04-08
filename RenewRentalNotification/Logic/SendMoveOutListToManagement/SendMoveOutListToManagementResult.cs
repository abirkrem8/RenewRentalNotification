using RenewRentalNotification.Logic.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenewRentalNotification.Logic.SendMoveOutListToManagement
{
    public class SendMoveOutListToManagementResult
    {
        public SendMoveOutListToManagementResultStatus SendMoveOutListToManagementResultStatus { get; set; }


    }



    public enum SendMoveOutListToManagementResultStatus
    {
        Success,
        ValidationError,
        EmailError
    }
}
