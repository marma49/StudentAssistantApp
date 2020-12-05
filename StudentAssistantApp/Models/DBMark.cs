using System;
using System.Collections.Generic;
using System.Text;

namespace StudentAssistantApp.Models
{
    public class DBMark
    {
        public int DBMarkId { get; set; }
        public string Subject { get; set; }
        public int Mark { get; set; }
        public int Semester { get; set; }
        public DateTime Date { get; set; }
    }
}
