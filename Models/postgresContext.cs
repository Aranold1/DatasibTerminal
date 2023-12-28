﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DataSibTerminal.Models;

public partial class postgresContext : DbContext
{
    public postgresContext(DbContextOptions<postgresContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Ticket> Ticket { get; set; }

    public virtual DbSet<Users> Users { get; set; }

    public virtual DbSet<Message> Message { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Ticket>(entity =>
        {
            entity.HasKey(e => e.CreationTime).HasName("ticket_pkey");
        });

        modelBuilder.Entity<Users>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("users_pkey");
        });

        modelBuilder.Entity<Message>(entity =>
        {
            entity.HasKey(e => e.Fk_ticket_id).HasName("messages_pkey");
        });
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}