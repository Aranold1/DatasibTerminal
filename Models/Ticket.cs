﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DataSibTerminal.Models;

[Table("ticket")]
public partial class Ticket
{
    [Column("issolved")]
    public bool? Issolved { get; set; } = false;

    [Required]
    [Column("description")]
    [StringLength(2600)]
    public string Description { get; set; }

    [Required]
    [Column("anydesk_id")]
    [StringLength(12)]
    public string AnydeskId { get; set; }

    [Key]
    [Column("creation_time")]
    public double CreationTime { get; set; }
}