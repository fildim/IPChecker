﻿using System;
using System.Collections.Generic;

namespace IPChecker.Models;

public partial class Country
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string TwoLetterCode { get; set; } = null!;

    public string ThreeLetterCode { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<IpAddress> IpAddresses { get; set; } = new List<IpAddress>();
}
