using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataSibTerminal.Models;

public partial class Ticket
{
    public int TicketId { get; set; }
    [Required]
    public string? AnydeskId { get; set; }
    [Required]
    public string? Description { get; set; }
    
    public bool IsSolved { get; set; } = false;

    public string? Username { get; set; }

    public int? FkUserId { get; set; }

    public DateTime CreationTime { get; set; }
}
