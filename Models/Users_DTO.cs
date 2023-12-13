using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;



namespace DataSibTerminal.Models
{
    public class Users_DTO
    {
        [Required]
        [Column("email")]
        [StringLength(50)]
        public string Email { get; set; }

        [Required]
        [Column("password")]
        [StringLength(30)]
        public string Password { get; set; }
    }
}
