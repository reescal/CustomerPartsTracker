using System;
using Microsoft.EntityFrameworkCore;
using CustomerPartsTracker.Shared.Models;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

#nullable disable

namespace CustomerPartsTracker.Server.Data
{
    public partial class CustomerPartsTrackerContext : DbContext
    {
        public CustomerPartsTrackerContext()
        {
        }

        public CustomerPartsTrackerContext(DbContextOptions<CustomerPartsTrackerContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Part> Parts { get; set; }
        public virtual DbSet<Tracker> Trackers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                IConfigurationRoot configuration = new ConfigurationBuilder()
                          .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                          .AddJsonFile("appsettings.json")
                          .Build();
                optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
                //optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=CustomerPartsTracker;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PK_CustomerId");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("CustomerId");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(CustomerValidations.MaxLength)
                    .IsUnicode(true)
                    .HasColumnName("CustomerName");

                entity.HasMany(c => c.Parts).WithOne(a => a.Customer).HasForeignKey(a => a.CustomerId);

            });

            modelBuilder.Entity<Part>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PK_PartId");

                entity.Property(e => e.CustomerId)
                    .IsRequired()
                    .HasColumnName("CustomerId");

                entity.Property(e => e.Id)
                    .IsRequired()
                    .ValueGeneratedOnAdd()
                    .HasColumnName("PartId");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(65)
                    .IsUnicode(true)
                    .HasColumnName("PartName");

                entity.Property(e => e.Number)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(true)
                    .HasColumnName("PartNumber");

                entity.HasMany(c => c.Trackers).WithOne(a => a.Part).HasForeignKey(a => a.PartId);
            });

            modelBuilder.Entity<Tracker>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PK_TrackingId");

                entity.ToTable("Trackers");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("TrackingId");

                entity.Property(e => e.Comments)
                    .IsUnicode(true)
                    .HasColumnName("Comments");

                entity.Property(e => e.SecondaryId)
                    .HasMaxLength(25)
                    .IsUnicode(true)
                    .HasColumnName("SecondaryId");

                entity.Property(e => e.SerialNumber)
                    .HasMaxLength(25)
                    .IsUnicode(true)
                    .HasColumnName("SerialNumber");

                entity.Property(e => e.LotNo)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(true)
                    .HasColumnName("LotNo");

                entity.Property(e => e.PartId)
                    .IsRequired()
                    .HasColumnName("PartId");

                entity.Property(e => e.PoNo)
                    .HasMaxLength(25)
                    .IsUnicode(true)
                    .HasColumnName("PoNo");

                entity.Property(e => e.ProjectNo)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(true)
                    .HasColumnName("ProjectNo");

                entity.Property(e => e.QaNote)
                    .IsUnicode(true)
                    .HasColumnName("QaNote");

                entity.Property(e => e.ReceivedDate)
                    .IsRequired()
                    .HasColumnType("date");

                entity.Property(e => e.ShippedDate).HasColumnType("date");
            });

            OnModelCreatingPartial(modelBuilder);

            //modelBuilder.Seed();
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
