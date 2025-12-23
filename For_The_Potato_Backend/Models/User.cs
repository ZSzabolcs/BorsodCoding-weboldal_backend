using System;
using System.Collections.Generic;

namespace For_The_Potato_Backend.Models;

public partial class User
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string Password { get; set; } = null!;

    public DateTime RegDate { get; set; }

    public DateTime ModDate { get; set; }

    public string? Email { get; set; }

    public virtual Save? Save { get; set; }
}
