﻿// <auto-generated />
using System;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(BreweryContext))]
    [Migration("20221010150354_EntitiesDesign")]
    partial class EntitiesDesign
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Domain.AggregatesModel.BeerAggregate.Beer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("AlcoholContent")
                        .HasColumnType("float");

                    b.Property<Guid>("BreweryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Deleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<double>("Quantity")
                        .HasColumnType("float");

                    b.Property<double>("SellPrice")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("BreweryId");

                    b.ToTable("Beer");
                });

            modelBuilder.Entity("Domain.AggregatesModel.BreweryAggregate.Brewery", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Deleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Brewery");
                });

            modelBuilder.Entity("Domain.AggregatesModel.InvoiceAggregate.Invoice", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("BreweryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("WholesalerId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("BreweryId");

                    b.HasIndex("WholesalerId");

                    b.ToTable("Invoice");
                });

            modelBuilder.Entity("Domain.AggregatesModel.OrderAggregate.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("BeerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("InvoiceId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("Quantity")
                        .HasColumnType("float");

                    b.Property<double>("UnityPrice")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("BeerId");

                    b.HasIndex("InvoiceId");

                    b.ToTable("Order");
                });

            modelBuilder.Entity("Domain.AggregatesModel.WholesalerAggregate.Wholesaler", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Deleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Wholesaler");
                });

            modelBuilder.Entity("Domain.AggregatesModel.WholesalerStockAggregate.WholesalerStock", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("BeerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<double>("Quantity")
                        .HasColumnType("float");

                    b.Property<Guid>("WholesalerId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("BeerId");

                    b.HasIndex("WholesalerId");

                    b.ToTable("WholesalerStock");
                });

            modelBuilder.Entity("Infrastructure.EventLogEF.IntegrationEventLogEntry", b =>
                {
                    b.Property<Guid>("EventId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("EntityId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EventTypeName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("TransactionId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("EventId");

                    b.ToTable("IntegrationEventLogEntry");
                });

            modelBuilder.Entity("Domain.AggregatesModel.BeerAggregate.Beer", b =>
                {
                    b.HasOne("Domain.AggregatesModel.BreweryAggregate.Brewery", "Brewery")
                        .WithMany("Beers")
                        .HasForeignKey("BreweryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Brewery");
                });

            modelBuilder.Entity("Domain.AggregatesModel.InvoiceAggregate.Invoice", b =>
                {
                    b.HasOne("Domain.AggregatesModel.BreweryAggregate.Brewery", "Brewery")
                        .WithMany("Invoices")
                        .HasForeignKey("BreweryId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Domain.AggregatesModel.WholesalerAggregate.Wholesaler", "Wholesaler")
                        .WithMany("Invoices")
                        .HasForeignKey("WholesalerId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Brewery");

                    b.Navigation("Wholesaler");
                });

            modelBuilder.Entity("Domain.AggregatesModel.OrderAggregate.Order", b =>
                {
                    b.HasOne("Domain.AggregatesModel.BeerAggregate.Beer", "Beer")
                        .WithMany("Orders")
                        .HasForeignKey("BeerId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Domain.AggregatesModel.InvoiceAggregate.Invoice", "Invoice")
                        .WithMany("Orders")
                        .HasForeignKey("InvoiceId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Beer");

                    b.Navigation("Invoice");
                });

            modelBuilder.Entity("Domain.AggregatesModel.WholesalerStockAggregate.WholesalerStock", b =>
                {
                    b.HasOne("Domain.AggregatesModel.BeerAggregate.Beer", "Beer")
                        .WithMany("WholesalerStocks")
                        .HasForeignKey("BeerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.AggregatesModel.WholesalerAggregate.Wholesaler", "Wholesaler")
                        .WithMany("WholesalerStocks")
                        .HasForeignKey("WholesalerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Beer");

                    b.Navigation("Wholesaler");
                });

            modelBuilder.Entity("Domain.AggregatesModel.BeerAggregate.Beer", b =>
                {
                    b.Navigation("Orders");

                    b.Navigation("WholesalerStocks");
                });

            modelBuilder.Entity("Domain.AggregatesModel.BreweryAggregate.Brewery", b =>
                {
                    b.Navigation("Beers");

                    b.Navigation("Invoices");
                });

            modelBuilder.Entity("Domain.AggregatesModel.InvoiceAggregate.Invoice", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("Domain.AggregatesModel.WholesalerAggregate.Wholesaler", b =>
                {
                    b.Navigation("Invoices");

                    b.Navigation("WholesalerStocks");
                });
#pragma warning restore 612, 618
        }
    }
}
