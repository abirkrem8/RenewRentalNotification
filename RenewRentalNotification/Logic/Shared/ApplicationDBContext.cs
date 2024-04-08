using Microsoft.EntityFrameworkCore;
using RenewRentalNotification.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenewRentalNotification.Logic.Shared
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // Your Entities
        public DbSet<RentalProperty> RentalProperties { get; set; }
        public DbSet<RentalTenant> RentalTenants { get; set; }
        public DbSet<TenantAssignment> TenantAssignments { get; set; }
    }
}
