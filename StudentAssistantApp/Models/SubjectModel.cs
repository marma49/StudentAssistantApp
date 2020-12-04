using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentAssistantApp.Models
{
    public class SubjectModel
    {
        public string SubjectName { get; set; }
        private BindableCollection<GradeModel> grades = new BindableCollection<GradeModel>();
        public BindableCollection<GradeModel> Grades
        {
            get { return grades; }
            set { grades = value; }
        }
    }
}
