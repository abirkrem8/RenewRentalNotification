using Microsoft.EntityFrameworkCore;
using RenewRentalNotification.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenewRentalNotification.Logic
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // Your Entities
        public DbSet<RentalProperty> RentalProperties { get; set; }
        public DbSet<Tenant> Tenants { get; set; }
    }
}
