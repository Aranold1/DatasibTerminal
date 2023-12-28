using System;
using System.Collections.Generic;

namespace DataSibTerminal.Models;

public partial class User
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Password { get; set; }

    public string? Role { get; set; }
}
