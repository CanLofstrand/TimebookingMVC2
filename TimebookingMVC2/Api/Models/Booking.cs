﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TimebookingMVC2.Api.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public DateTime Date { get; set; }
    }
}