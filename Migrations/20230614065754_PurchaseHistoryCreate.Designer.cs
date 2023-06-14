﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ShoppingMvcApp.Migrations
{
    [DbContext(typeof(ShoppingMvcAppContext))]
    [Migration("20230614065754_PurchaseHistoryCreate")]
    partial class PurchaseHistoryCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.32")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("ShoppingMvcApp.Models.Product", b =>
                {
                    b.Property<int>("productId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("count")
                        .HasColumnType("int");

                    b.Property<string>("create_date")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("image_url")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("price")
                        .HasColumnType("int");

                    b.Property<string>("productName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("productId");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("ShoppingMvcApp.Models.PurchaseHistory", b =>
                {
                    b.Property<int>("PurchaseHistoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("count")
                        .HasColumnType("int");

                    b.Property<int>("detailsId")
                        .HasColumnType("int");

                    b.Property<int>("productId")
                        .HasColumnType("int");

                    b.Property<string>("purchaseDate")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("userId")
                        .HasColumnType("int");

                    b.HasKey("PurchaseHistoryId");

                    b.HasIndex("productId");

                    b.HasIndex("userId");

                    b.ToTable("PurchaseHistory");
                });

            modelBuilder.Entity("ShoppingMvcApp.Models.User", b =>
                {
                    b.Property<int>("userId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("address")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("mail")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("password")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("tel")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("userId");

                    b.ToTable("User");
                });

            modelBuilder.Entity("ShoppingMvcApp.Models.PurchaseHistory", b =>
                {
                    b.HasOne("ShoppingMvcApp.Models.Product", null)
                        .WithMany("purchaseHistorys")
                        .HasForeignKey("productId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ShoppingMvcApp.Models.User", null)
                        .WithMany("purchaseHistorys")
                        .HasForeignKey("userId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
