using System;
using System.Collections.Generic;

namespace DataSibTerminal.Models;

public partial class Ticket
{
    public int TicketId { get; set; }

    public string? AnydeskId { get; set; }

    public string? Description { get; set; }

    public bool? IsSolved { get; set; }

    public string? Username { get; set; }

    public int? FkUserId { get; set; }

    public DateTime CreationTime { get; set; }
}
