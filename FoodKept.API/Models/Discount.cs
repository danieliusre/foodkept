// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodKept.Models
{
    public class Discount
    {
        public int ID { get; set; }
        public double DiscountPrice { get; set; }
        public int DiscountPercent { get; set; }
        public TimeSpan FromTime { get; set; }
        public TimeSpan ToTime { get; set; }
        public int FoodId { get; set; }
        public virtual Food Food { get; set; }
    }
}
