using System;
using System.Collections.Generic;

namespace IPChecker.Models;

public partial class IpAddress
{
    public int Id { get; set; }

    public int CountryId { get; set; }

    public string Ip { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual Country Country { get; set; } = null!;
}
