using System;
using System.Collections.Generic;

namespace For_The_Potato_Backend.Models;

public partial class Aspnetuserclaim
{
    public int Id { get; set; }

    public string UserId { get; set; } = null!;

    public string? ClaimType { get; set; }

    public string? ClaimValue { get; set; }

    public virtual Aspnetuser User { get; set; } = null!;
}
