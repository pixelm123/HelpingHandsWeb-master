using HelpingHandsWeb.Models.Users;
using HelpingHandsWeb.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HelpingHandsWeb.Data
{
    public class ApplicationDbContext : DbContext 
    {
            public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
                : base(options)
            {
            }

        public DbSet<PreferredSuburb> PreferredSuburbs { get; set; }
        public DbSet<Suburb> Suburbs { get; set; }
        public DbSet<PatientCondition> PatientConditions { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<ChronicCondition> ChronicConditions { get; set; }
        public DbSet<CareVisit> CareVisits { get; set; }
        public DbSet<CareContract> CareContracts { get; set; }
        public DbSet<Business> Businesses { get; set; }
        public DbSet<UserType> UserTypes { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Nurse> Nurses { get; set; }
        public DbSet<Appointment> Appointments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

           
        }
    }
}