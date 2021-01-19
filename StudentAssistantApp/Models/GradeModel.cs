using System;
using System.Collections.Generic;
using System.Text;

namespace StudentAssistantApp.Models
{
    public class GradeModel
    {
        public int GradeId { get; set; }
        public double GradeValue { get; set; }
        public string Type { get; set; }
        public double Wage { get; set; }
        public DateTime Date { get; set; }
    }
}
