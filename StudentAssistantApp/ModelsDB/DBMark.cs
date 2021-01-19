using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.SqlServer;
using System.Collections.Generic;
using System.Text;
using StudentAssistantApp.ModelsDB;

namespace StudentAssistantApp.Models
{
    public class DBMark
    {
        public int DBMarkId { get; set; }
        public string Subject { get; set; }
        public double Mark { get; set; }
        public int Semester { get; set; }
        public DateTime Date { get; set; }
        //[ForeignKey("Subjects")]
        //public int SubjectId { get; set; }
        public virtual DBSubject DBSubject { get; set; }
    }
}
