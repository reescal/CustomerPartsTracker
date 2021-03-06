// <auto-generated />
using System;
using CustomerPartsTracker.Server.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CustomerPartsTracker.Server.Migrations
{
    [DbContext(typeof(CustomerPartsTrackerContext))]
    partial class CustomerPartsTrackerContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.5")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CustomerPartsTracker.Shared.Models.Customer", b =>
                {
                    b.Property<short>("CustomerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("smallint")
                        .HasColumnName("CustomerId")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CustomerName")
                        .IsRequired()
                        .HasMaxLength(65)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(65)");

                    b.HasKey("CustomerId")
                        .HasName("PK_CustomerId");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("CustomerPartsTracker.Shared.Models.Part", b =>
                {
                    b.Property<short>("PartId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("smallint")
                        .HasColumnName("PartId")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<short>("CustomerId")
                        .HasColumnType("smallint")
                        .HasColumnName("CustomerId");

                    b.Property<string>("PartName")
                        .IsRequired()
                        .HasMaxLength(65)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(65)");

                    b.Property<string>("PartNumber")
                        .IsRequired()
                        .HasMaxLength(25)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(25)");

                    b.HasKey("PartId")
                        .HasName("PK_PartId");

                    b.HasIndex("CustomerId");

                    b.ToTable("Parts");
                });

            modelBuilder.Entity("CustomerPartsTracker.Shared.Models.Tracker", b =>
                {
                    b.Property<short>("TrackingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("smallint")
                        .HasColumnName("TrackingId")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Comments")
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Comments");

                    b.Property<string>("LotNo")
                        .IsRequired()
                        .HasMaxLength(25)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(25)")
                        .HasColumnName("LotNo");

                    b.Property<short>("PartId")
                        .HasColumnType("smallint")
                        .HasColumnName("PartId");

                    b.Property<string>("PoNo")
                        .HasMaxLength(25)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(25)")
                        .HasColumnName("PoNo");

                    b.Property<string>("ProjectNo")
                        .IsRequired()
                        .HasMaxLength(10)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(10)")
                        .HasColumnName("ProjectNo");

                    b.Property<string>("QaNote")
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("QaNote");

                    b.Property<DateTime>("ReceivedDate")
                        .HasColumnType("date");

                    b.Property<string>("SecondaryId")
                        .HasMaxLength(25)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(25)")
                        .HasColumnName("SecondaryId");

                    b.Property<string>("SerialNumber")
                        .HasMaxLength(25)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(25)")
                        .HasColumnName("SerialNumber");

                    b.Property<DateTime?>("ShippedDate")
                        .HasColumnType("date");

                    b.HasKey("TrackingId")
                        .HasName("PK_TrackingId");

                    b.HasIndex("PartId");

                    b.ToTable("Trackers");
                });

            modelBuilder.Entity("CustomerPartsTracker.Shared.Models.Part", b =>
                {
                    b.HasOne("CustomerPartsTracker.Shared.Models.Customer", "Customer")
                        .WithMany("Parts")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("CustomerPartsTracker.Shared.Models.Tracker", b =>
                {
                    b.HasOne("CustomerPartsTracker.Shared.Models.Part", "Part")
                        .WithMany("Trackers")
                        .HasForeignKey("PartId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Part");
                });

            modelBuilder.Entity("CustomerPartsTracker.Shared.Models.Customer", b =>
                {
                    b.Navigation("Parts");
                });

            modelBuilder.Entity("CustomerPartsTracker.Shared.Models.Part", b =>
                {
                    b.Navigation("Trackers");
                });
#pragma warning restore 612, 618
        }
    }
}
