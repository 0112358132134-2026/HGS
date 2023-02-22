﻿using System;
using System.Collections.Generic;

namespace HGS.Models;

public partial class Speciality
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Doctor> Doctors { get; } = new List<Doctor>();
}
