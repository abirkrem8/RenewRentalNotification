﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenewRentalNotification.Models
{
    public class Tenant : EntityBase
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }

        [ForeignKey("RentalPropertyId")]
        public RentalProperty RentalProperty { get; set; }
        public DateTime DateOfMoveIn { get; set; }
        public DateTime ExpectedMoveOutDate { get; set; }
    }
}
