﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace DataSibTerminal.Models
{

    [Table("ticket")]
    public partial class Ticket
    {
        [Column("ticket_id")]
        public int ticket_id { get; set; }
        [Display(Name = "опишите вашу проблему")]
        [Required]
        [Column("description")]
        public string Description { get; set; }

        [Display(Name = "укажите ваш anydesk id")]
        [Required]
        [Column("anydesk_id")]
        [StringLength(12, MinimumLength = 12)]
        
        public string AnydeskId { get; set; }
        [Column("username")]
        [StringLength(50)]
        public string Username { get; set; }
        [Column("creation_time")]
        public DateTime CreationTime { get; set; }

        [Column("is_solved")]
        public bool IsSolved { get; set; }

        [Column("fk_user_id")]
        public int UserId { get; set; }
    }
}

