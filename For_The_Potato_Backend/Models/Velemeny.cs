using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace For_The_Potato_Backend.Models;

public partial class Velemeny
{
    public string Id { get; set; } = null!;

    public string Ertekeles { get; set; } = null!;

    public string Megjegyzes { get; set; } = null!;

    [JsonIgnore]
    public virtual Aspnetuser IdNavigation { get; set; } = null!;
}
