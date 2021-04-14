﻿using MisGastos.Core.Enumerations;
using System;
using System.Collections.Generic;
using System.Text;

namespace MisGastos.Core.DTOs
{
    public class SecurityDto
    {
        public string User { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public RoleType? Role { get; set; }
    }
}
