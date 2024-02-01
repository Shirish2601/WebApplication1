﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MachineManagement.Models
{
    public class Machine
    {
        public string? MachineName { get; set; }  = string.Empty;
        public List<Asset> Assets { get; set; } = new();
    }
}
