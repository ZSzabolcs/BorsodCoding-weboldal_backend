using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AuthApi.Models;

public partial class Velemeny
{
    public string Id { get; set; } = null!;

    [Column(TypeName = "char(1)")]
    public string Ertekeles { get; set; } = null!;

    public string Megjegyzes { get; set; } = null!;

    public DateTime RegDate { get; set; }
    public DateTime ModDate { get; set; }


    [JsonIgnore]
    public virtual ApplicationUser User { get; set; } = null!;
}
