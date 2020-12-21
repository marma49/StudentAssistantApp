using Caliburn.Micro;
using StudentAssistantApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using MaterialDesignColors;
using MaterialDesignThemes.Wpf;
using System.Linq;

namespace StudentAssistantApp.ViewModels
{
    class NotesWindowViewModel : Screen
    {
        private BindableCollection<NoteModel> notes = new BindableCollection<NoteModel>();
        private string noteName;
        private string noteContent;
        private bool isDialogOpen, isEditing;
        private int itemIndex, itemCount = 0;
        public BindableCollection<NoteModel> Notes
        {
            get { return notes; }
            set { notes = value; }
        }
        public string NoteName
        {
            get { return noteName; }
            set
            {
                noteName = value;
                NotifyOfPropertyChange("NoteName");
            }
        }
        public string NoteContent
        {
            get { return noteContent; }
            set
            {
                noteContent = value;
                NotifyOfPropertyChange("NoteContent");
            }
        }
        public bool IsDialogOpen
        {
            get { return isDialogOpen; }
            set
            {
                isDialogOpen = value;
                NotifyOfPropertyChange("IsDialogOpen");
            }
        }
        public void CloseDialog()
        {
            IsDialogOpen = false;
            isEditing = false;
            //NoteContent = "";
            //NoteName = "";
        }

        public NotesWindowViewModel()
        {
            //Notes.Add(new NoteModel { NoteName = "test1", NoteContent = "TEST1", NoteId = itemCount++ });
            //Notes.Add(new NoteModel { NoteName = "test2", NoteContent = "TEST2", NoteId = itemCount++ });
            //Notes.Add(new NoteModel { NoteName = "test3", NoteContent = "TEST3", NoteId = itemCount++ });

            //Pobranie notatek z bazy danych
            using (var context = new StudentAppContext())
            {
                var notes = context.DBNotes.ToList();
                foreach (DBNote n1 in notes)
                {
                    Notes.Add(new NoteModel { NoteName = n1.NoteHeadline, NoteContent = n1.NoteText, NoteId = n1.DBNoteId });
                }
            }
        }

        public void SaveNote()
        {

            //Dodanie notatki do bazy danych
            int idIndex;
            var dbnote = new DBNote { NoteHeadline = noteName, NoteText = noteContent };
            using (var context = new StudentAppContext())
            {
                context.DBNotes.Add(dbnote);
                context.SaveChanges();
                var obiekt = context.DBNotes.OrderByDescending(x => x.DBNoteId).FirstOrDefault();
                idIndex = obiekt.DBNoteId;
            }

            Notes.Add(new NoteModel { NoteName = noteName, NoteContent = noteContent, NoteId = idIndex });

            NoteName = "";
            NoteContent = "";
            IsDialogOpen = false;
            isEditing = false;
        }

        public void LoadNote(int index)
        {

            NoteName = Notes[index].NoteName;
            NoteContent = Notes[index].NoteContent;

            /*DBNote note1; 
            using(var context = new StudentAppContext())
            {
                note1 = context.DBNotes.Where(x => x.DBNoteId == index).FirstOrDefault();
                note1.NoteHeadline = noteName;
                note1.NoteText = NoteContent;

                context.SaveChanges();
            }*/
            IsDialogOpen = false;
        }



    }
}
