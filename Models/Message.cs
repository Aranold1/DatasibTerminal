using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataSibTerminal.Models;

public partial class Message
{
    public int MessageId { get; set; }

    public int? FkTicketId { get; set; }

    public string? UserRole { get; set; }
    [Required]
    public string? Message1 { get; set; }

    public DateTime? SendTime { get; set; }
}
