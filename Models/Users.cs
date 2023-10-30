﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DataSibTerminal.Models;

[Table("users")]
[Index("Email", Name = "users_email_key", IsUnique = true)]
public partial class Users
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    [StringLength(30)]
    public string Name { get; set; }

    [Required]
    [EmailAddress]
    [Column("email")]
    [StringLength(50)]
    public string Email { get; set; }
    [Required]
    [Column("password")]
    [StringLength(30)]
    public string Password { get; set; }
}