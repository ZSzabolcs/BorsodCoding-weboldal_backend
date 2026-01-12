using System;
using System.Collections.Generic;

namespace For_The_Potato_Backend.Models;

public partial class Save
{
    public string Id { get; set; } = null!;

    public int Points { get; set; }

    public int Level { get; set; }

    public string Language { get; set; } = null!;

    public DateTime RegDate { get; set; } = DateTime.Now;

    public DateTime ModDate { get; set; } = DateTime.Now;

    public virtual Aspnetuser IdNavigation { get; set; } = null!;
}
