using System;
using System.Collections.Generic;

namespace For_The_Potatoe_Backend.Models;

public partial class User
{

    public Guid Id { get; set; } = Guid.NewGuid();

    public string Name { get; set; } = null!;

    public string Password { get; set; } = null!;

    public DateTime RegDate { get; set; } = DateTime.Now;

    public DateTime ModDate { get; set; } = DateTime.Now;

    public virtual Save? Save { get; set; }
}
