// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using System.Linq;

namespace FoodKept.Models
{
    public class Food : IComparable<Food>
    {
        private int _quantity;

        public int ID { get; set; }
        public string FoodName { get; set; }
        public double Price { get; set; }
        public int Quantity
        {
            get { return _quantity; }
            set
            {
                if (value >= 20) { _quantity = 20; }
                else { _quantity = value; }
            }
        }
        public string FoodCategory { get; set; }
        public string RestaurantName { get; set; }
        public int Discount { get; set; }
        public byte[] FoodImage { get; set; }
        public string ApplicationUserId { get; set; }
        //Compare Food by quantity
        public int CompareTo(Food other)
        {
            return Quantity.CompareTo(other.Quantity);
        }
    }
}
