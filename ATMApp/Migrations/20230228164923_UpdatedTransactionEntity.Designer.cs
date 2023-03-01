﻿// <auto-generated />
using System;
using ATMApp.Domain.DataBase;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ATMApp.Migrations
{
    [DbContext(typeof(AtmDbContext))]
    [Migration("20230228164923_UpdatedTransactionEntity")]
    partial class UpdatedTransactionEntity
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("ATMApp.Domain.Entities.InternalTransfer", b =>
                {
                    b.Property<int>("InternalTransferId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("InternalTransferId"), 1L, 1);

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<long>("ReciepeintBankAccountNumber")
                        .HasColumnType("bigint");

                    b.Property<string>("RecipientBankAccountName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("TimeStamp")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.Property<decimal>("TransferAmount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime?>("Updated")
                        .HasColumnType("datetime2");

                    b.HasKey("InternalTransferId");

                    b.ToTable("Transfers");
                });

            modelBuilder.Entity("ATMApp.Domain.Entities.Transaction", b =>
                {
                    b.Property<int>("TransactionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TransactionId"), 1L, 1);

                    b.Property<string>("Descriprion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("TransactionAmount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("TransactionDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("TransactionType")
                        .HasColumnType("int");

                    b.Property<int>("UserAccountId")
                        .HasColumnType("int");

                    b.HasKey("TransactionId");

                    b.HasIndex("UserAccountId");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("ATMApp.Domain.Entities.UserAccount", b =>
                {
                    b.Property<int>("UserAccountId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserAccountId"), 1L, 1);

                    b.Property<decimal>("AccountBalance")
                        .HasColumnType("decimal(18,2)");

                    b.Property<long>("AccountNumber")
                        .HasColumnType("bigint");

                    b.Property<long>("CardNumber")
                        .HasColumnType("bigint");

                    b.Property<int>("CardPin")
                        .HasColumnType("int");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("FullName")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("IsLocked")
                        .HasColumnType("bit");

                    b.Property<byte[]>("TimeStamp")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.Property<int>("TotalLogin")
                        .HasColumnType("int");

                    b.Property<DateTime?>("Updated")
                        .HasColumnType("datetime2");

                    b.HasKey("UserAccountId");

                    b.HasIndex(new[] { "FullName", "AccountBalance" }, "IX_Task_Name");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ATMApp.Domain.Entities.Transaction", b =>
                {
                    b.HasOne("ATMApp.Domain.Entities.UserAccount", "UserAccount")
                        .WithMany("Transactions")
                        .HasForeignKey("UserAccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserAccount");
                });

            modelBuilder.Entity("ATMApp.Domain.Entities.UserAccount", b =>
                {
                    b.Navigation("Transactions");
                });
#pragma warning restore 612, 618
        }
    }
}