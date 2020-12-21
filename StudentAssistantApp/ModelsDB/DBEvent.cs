using System;
using System.Collections.Generic;
using System.Text;

namespace StudentAssistantApp.ModelsDB
{
    public class DBEvent
    {
        public int DBEventId { get; set; }
        public string EventTitle { get; set; }
        public string EventContent { get; set; }
        public DateTime DateEvent { get; set; }
    }
}
