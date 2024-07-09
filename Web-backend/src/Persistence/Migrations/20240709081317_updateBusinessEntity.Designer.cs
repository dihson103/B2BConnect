﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Persistence;

#nullable disable

namespace Persistence.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240709081317_updateBusinessEntity")]
    partial class updateBusinessEntity
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Domain.Entities.Account", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<bool>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(true);

                    b.Property<bool>("IsAdmin")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false);

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("Domain.Entities.Branch", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("varchar(200)");

                    b.Property<Guid>("BusinessId")
                        .HasColumnType("uuid");

                    b.Property<string>("Email")
                        .HasColumnType("varchar(50)");

                    b.Property<bool>("IsMainBranch")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false);

                    b.Property<string>("Phone")
                        .HasColumnType("varchar(10)");

                    b.HasKey("Id");

                    b.HasIndex("BusinessId");

                    b.ToTable("Branches");
                });

            modelBuilder.Entity("Domain.Entities.Business", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("AccountId")
                        .HasColumnType("uuid");

                    b.Property<string>("AvatarImage")
                        .HasColumnType("varchar(50)");

                    b.Property<string>("CoverImage")
                        .HasColumnType("varchar(50)");

                    b.Property<DateOnly>("DateOfEstablishment")
                        .HasColumnType("date");

                    b.Property<bool>("IsVerified")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<int>("NumberOfEmployee")
                        .HasColumnType("integer");

                    b.Property<Guid?>("RepresentativeId")
                        .HasColumnType("uuid");

                    b.Property<string>("TaxCode")
                        .IsRequired()
                        .HasColumnType("varchar(15)");

                    b.Property<string>("WebSite")
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("AccountId")
                        .IsUnique();

                    b.HasIndex("TaxCode")
                        .IsUnique();

                    b.ToTable("Businesses");
                });

            modelBuilder.Entity("Domain.Entities.Event", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<DateTime>("EndAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<DateTime>("StartAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("Domain.Entities.EventIndustry", b =>
                {
                    b.Property<Guid>("EventId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("IndustryId")
                        .HasColumnType("uuid");

                    b.HasKey("EventId", "IndustryId");

                    b.HasIndex("IndustryId");

                    b.ToTable("EventIndustries");
                });

            modelBuilder.Entity("Domain.Entities.Image", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("BusinessId")
                        .HasColumnType("uuid");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("BusinessId");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("Domain.Entities.Industry", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(200)");

                    b.HasKey("Id");

                    b.ToTable("Industries");
                });

            modelBuilder.Entity("Domain.Entities.Participation", b =>
                {
                    b.Property<Guid>("BusinessId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("EventId")
                        .HasColumnType("uuid");

                    b.Property<DateOnly>("JoinDate")
                        .HasColumnType("date");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.HasKey("BusinessId", "EventId");

                    b.HasIndex("EventId");

                    b.ToTable("Participations");
                });

            modelBuilder.Entity("Domain.Entities.Representative", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("varchar(200)");

                    b.Property<Guid>("BusinessId")
                        .HasColumnType("uuid");

                    b.Property<DateOnly>("Dob")
                        .HasColumnType("date");

                    b.Property<string>("Fullname")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<bool>("Gender")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(true);

                    b.Property<string>("GovernmentId")
                        .IsRequired()
                        .HasColumnType("varchar(12)");

                    b.HasKey("Id");

                    b.HasIndex("BusinessId")
                        .IsUnique();

                    b.ToTable("Representatives");
                });

            modelBuilder.Entity("Domain.Entities.Sector", b =>
                {
                    b.Property<Guid>("BusinessId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("IndustryId")
                        .HasColumnType("uuid");

                    b.HasKey("BusinessId", "IndustryId");

                    b.HasIndex("IndustryId");

                    b.ToTable("Sectors");
                });

            modelBuilder.Entity("Domain.Entities.Verification", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("BusinessId")
                        .HasColumnType("uuid");

                    b.Property<string>("BusinessLicense")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<int>("BusinessType")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("CheckedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("EstablishmentCertificate")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<bool>("IsChecked")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false);

                    b.Property<string>("Note")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("BusinessId");

                    b.ToTable("Verifications");
                });

            modelBuilder.Entity("Domain.Entities.Branch", b =>
                {
                    b.HasOne("Domain.Entities.Business", "Business")
                        .WithMany("Branches")
                        .HasForeignKey("BusinessId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Business");
                });

            modelBuilder.Entity("Domain.Entities.Business", b =>
                {
                    b.HasOne("Domain.Entities.Account", "Account")
                        .WithOne("Business")
                        .HasForeignKey("Domain.Entities.Business", "AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("Domain.Entities.EventIndustry", b =>
                {
                    b.HasOne("Domain.Entities.Event", "Event")
                        .WithMany("EventIndustries")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Industry", "Industry")
                        .WithMany("EventIndustries")
                        .HasForeignKey("IndustryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Event");

                    b.Navigation("Industry");
                });

            modelBuilder.Entity("Domain.Entities.Image", b =>
                {
                    b.HasOne("Domain.Entities.Business", "Business")
                        .WithMany("Images")
                        .HasForeignKey("BusinessId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Business");
                });

            modelBuilder.Entity("Domain.Entities.Participation", b =>
                {
                    b.HasOne("Domain.Entities.Business", "Business")
                        .WithMany("Participations")
                        .HasForeignKey("BusinessId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Event", "Event")
                        .WithMany("Participations")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Business");

                    b.Navigation("Event");
                });

            modelBuilder.Entity("Domain.Entities.Representative", b =>
                {
                    b.HasOne("Domain.Entities.Business", "Business")
                        .WithOne("Representative")
                        .HasForeignKey("Domain.Entities.Representative", "BusinessId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Business");
                });

            modelBuilder.Entity("Domain.Entities.Sector", b =>
                {
                    b.HasOne("Domain.Entities.Business", "Business")
                        .WithMany("Sectors")
                        .HasForeignKey("BusinessId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Industry", "Industry")
                        .WithMany("Sectors")
                        .HasForeignKey("IndustryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Business");

                    b.Navigation("Industry");
                });

            modelBuilder.Entity("Domain.Entities.Verification", b =>
                {
                    b.HasOne("Domain.Entities.Business", "Business")
                        .WithMany()
                        .HasForeignKey("BusinessId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Business");
                });

            modelBuilder.Entity("Domain.Entities.Account", b =>
                {
                    b.Navigation("Business")
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Entities.Business", b =>
                {
                    b.Navigation("Branches");

                    b.Navigation("Images");

                    b.Navigation("Participations");

                    b.Navigation("Representative");

                    b.Navigation("Sectors");
                });

            modelBuilder.Entity("Domain.Entities.Event", b =>
                {
                    b.Navigation("EventIndustries");

                    b.Navigation("Participations");
                });

            modelBuilder.Entity("Domain.Entities.Industry", b =>
                {
                    b.Navigation("EventIndustries");

                    b.Navigation("Sectors");
                });
#pragma warning restore 612, 618
        }
    }
}
