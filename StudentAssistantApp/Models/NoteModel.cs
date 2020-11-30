using System;
using System.Collections.Generic;
using System.Text;

namespace StudentAssistantApp.Models
{
    public class NoteModel
    {
        public string NoteName { get; set; }
        public string NoteContent { get; set; }
        public string NoteCategory { get; set; }
        public int NoteId { get; set; } = 0;
    }
}
