using System;
using System.Collections.Generic;

namespace AuthApi.Models;

public partial class Nyelvarany
{
    public string Language { get; set; } = null!;

    public decimal? Szazalek { get; set; }
}
