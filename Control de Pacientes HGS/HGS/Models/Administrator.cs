using System;
using System.Collections.Generic;

namespace HGS.Models;

public partial class Administrator
{
    public int Id { get; set; }

    public string User { get; set; } = null!;

    public string Password { get; set; } = null!;
}
