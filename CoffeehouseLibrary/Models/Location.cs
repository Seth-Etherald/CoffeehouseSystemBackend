using System;
using System.Collections.Generic;

namespace CoffeehouseLibrary.Models;

public partial class Location
{
    public int LocationId { get; set; }

    public string? PlusCode { get; set; }

    public int? UserId { get; set; }

    public virtual User? User { get; set; }
}
