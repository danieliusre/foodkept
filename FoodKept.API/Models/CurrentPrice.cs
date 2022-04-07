// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.ComponentModel.DataAnnotations.Schema;


namespace FoodKept.Models
{
    public class CurrentPrice
    {
        public CurrentPrice() { }

        [ForeignKey("Food")]
        public int ID { get; set; }
        public double OldPrice { get; set; }
        public double DiscountPrice { get; set; }
        public int DiscountPercent { get; set; }
        public bool IsDiscount { get; set; }
        public virtual Food Food {get; set;}
    }
}
