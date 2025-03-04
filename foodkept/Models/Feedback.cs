﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodKept.Models
{
    public class Feedback
    {
        public int ID { get; set; }
        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        public int Points { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string RestaurantName { get; set; }
        public string Email { get; set; }
        public string Answer { get; set; }
    }
}
