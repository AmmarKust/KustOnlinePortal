using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Application.Models
{
    public class DSModel
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "This Field is Required")]
        public string Room { get; set; }

        [Required(ErrorMessage = "This Field is Required")]
        [DataType(DataType.Date)]
        public string Date { get; set; }

        [Required(ErrorMessage = "This Field is Required")]
        [DataType(DataType.Time)]
        public string Time { get; set; }

        [Required(ErrorMessage = "This Field is Required")]
        public string Paper { get; set; }

        [Required(ErrorMessage = "This Field is Required")]
        public string Semester { get; set; }
    }
}