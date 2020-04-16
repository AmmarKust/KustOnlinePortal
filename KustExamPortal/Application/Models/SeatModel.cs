using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Application.Models
{
    public class SeatModel
    {
        public int ID { get; set; }
        public string Room { get; set; }
        public string Time { get; set; }
        public string SeatNo { get; set; }
        public string Paper { get; set; }
        public string RegistrationNo { get; set; }

        public virtual Authentication Authentication { get; set; }
    }
}