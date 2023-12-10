using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vizeeta.Domain.Entities;

namespace Vizeeta.Domain.Data
{
    public class StoreContext : IdentityDbContext<AppUser>
    {
        public StoreContext(DbContextOptions<StoreContext> options):base(options)
        { }

        public DbSet<Admin> Admin { set; get; }
        public DbSet<Patient> Patient { set; get; }
        public DbSet<Doctor> Doctor { set; get; }
        public DbSet<Booking> Booking { set; get; }
        public DbSet<Appoinment> Appoinment { set; get; }
        public DbSet<Specialization> Specialization { set; get; }
        public DbSet<Day> Day { set; get; }
        public DbSet<Times> Times { set; get; }
        public DbSet<Cupone> Cupons { set; get; }
        
    }
}
