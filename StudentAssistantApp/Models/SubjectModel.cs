using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentAssistantApp.Models
{
    public class SubjectModel
    {
        public string SubjectName { get; set; }
        private BindableCollection<GradeModel> _grades = new BindableCollection<GradeModel>();

        public SubjectModel(string SubjectName, BindableCollection<GradeModel> Grades)
        {
            this.SubjectName = SubjectName;
            this.Grades = Grades;
        }

        public BindableCollection<GradeModel> Grades
        {
            get { return _grades; }
            set { _grades = value; }
        }
    }
}
