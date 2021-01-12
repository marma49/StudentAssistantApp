using Caliburn.Micro;
using StudentAssistantApp.Models;
using StudentAssistantApp.ModelsDB;
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
        private string helperTextGrade = "";
        private SubjectModel chosenSubject;
        private bool isDialogOpen = false;
        private double grade = 0;
        private double minGrade = 1;
        private double maxGrade = 5;
        private int selectedItemToRemove = -1;

        public GradesWindowViewModel()
        {
            using(var context = new StudentAppContext())
            {
                //Pobranie i wyświetlenie przedmiotow i ocen
                var dbsubjects = context.DBSubjects.ToList();
                var dbmarks = context.DBMarks.ToList();
                foreach(DBSubject s1 in dbsubjects)
                {
                    var lista = dbmarks.Where(x => x.Subject == s1.SubjectName).ToList();
                    BindableCollection<GradeModel> gm1 = new BindableCollection<GradeModel>();
                    foreach (DBMark mark1 in lista)
                    {
                        gm1.Add( new GradeModel { Date = mark1.Date, GradeValue = mark1.Mark });
                    }
                    
                        Subjects.Add(new SubjectModel
                        {
                            SubjectName = s1.SubjectName,
                            Grades = gm1
                        });
                    
                }
            }
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
                if (value < minGrade || value > maxGrade)
                {
                    HelperTextGrade = $"Put value from interval {minGrade} - {maxGrade}";
                }
                else
                {
                    HelperTextGrade = "";
                }

                grade = value;
                NotifyOfPropertyChange("Grade");
                NotifyOfPropertyChange("HelperTextGrade");
            }
        }

        public string HelperTextGrade
        {
            get
            {
                return helperTextGrade;
            }
            set
            {
                helperTextGrade = value;
                NotifyOfPropertyChange("HelperTextGrade");
            }
        }

        public double MinGrade
        {
            get { return minGrade; }
            set { minGrade = value; }
        }

        public double MaxGrade
        {
            get { return maxGrade; }
            set { maxGrade = value; }
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
                //Zapisanie przedmiotu do bazy danych
                var dbsubject = new DBSubject { SubjectName = newSubject}; 
                using(var context = new StudentAppContext())
                {
                    context.DBSubjects.Add(dbsubject);

                    context.SaveChanges();
                }
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

        public void AddNewGrade(double grade)
        {
            //Dodanie oceny do bazy danych
            var dbmark = new DBMark { Mark = int.Parse(Grade.ToString()), Date = DateTime.Now, Subject = newSubject };
            using(var context = new StudentAppContext())
            {
                context.DBMarks.Add(dbmark);

                context.SaveChanges();
            }
            chosenSubject.Grades.Add(new GradeModel { Date = DateTime.Now, GradeValue = Grade });
            Grade = 0;
            IsDialogOpen = false;
            HelperTextGrade = "";
        }

        public bool CanAddNewGrade(double grade)
        {
            if (grade < minGrade || grade > maxGrade)
            {
                return false;
            }
            return true;
        }

        public void OpenDialog(string subjectName)
        {
            IsDialogOpen = true;
            chosenSubject = Subjects.First(n => n.SubjectName == subjectName);
        }
    }


}


