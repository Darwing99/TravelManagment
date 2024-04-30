﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string Name { get; set; } = "";
        public string Address { get; set; } = "";
        public bool Status { get; set; } = true;
    }

}