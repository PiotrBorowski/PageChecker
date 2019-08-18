﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PageCheckerAPI.DataAccess;

namespace PageCheckerAPI.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.0-rtm-35687")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("PageCheckerAPI.Models.Page", b =>
                {
                    b.Property<Guid>("PageId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Body")
                        .HasColumnType("ntext");

                    b.Property<string>("BodyDifference")
                        .HasColumnType("ntext");

                    b.Property<int>("CheckingType");

                    b.Property<bool>("HasChanged");

                    b.Property<string>("Name");

                    b.Property<TimeSpan>("RefreshRate");

                    b.Property<bool>("Stopped");

                    b.Property<string>("Url")
                        .IsRequired();

                    b.Property<Guid>("UserId");

                    b.HasKey("PageId");

                    b.HasIndex("UserId");

                    b.ToTable("Pages");
                });

            modelBuilder.Entity("PageCheckerAPI.Models.User", b =>
                {
                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<byte[]>("PasswordHash");

                    b.Property<byte[]>("PasswordSalt");

                    b.Property<string>("UserName")
                        .IsRequired();

                    b.Property<bool>("Verified");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("PageCheckerAPI.Models.Page", b =>
                {
                    b.HasOne("PageCheckerAPI.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
