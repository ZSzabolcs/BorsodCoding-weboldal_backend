using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AuthApi.Models;

public partial class Save
{
    public string Id { get; set; } = null!;

    public int Points { get; set; }

    public int Level { get; set; }

    [Column(TypeName = "char(2)")]
    public string Language { get; set; } = null!;

    public DateTime RegDate { get; set; }

    public DateTime ModDate { get; set; }

    [JsonIgnore]
    public virtual ApplicationUser User { get; set; }

}
