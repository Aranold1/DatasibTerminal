using System;
using System.Collections.Generic;

namespace DataSibTerminal.Models;

public partial class Message
{
    public int MessageId { get; set; }

    public int? FkTicketId { get; set; }

    public string? UserRole { get; set; }

    public string? Message1 { get; set; }

    public DateTime? SendTime { get; set; }
}
