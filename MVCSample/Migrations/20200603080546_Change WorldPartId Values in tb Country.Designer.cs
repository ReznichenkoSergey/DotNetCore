﻿// <auto-generated />
using System;
using MVCSample.Models.Infestation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MVCSample.Migrations
{
    [DbContext(typeof(InfestationContext))]
    [Migration("20200603080546_Change WorldPartId Values in tb Country")]
    partial class ChangeWorldPartIdValuesintbCountry
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MVCSample.Models.Infestation.Country", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("DeadCount")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Population")
                        .HasColumnType("int");

                    b.Property<int>("RecoveredCount")
                        .HasColumnType("int");

                    b.Property<int>("SickCount")
                        .HasColumnType("int");

                    b.Property<bool>("Vaccine")
                        .HasColumnType("bit");

                    b.Property<int?>("WorldPartId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("WorldPartId");

                    b.ToTable("Countries");
                });

            modelBuilder.Entity("MVCSample.Models.Infestation.Human", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<int>("CountryId")
                        .HasColumnType("int");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Gender")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsSick")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.ToTable("Humans");
                });

            modelBuilder.Entity("MVCSample.Models.Infestation.News", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AuthorId")
                        .HasColumnType("int");

                    b.Property<bool>("IsFake")
                        .HasColumnType("bit");

                    b.Property<string>("Text")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.ToTable("News");
                });

            modelBuilder.Entity("MVCSample.Models.Infestation.WorldPart", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("WorldParts");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Africa"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Eurasia"
                        },
                        new
                        {
                            Id = 3,
                            Name = "North America"
                        },
                        new
                        {
                            Id = 4,
                            Name = "South America"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Antarctica"
                        },
                        new
                        {
                            Id = 6,
                            Name = "Australia"
                        });
                });

            modelBuilder.Entity("MVCSample.Models.Infestation.Country", b =>
                {
                    b.HasOne("MVCSample.Models.Infestation.WorldPart", "WorldPart")
                        .WithMany("Countries")
                        .HasForeignKey("WorldPartId");
                });

            modelBuilder.Entity("MVCSample.Models.Infestation.Human", b =>
                {
                    b.HasOne("MVCSample.Models.Infestation.Country", "Country")
                        .WithMany("Humans")
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MVCSample.Models.Infestation.News", b =>
                {
                    b.HasOne("MVCSample.Models.Infestation.Human", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
