using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TimebookingMVC2.Api.Models
{
    public class BookingWEndDate
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public DateTime Date { get; set; }
        public DateTime EndDate { get; set; }
    }
}