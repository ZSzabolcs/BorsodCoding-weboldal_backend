using System;
using System.Collections.Generic;

namespace For_The_Potatoe_Backend.Models;

public partial class Save
{
    public int UserId { get; set; }

    public int Points { get; set; }

    public int Level { get; set; }

    public string Language { get; set; } = null!;

    public DateTime Date { get; set; }

    public virtual User User { get; set; } = null!;
}
