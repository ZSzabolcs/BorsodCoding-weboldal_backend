using System;
using System.Collections.Generic;

namespace For_The_Potatoe_Backend.Models;

public partial class User
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Password { get; set; } = null!;

    public DateTime Date { get; set; }

    public virtual Save? Save { get; set; }
}
