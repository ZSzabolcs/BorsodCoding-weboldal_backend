using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AuthApi.Models;

public partial class Velemeny
{
    public string Id { get; set; } = null!;

    public string Ertekeles { get; set; } = null!;

    public string Megjegyzes { get; set; } = null!;


    [JsonIgnore]
    public virtual ApplicationUser User { get; set; } = null!;
}
