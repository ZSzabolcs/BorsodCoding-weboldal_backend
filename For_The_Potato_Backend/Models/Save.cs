using System;
using System.Collections.Generic;

namespace For_The_Potato_Backend.Models;

public partial class Save
{
    public Guid Id { get; set; }

    public int Points { get; set; }

    public int Level { get; set; }

    public string Language { get; set; } = null!;

    public DateTime RegDate { get; set; }

    public DateTime ModDate { get; set; }

    public virtual User IdNavigation { get; set; } = null!;
}
