using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Application.Models
{
    public class SubjectModel
    {
        public int ID { get; set; }
        public string CourseCode { get; set; }
        public string CourseTitle { get; set; }
        public string Semester { get; set; }
    }
}