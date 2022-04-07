// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FoodKept.Models
{
    public class Tokens
    {
        [Key]
        public string Username { get; set; }
        public string Token { get; set; }
    }
}
