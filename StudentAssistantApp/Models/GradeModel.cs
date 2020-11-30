using System;
using System.Collections.Generic;
using System.Text;

namespace StudentAssistantApp.Models
{
    public class GradeModel
    {
        public double GradeValue { get; set; }
        public double Weight { get; set; } = 1;
        public DateTime Date { get; set; }

        public GradeModel(double GradeValue, double Weight, DateTime Date)
        {
            this.GradeValue = GradeValue;
            this.Weight = Weight;
            this.Date = Date;
        }
    }
}
