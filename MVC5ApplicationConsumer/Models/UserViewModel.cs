﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC5ApplicationConsumer.Models
{
    public class UserViewModel
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Contact { get; set; }
    }
}