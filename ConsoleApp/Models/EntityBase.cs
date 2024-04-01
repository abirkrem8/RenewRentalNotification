﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenewRentalNotification.Models
{
    public class EntityBase
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime CreationTimestamp { get; set; }
    }
}
