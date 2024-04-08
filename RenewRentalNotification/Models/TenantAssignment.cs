using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenewRentalNotification.Models
{
    public class TenantAssignment :EntityBase
    {
        [ForeignKey("RentalPropertyId")]
        public RentalProperty RentalProperty { get; set; }
        public DateTime DateOfMoveIn { get; set; }
        public DateTime ExpectedMoveOutDate { get; set; }

        [ForeignKey("RentalTenantId")]
        public RentalTenant Tenant { get; set; }
    }
}
