﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodKept.Models
{
    public class ShoppingCart
    {
        public int Id { get; set; }
        public int FoodId { get; set; }
        public string ApplicationUserId { get; set; }
        public int Quantity { get; set; }
        public bool Reserved { get; set; }
        public virtual Food Food { get; set; }
        public virtual ApplicationUser Customer { get; set; }
    }
}
