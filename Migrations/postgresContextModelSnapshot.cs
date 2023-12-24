﻿// <auto-generated />
using System;
using DataSibTerminal.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DataSibTerminal.Migrations
{
    [DbContext(typeof(postgresContext))]
    partial class postgresContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("DataSibTerminal.Models.Ticket", b =>
                {
                    b.Property<double>("CreationTime")
                        .HasColumnType("double precision")
                        .HasColumnName("creation_time");

                    b.Property<string>("AnydeskId")
                        .IsRequired()
                        .HasMaxLength(12)
                        .HasColumnType("character varying(12)")
                        .HasColumnName("anydesk_id");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(2600)
                        .HasColumnType("character varying(2600)")
                        .HasColumnName("description");

                    b.Property<bool?>("Issolved")
                        .HasColumnType("boolean")
                        .HasColumnName("issolved");

                    b.Property<int>("user_id")
                        .HasColumnType("integer")
                        .HasColumnName("user_id");

                    b.Property<string>("username")
                        .HasColumnType("text")
                        .HasColumnName("username");

                    b.HasKey("CreationTime")
                        .HasName("ticket_pkey");

                    b.ToTable("ticket");
                });

            modelBuilder.Entity("DataSibTerminal.Models.Users", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("email");

                    b.Property<string>("Name")
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)")
                        .HasColumnName("name");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)")
                        .HasColumnName("password");

                    b.HasKey("Id")
                        .HasName("users_pkey");

                    b.HasIndex(new[] { "Email" }, "users_email_key")
                        .IsUnique();

                    b.ToTable("users");
                });
#pragma warning restore 612, 618
        }
    }
}
