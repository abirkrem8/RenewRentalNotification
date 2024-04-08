using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenewRentalNotification.Logic.SendMoveOutListToManagement
{
    public class SendMoveOutListToManagementItem
    {
        public List<SendMoveOutListToManagementListItem> MoveOutList { get; set; } = new List<SendMoveOutListToManagementListItem>();


    }

    public class SendMoveOutListToManagementListItem
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }

        public string Address1 { get; set; } = string.Empty;
        public string Address2 { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string ZipCode { get; set; } = string.Empty;

        public DateTime DateOfMoveIn { get; set; }
        public DateTime ExpectedMoveOutDate { get; set; }
    }
}
