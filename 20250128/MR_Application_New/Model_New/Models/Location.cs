using System;
using System.Collections.Generic;

namespace Model_New.Models;

public partial class Location
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public decimal Latitude { get; set; }

    public decimal Longitude { get; set; }
}
