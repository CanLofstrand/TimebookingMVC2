using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TimebookingMVC2.Models
{
    public class BookingModel
    {
        [Required(ErrorMessage = "Date required")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Start date")]
        public DateTime Starting_Date { get; set; }

        [Required(ErrorMessage = "Time required")]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:H:mm}", ApplyFormatInEditMode = true)]
        [Display(Name = "Start time")]
        public DateTime Start_Time { get; set; }

        [Required(ErrorMessage = "Date required")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "End date")]
        public DateTime Ending_Date { get; set; }

        [Required(ErrorMessage = "Time required")]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:H:mm}", ApplyFormatInEditMode = true)]
        [Display(Name = "End time")]
        public DateTime Ending_Time { get; set; }

        [Required(ErrorMessage = "Notes required")]
        [Display(Name = "Notes")]
        public string Notes { get; set; }
    }
}