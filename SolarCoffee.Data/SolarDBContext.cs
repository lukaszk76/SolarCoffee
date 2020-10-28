using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SolarCoffee.Data.Modules;

namespace SolarCoffee.Data
{
    public class SolarDBContext : IdentityDbContext
    {
        public SolarDBContext(){}

        public SolarDBContext(DbContextOptions options) : base(options) {}

        public virtual DbSet<Customer> Customers{ get; set; }
        public virtual DbSet<CustomerAdress> CustomerAdresses{ get; set; }
    }
}
