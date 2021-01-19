using Caliburn.Micro;
using StudentAssistantApp.Models;
using StudentAssistantApp.ModelsDB;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

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
        private bool isEditing = false;
        private int gradeIndex;
        private string grade = "0";
        private string wage = "0";
        private double minGrade = 1;
        private double maxGrade = 100;
        private double minWage = 0;
        private double maxWage = 100;
        private int selectedItemToRemove = -1;
        private string selectedType = "inne";

        public GradesWindowViewModel()
        {
            //Pobranie i wyświetlenie przedmiotów i ocen z BD
            using (var context = new StudentAppContext())
            {
                var subjects = context.DBSubjects.ToList();
                foreach (DBSubject dbs in subjects)
                {
                    BindableCollection<GradeModel> gm = new BindableCollection<GradeModel>();
                    if (dbs.Marks != null)
                    {
                        var dbmarks = context.DBMarks.ToList().Where(x => x.DBSubject == dbs);
                        foreach (DBMark mark in dbmarks)
                        {
                            gm.Add(new GradeModel
                            {
                                GradeId = mark.DBMarkId,
                                Date = mark.Date,
                                GradeValue = mark.Mark
                            });
                        }
                    }
                    Subjects.Add(new SubjectModel
                    {
                        SubjectName = dbs.SubjectName,
                        Grades = gm
                    });
                }
            }

            TypesOfGrades.Add("sprawdzian");
            TypesOfGrades.Add("kartkówka");
            TypesOfGrades.Add("odpowiedź ustna");
            TypesOfGrades.Add("zadanie");
            TypesOfGrades.Add("aktywność");
            TypesOfGrades.Add("inne");
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
                Grade = "0";
                Wage = "0";
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
                        HelperTextGrade = $"Wpisz liczbę z zakresu {minGrade} - {maxGrade}";
                        NotifyOfPropertyChange("HelperTextGrade");
                    }
                    else
                    {
                        HelperTextGrade = "";
                    }
                }
                catch
                {
                    HelperTextGrade = $"Wpisz liczbę z zakresu {minGrade} - {maxGrade}";
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
                var subject = Subjects.ElementAt(SelectedItemToRemove); // pobieram przedmiot do usunięcia
                Subjects.Remove(subject);

                //Usunięcie przedmiotu i jego ocen z bazy
                using (var context = new StudentAppContext())
                {
                    DBSubject dbsubject = context.DBSubjects.FirstOrDefault(x => x.SubjectName == subject.SubjectName);
                    var dbmarks = context.DBMarks.ToList().Where(x => x.DBSubject == dbsubject);

                    context.RemoveRange(dbmarks);
                    context.SaveChanges();

                    context.DBSubjects.Remove(dbsubject);
                    context.SaveChanges();
                }
            }
        }

        public void AddNewGrade(string grade, string wage)
        {
            if (!isEditing)
            {
                //Dodanie oceny do bazy danych
                int itemId;
                using (var context = new StudentAppContext())
                {
                    DBSubject dbs = context.DBSubjects.FirstOrDefault(x => x.SubjectName == chosenSubject.SubjectName);
                    DBMark dbmark = new DBMark(); //{ Mark = int.Parse(Grade), Date = DateTime.Now, DBSubject = dbs};
                    dbmark.Mark = double.Parse(Grade);
                    dbmark.Date = DateTime.Now;
                    dbmark.DBSubject = dbs;
                    dbmark.Subject = chosenSubject.SubjectName;
                    dbs.Marks.Add(dbmark);
                    //context.Entry(dbs).State = EntityState.Modified;
                    context.SaveChanges();

                    var ostatniObiekt = context.DBMarks.OrderByDescending(x => x.DBMarkId).FirstOrDefault();
                    itemId = ostatniObiekt.DBMarkId;
                }
                chosenSubject.Grades.Add(new GradeModel { GradeId = itemId, Date = DateTime.Now, GradeValue = Convert.ToDouble(Grade), Wage = Convert.ToDouble(Wage), Type = SelectedType });
            }
            else
            {
                var listaPrzedmiotow = Subjects.ToList();
                foreach (SubjectModel przedmiot in listaPrzedmiotow)
                {
                    var oceny = przedmiot.Grades.ToList();
                    foreach (GradeModel ocena in oceny)
                    {
                        if (gradeIndex == ocena.GradeId)
                        {
                            ocena.GradeValue = double.Parse(grade);
                            ocena.Wage = double.Parse(wage);
                            Subjects.Refresh();
                        }
                    }
                }

                //Edycja oceny w bazie danych
                using (var context = new StudentAppContext())
                {
                    DBMark dbm = context.DBMarks.FirstOrDefault(x => x.DBMarkId == gradeIndex);
                    dbm.Mark = double.Parse(grade);
                    context.SaveChanges();
                }

                isEditing = false;
            }

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
            IsDialogOpen = true;

            var listBoxItem = sender as System.Windows.Controls.ListBoxItem; //get sender
            gradeIndex = int.Parse(listBoxItem.Tag.ToString());

            isEditing = true;
        }

        public async Task DeleteGradeAscync(object sender)
        {
            var listBoxItem = sender as System.Windows.Controls.ListBoxItem; //get sender
            gradeIndex = int.Parse(listBoxItem.Tag.ToString());

            var listaPrzedmiotow = Subjects.ToList();
            foreach (SubjectModel przedmiot in listaPrzedmiotow)
            {
                var oceny = przedmiot.Grades.ToList();
                foreach (GradeModel ocena in oceny)
                {
                    if (gradeIndex == ocena.GradeId)
                    {
                        await Task.Delay(200);
                        przedmiot.Grades.Remove(ocena);
                    }


                }
            }

            //Usunięcie oceny z bazy
            using (var context = new StudentAppContext())
            {
                DBMark dbm = context.DBMarks.FirstOrDefault(x => x.DBMarkId == gradeIndex);

                context.DBMarks.Remove(dbm);

                context.SaveChanges();
            }
        }


    }
}


