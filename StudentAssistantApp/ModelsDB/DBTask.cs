using System;
using System.Collections.Generic;
using System.Text;

namespace StudentAssistantApp.Models
{
    public class DBTask
    {
        public int DBTaskId { get; set; }
        public string TaskHeadline { get; set; }
        public string TaskText { get; set; }
        public Nullable<DateTime> Deadline { get; set; }
    }
}
