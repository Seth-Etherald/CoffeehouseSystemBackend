using System;
using System.Collections.Generic;

namespace CoffeehouseLibrary.Models;

public partial class Banner
{
    public int BannerId { get; set; }

    public int? UserId { get; set; }

    public string? ImageUrl { get; set; }

    public virtual User? User { get; set; }
}
