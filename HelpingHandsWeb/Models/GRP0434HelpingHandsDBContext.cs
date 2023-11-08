using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace HelpingHandsWeb.Models
{
    public partial class GRP0434HelpingHandsDBContext : DbContext
    {
        public GRP0434HelpingHandsDBContext()
        {
        }

        public GRP0434HelpingHandsDBContext(DbContextOptions<GRP0434HelpingHandsDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Appointment> Appointments { get; set; } = null!;
        public virtual DbSet<AspNetRole> AspNetRoles { get; set; } = null!;
        public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; } = null!;
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; } = null!;
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; } = null!;
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; } = null!;
        public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; } = null!;
        public virtual DbSet<Business> Businesses { get; set; } = null!;
        public virtual DbSet<CareContract> CareContracts { get; set; } = null!;
        public virtual DbSet<CareVisit> CareVisits { get; set; } = null!;
        public virtual DbSet<ChronicCondition> ChronicConditions { get; set; } = null!;
        public virtual DbSet<City> Cities { get; set; } = null!;
        public virtual DbSet<Nurse> Nurses { get; set; } = null!;
        public virtual DbSet<Patient> Patients { get; set; } = null!;
        public virtual DbSet<PatientCondition> PatientConditions { get; set; } = null!;
        public virtual DbSet<PreferredSuburb> PreferredSuburbs { get; set; } = null!;
        public virtual DbSet<Suburb> Suburbs { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<Usertype> Usertypes { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=SICT-SQL.MANDELA.AC.ZA;Database=GRP-04-34-HelpingHandsDB;User ID=GRP-04-34;Password=grp-04-34-2023#;Trusted_Connection=False;MultipleActiveResultSets=true;TrustServerCertificate=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Appointment>(entity =>
            {
                entity.ToTable("APPOINTMENT");

                entity.Property(e => e.AppointmentId).HasColumnName("AppointmentID");

                entity.Property(e => e.AppointmentDate).HasColumnType("date");

                entity.Property(e => e.NurseId).HasColumnName("NurseId");

                entity.Property(e => e.PatientId).HasColumnName("PatientId");

                entity.Property(e => e.Title).HasMaxLength(100);

                entity.HasOne(d => d.Nurse)
                    .WithMany(p => p.Appointments)
                    .HasForeignKey(d => d.NurseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_APPOINTMENT_Nurse");

                entity.HasOne(d => d.Patient)
                    .WithMany(p => p.Appointments)
                    .HasForeignKey(d => d.PatientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_APPOINTMENT_Patient");
            });

            modelBuilder.Entity<AspNetRole>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedName] IS NOT NULL)");

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetRoleClaim>(entity =>
            {
                entity.HasIndex(e => e.RoleId, "IX_AspNetRoleClaims_RoleId");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetRoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<AspNetUser>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);

                entity.HasMany(d => d.Roles)
                    .WithMany(p => p.Users)
                    .UsingEntity<Dictionary<string, object>>(
                        "AspNetUserRole",
                        l => l.HasOne<AspNetRole>().WithMany().HasForeignKey("RoleId"),
                        r => r.HasOne<AspNetUser>().WithMany().HasForeignKey("UserId"),
                        j =>
                        {
                            j.HasKey("UserId", "RoleId");

                            j.ToTable("AspNetUserRoles");

                            j.HasIndex(new[] { "RoleId" }, "IX_AspNetUserRoles_RoleId");
                        });
            });

            modelBuilder.Entity<AspNetUserClaim>(entity =>
            {
                entity.HasIndex(e => e.UserId, "IX_AspNetUserClaims_UserId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserLogin>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.HasIndex(e => e.UserId, "IX_AspNetUserLogins_UserId");

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.ProviderKey).HasMaxLength(128);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserToken>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.Name).HasMaxLength(128);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserTokens)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<Business>(entity =>
            {
                entity.ToTable("BUSINESS");

                entity.Property(e => e.BusinessId)
                    .ValueGeneratedNever()
                    .HasColumnName("BusinessID");

                entity.Property(e => e.Address).HasMaxLength(100);

                entity.Property(e => e.ContactNumber).HasMaxLength(20);

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.OperatingHours).HasMaxLength(100);

                entity.Property(e => e.OrganizationName).HasMaxLength(100);
            });

            modelBuilder.Entity<CareContract>(entity =>
            {
                entity.HasKey(e => e.ContractId);

                entity.ToTable("CARE_CONTRACT");

                entity.Property(e => e.ContractId).HasColumnName("ContractID");

                entity.Property(e => e.AddressLine1).HasMaxLength(100);

                entity.Property(e => e.AddressLine2).HasMaxLength(100);

                entity.Property(e => e.ContractDate).HasColumnType("date");

                entity.Property(e => e.ContractStatus).HasMaxLength(50);

                entity.Property(e => e.EndCareDate).HasColumnType("date");

                entity.Property(e => e.NurseId).HasColumnName("NurseId");

                entity.Property(e => e.PatientId).HasColumnName("PatientId");

                entity.Property(e => e.StartCareDate).HasColumnType("date");

                entity.HasOne(d => d.Nurse)
                    .WithMany(p => p.CareContracts)
                    .HasForeignKey(d => d.NurseId)
                    .HasConstraintName("FK_CARE_CONTRACT_Nurse");

                entity.HasOne(d => d.Patient)
                    .WithMany(p => p.CareContracts)
                    .HasForeignKey(d => d.PatientId)
                    .HasConstraintName("FK_CARE_CONTRACT_Patient");

                entity.HasOne(d => d.Suburb)
                    .WithMany(p => p.CareContracts)
                    .HasForeignKey(d => d.SuburbId)
                    .HasConstraintName("FK_CARE_CONTRACT_Suburb");
            });

            modelBuilder.Entity<CareVisit>(entity =>
            {
                entity.HasKey(e => e.VisitId);

                entity.ToTable("CARE_VISIT");

                entity.Property(e => e.VisitId).HasColumnName("VisitID");

                entity.Property(e => e.ContractId).HasColumnName("ContractID");

                entity.Property(e => e.PatientId).HasColumnName("PatientId");

                entity.Property(e => e.VisitDate).HasColumnType("date");

                entity.HasOne(d => d.Contract)
                    .WithMany(p => p.CareVisits)
                    .HasForeignKey(d => d.ContractId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CARE_VISIT_CareContract");

                entity.HasOne(d => d.Patient)
                    .WithMany(p => p.CareVisits)
                    .HasForeignKey(d => d.PatientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CARE_VISIT_Patient");
            });

            modelBuilder.Entity<ChronicCondition>(entity =>
            {
                entity.HasKey(e => e.ConditionId)
                    .HasName("PK__CHRONIC___37F5C0EFB4059644");

                entity.ToTable("CHRONIC_CONDITION");

                entity.Property(e => e.ConditionId).HasColumnName("ConditionID");

                entity.Property(e => e.Name).HasMaxLength(100);
            });

            modelBuilder.Entity<City>(entity =>
            {
                entity.ToTable("CITY");

                entity.Property(e => e.Abbreviation).HasMaxLength(10);

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<Nurse>(entity =>
            {
                entity.ToTable("NURSE");

                entity.Property(e => e.NurseId).HasColumnName("NurseId");

                entity.Property(e => e.FirstName).HasMaxLength(50);

                entity.Property(e => e.Gender)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Idno)
                    .HasMaxLength(20)
                    .HasColumnName("IDNo");

                entity.Property(e => e.Surname).HasMaxLength(50);

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Nurses)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_NURSE_UserID");
            });

            modelBuilder.Entity<Patient>(entity =>
            {
                entity.ToTable("PATIENTS");

                entity.Property(e => e.PatientId).HasColumnName("PatientId");

                entity.Property(e => e.DateOfBirth).HasColumnType("date");

                entity.Property(e => e.EmergencyContactNo).HasMaxLength(15);

                entity.Property(e => e.EmergencyPerson).HasMaxLength(100);

                entity.Property(e => e.FirstName).HasMaxLength(50);

                entity.Property(e => e.Gender)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.Surname).HasMaxLength(50);

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Patients)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_PATIENTS_UserID");
            });

            modelBuilder.Entity<PatientCondition>(entity =>
            {
                entity.ToTable("PATIENT_CONDITION");

                entity.Property(e => e.PatientConditionId).ValueGeneratedNever();

                entity.Property(e => e.ConditionId).HasColumnName("ConditionID");

                entity.Property(e => e.PatientId).HasColumnName("PatientId");

                entity.HasOne(d => d.Condition)
                    .WithMany(p => p.PatientConditions)
                    .HasForeignKey(d => d.ConditionId)
                    .HasConstraintName("FK__PATIENT_C__Condi__52E34C9D");

                entity.HasOne(d => d.Patient)
                    .WithMany(p => p.PatientConditions)
                    .HasForeignKey(d => d.PatientId)
                    .HasConstraintName("FK__PATIENT_C__Patie__51EF2864");
            });

            modelBuilder.Entity<PreferredSuburb>(entity =>
            {
                entity.HasKey(e => e.PrefferredSuburb)
                    .HasName("PK__PREFERRE__62B23593833C8805");

                entity.ToTable("PREFERRED_SUBURB");

                entity.Property(e => e.PrefferredSuburb).ValueGeneratedNever();

                entity.Property(e => e.NurseId).HasColumnName("NurseId");

                entity.Property(e => e.SuburbId).HasColumnName("SuburbID");

                entity.HasOne(d => d.Nurse)
                    .WithMany(p => p.PreferredSuburbs)
                    .HasForeignKey(d => d.NurseId)
                    .HasConstraintName("FK__PREFERRED__Nurse__640DD89F");

                entity.HasOne(d => d.Suburb)
                    .WithMany(p => p.PreferredSuburbs)
                    .HasForeignKey(d => d.SuburbId)
                    .HasConstraintName("FK__PREFERRED__Subur__6501FCD8");
            });

            modelBuilder.Entity<Suburb>(entity =>
            {
                entity.ToTable("SUBURBS");

                entity.Property(e => e.PostalCode).HasMaxLength(10);

                entity.Property(e => e.Suburb1)
                    .HasMaxLength(50)
                    .HasColumnName("Suburb");

                entity.HasOne(d => d.City)
                    .WithMany(p => p.Suburbs)
                    .HasForeignKey(d => d.CityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SUBURBS_CITY");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("USER");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.ContactNo).HasMaxLength(15);

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.Password).HasMaxLength(50);

                entity.Property(e => e.Status)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('A')")
                    .IsFixedLength();

                entity.Property(e => e.UserName).HasMaxLength(50);

                entity.Property(e => e.UserType)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.HasOne(d => d.UserTypeNavigation)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.UserType)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_USER_UserType");
            });

            modelBuilder.Entity<Usertype>(entity =>
            {
                entity.ToTable("USERTYPE");

                entity.Property(e => e.UserTypeId)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.UserTypeDesc).HasMaxLength(100);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
