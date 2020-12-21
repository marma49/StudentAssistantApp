using System;
using System.Collections.Generic;
using System.Text;
using StudentAssistantApp.Models;

namespace StudentAssistantApp.ModelsDB
{
    public class DBSubject
    {
        public int DBSubjectId { get; set; }
        public string SubjectName { get; set; }
        public virtual ICollection<DBMark> Marks { get; set; }
    }
}
