using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace For_The_Potato_Backend.Models;

public partial class Save
{
    public Guid Id { get; set; }

    public int Points { get; set; }

    public int Level { get; set; }

    public string Language { get; set; }

    public DateTime RegDate { get; set; } = DateTime.Now;

    public DateTime ModDate { get; set; } = DateTime.Now;

    public virtual User User { get; set; }
}
