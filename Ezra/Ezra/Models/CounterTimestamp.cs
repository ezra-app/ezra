﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ezra.Models
{
    public class CounterTimestamp : BaseModel
    {
        public DateTime InitialTime { get; set; }
        public bool Started { get; set; }
    }
}
