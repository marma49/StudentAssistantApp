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

        private BindableCollection<String> typesOfGrades = new BindableCollection<String>();

        private string newSubject = "";
        private string helperTextGrade = "";
        private SubjectModel chosenSubject;
        private bool isDialogOpen = false;
        private string grade = "";
        private string wage = "";
        private double minGrade = 1;
        private double maxGrade = 100;
        private double minWage = 0;
        private double maxWage = 100;
        private int selectedItemToRemove = -1;
        private string selectedType = "inne";

        public GradesWindowViewModel()
        {
            using (var context = new StudentAppContext())
            {
                //Pobranie i wyświetlenie przedmiotow i ocen
                var dbsubjects = context.DBSubjects.ToList();
                var dbmarks = context.DBMarks.ToList();
                foreach (DBSubject s1 in dbsubjects)
                {
                    var lista = dbmarks.Where(x => x.Subject == s1.SubjectName).ToList();
                    BindableCollection<GradeModel> gm1 = new BindableCollection<GradeModel>();
                    foreach (DBMark mark1 in lista)
                    {
                        gm1.Add(new GradeModel { Date = mark1.Date, GradeValue = mark1.Mark });
                    }

                    Subjects.Add(new SubjectModel
                    {
                        SubjectName = s1.SubjectName,
                        Grades = gm1
                    });

                }

                //Dodawanie typów ocen do TypesOfGrades
                TypesOfGrades.Add("sprawdzian");
                TypesOfGrades.Add("kartkówka");
                TypesOfGrades.Add("odpowiedź ustna");
                TypesOfGrades.Add("zadanie");
                TypesOfGrades.Add("aktywność");
                TypesOfGrades.Add("inne");
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

        public BindableCollection<String> TypesOfGrades
        {
            get { return typesOfGrades; }
            set { typesOfGrades = value; }
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
                Grade = "";
                Wage = "";
                NotifyOfPropertyChange("IsDialogOpen");
            }
        }

        public string Grade
        {
            get
            {
                return grade;
            }
            set
            {
                try
                {
                    if (Convert.ToDouble(value) < minGrade || Convert.ToDouble(value) > maxGrade)
                    {
                        HelperTextGrade = $"Put value from interval {minGrade} - {maxGrade}";
                        NotifyOfPropertyChange("HelperTextGrade");
                    }
                    else
                    {
                        HelperTextGrade = "";
                    }
                }
                catch
                {
                    HelperTextGrade = $"Put value from interval {minGrade} - {maxGrade}";
                }

                grade = value;
                NotifyOfPropertyChange("Grade");
            }
        }

        public string Wage // Na razie tu nic nie dodaję z HelperTextGrade
        {
            get
            {
                return wage;
            }
            set
            {
                try
                {
                    if (Convert.ToDouble(value) < minWage || Convert.ToDouble(value) > maxWage)
                    {

                    }
                    else
                    {

                    }
                }
                catch
                {

                }

                wage = value;
                NotifyOfPropertyChange("Wage");
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
        }

        public double MaxGrade
        {
            get { return maxGrade; }
        }

        public double MinWage
        {
            get { return minWage; }
        }

        public double MaxWage
        {
            get { return maxWage; }
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

        public string SelectedType
        {
            get { return selectedType; }
            set { selectedType = value; }
        }

        public void AddSubject()
        {
            if (NewSubject.Length > 0 && !Subjects.Any(n => n.SubjectName == NewSubject))
            {
                //Zapisanie przedmiotu do bazy danych
                var dbsubject = new DBSubject { SubjectName = newSubject };
                using (var context = new StudentAppContext())
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

        public void AddNewGrade(string grade, string wage)
        {
            //Dodanie oceny do bazy danych
            var dbmark = new DBMark { Mark = int.Parse(Grade), Date = DateTime.Now, Subject = newSubject }; // Error przy dodawaniu liczb z przecinkiem, mark by trzeba było na double zamienić. Z kropką liczb też na razie nie można dodać
            using (var context = new StudentAppContext())
            {
                context.DBMarks.Add(dbmark);

                context.SaveChanges();
            }
            chosenSubject.Grades.Add(new GradeModel { Date = DateTime.Now, GradeValue = Convert.ToDouble(Grade), Wage = Convert.ToDouble(Wage), Type = SelectedType });
            Grade = "";
            Wage = "";
            selectedType = "inne";
            IsDialogOpen = false;
            HelperTextGrade = "";
        }

        public bool CanAddNewGrade(string grade, string wage)
        {
            try
            {
                if (Convert.ToDouble(grade) < minGrade || Convert.ToDouble(grade) > maxGrade || Convert.ToDouble(wage) < minWage || Convert.ToDouble(wage) > maxWage)
                    return false;
                else
                    return true;
            }
            catch
            {
                return false;
            }
        }

        public void OpenDialog(string subjectName)
        {
            IsDialogOpen = true;
            chosenSubject = Subjects.First(n => n.SubjectName == subjectName);

        }
    
        public void EditGrade(object sender)
        {
            System.Windows.MessageBox.Show("I Edit grade");
        }

        public void DeleteGrade(object sender)
        {
            System.Windows.MessageBox.Show("I Delete grade");
        }


    }
}


