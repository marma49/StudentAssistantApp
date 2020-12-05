using Caliburn.Micro;
using StudentAssistantApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace StudentAssistantApp.ViewModels
{

    class GradesWindowViewModel : Screen
    {
        private BindableCollection<SubjectModel> subjects = new BindableCollection<SubjectModel>();

        private string newSubject = "";
        private SubjectModel chosenSubject;
        private bool isDialogOpen = false;
        private double grade = 0;
        private int selectedItemToRemove = -1;


        public GradesWindowViewModel()
        {
            Subjects.Add(new SubjectModel
            {
                SubjectName = "Fizyka",
                Grades = new BindableCollection<GradeModel>() {
                new GradeModel { Date = DateTime.Now, GradeValue = 5},
                new GradeModel { Date = DateTime.Now, GradeValue = 2},
            }
            });
        }

        public BindableCollection<SubjectModel> Subjects
        {
            get { return subjects; }
            set { subjects = value; }
        }

        public string NewSubject
        {
            get
            {
                return newSubject;
            }
            set
            {
                newSubject = value;
                NotifyOfPropertyChange(() => NewSubject);
            }
        }

        public bool IsDialogOpen
        {
            get
            {
                return isDialogOpen;
            }
            set
            {
                isDialogOpen = value;
                NotifyOfPropertyChange("IsDialogOpen");
            }
        }

        public double Grade
        {
            get
            {
                return grade;
            }
            set
            {
                grade = value;
                NotifyOfPropertyChange("Grade");
            }
        }

        public int SelectedItemToRemove
        {
            get
            {
                return selectedItemToRemove;
            }
            set
            {
                selectedItemToRemove = value;
                NotifyOfPropertyChange("SelectedItemToRemove");
            }
        }

        public void AddSubject()
        {
            if (NewSubject.Length > 0 && !Subjects.Any(n => n.SubjectName == NewSubject))
            {
                Subjects.Add(new SubjectModel { SubjectName = NewSubject, Grades = new BindableCollection<GradeModel>() });
                NewSubject = "";
            }

        }

        public void RemoveSubject()
        {
            if (SelectedItemToRemove != -1)
            {
                Subjects.RemoveAt(SelectedItemToRemove);
            }
        }

        public void AddNewGrade()
        {
            chosenSubject.Grades.Add(new GradeModel { Date = DateTime.Now, GradeValue = Grade });
            Grade = 0;
            IsDialogOpen = false;
        }

        public void OpenDialog(string subjectName)
        {
            IsDialogOpen = true;
            chosenSubject = Subjects.First(n => n.SubjectName == subjectName);
        }
    }


}


