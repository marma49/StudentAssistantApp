using Caliburn.Micro;
using StudentAssistantApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentAssistantApp.ViewModels
{

    class GradesWindowViewModel : Screen
    {
        public BindableCollection<GradeModel> g1 = new BindableCollection<GradeModel>();
        public BindableCollection<GradeModel> g2 = new BindableCollection<GradeModel>();
        public BindableCollection<GradeModel> g3 = new BindableCollection<GradeModel>();

        private BindableCollection<SubjectModel> _subjects = new BindableCollection<SubjectModel>();

        private string _newSubject = "";
        private SubjectModel _trashSubject;

        public GradesWindowViewModel()
        {
            g1.Add(new GradeModel(5, 1, new DateTime(2015, 12, 25)));
            g1.Add(new GradeModel(3, 3, new DateTime(2015, 11, 10)));
            g1.Add(new GradeModel(1, 2, new DateTime(2015, 12, 25)));
            g1.Add(new GradeModel(3, 3, new DateTime(2015, 12, 27)));
            g1.Add(new GradeModel(3, 1, new DateTime(2015, 10, 13)));

            g2.Add(new GradeModel(4, 2, new DateTime(2017, 5, 3)));
            g2.Add(new GradeModel(1, 3, new DateTime(2017, 6, 13)));

            g3.Add(new GradeModel(1, 5, new DateTime(2016, 6, 10)));
            g3.Add(new GradeModel(6, 3, new DateTime(2017, 4, 15)));
            g3.Add(new GradeModel(5, 1, new DateTime(2017, 4, 10)));

            Subjects.Add(new SubjectModel("Matematyka", g1));
            Subjects.Add(new SubjectModel("Fizyka", g2));
            Subjects.Add(new SubjectModel("Geografia", g3));
        }


        public BindableCollection<SubjectModel> Subjects
        {
            get { return _subjects; }
            set { _subjects = value; }
        }

        public string NewSubject
        {
            get
            {
                return _newSubject;
            }
            set
            {
                _newSubject = value;
                NotifyOfPropertyChange(() => NewSubject);
            }
        }

        public SubjectModel TrashSubject
        {
            get
            {
                return _trashSubject;
            }
            set
            {
                _trashSubject = value;
                NotifyOfPropertyChange(() => TrashSubject);
            }
        }

        public void AddSubject()
        {
            if (NewSubject.Length > 0)
                Subjects.Add(new SubjectModel(NewSubject, g2));
            return;
        }

        public void RemoveSubject()
        {
            Subjects.Remove(TrashSubject);
        }


    }


}


