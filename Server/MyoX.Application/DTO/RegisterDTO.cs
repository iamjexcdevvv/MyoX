﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyoX.Application.DTO
{
    public record RegisterDTO(string Email, string Password, string ConfirmPassword);
}
