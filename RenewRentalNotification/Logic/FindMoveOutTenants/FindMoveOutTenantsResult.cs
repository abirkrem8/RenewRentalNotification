using RenewRentalNotification.Logic.Shared;
using RenewRentalNotification.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenewRentalNotification.Logic.FindMoveOutTenants
{
    public class FindMoveOutTenantsResult
    {
        public FindMoveOutTenantsResultStatus FindMoveOutTenantsResultStatus { get; set; }
        public List<Error> FindMoveOutTenantsResultErrors { get; set; }

        public List<FindMoveOutTenantsResultItem> MoveOutTenantsResultItems { get; set; } = new List<FindMoveOutTenantsResultItem>();


    }

    public class FindMoveOutTenantsResultItem
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



    public enum FindMoveOutTenantsResultStatus
    {
        Success,
        ValidationError
    }
}
