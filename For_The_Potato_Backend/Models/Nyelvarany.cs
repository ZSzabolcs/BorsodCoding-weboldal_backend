using System;
using System.Collections.Generic;

namespace For_The_Potato_Backend.Models;

public partial class Nyelvarany
{
    public string Language { get; set; } = null!;

    public decimal? Szazalek { get; set; }
}
