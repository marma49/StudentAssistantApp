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
        private bool isDialogSaveOpen;
        private int noteIndex;
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
        public bool IsDialogSaveOpen
        {
            get { return isDialogSaveOpen; }
            set
            {
                isDialogSaveOpen = value;
                NotifyOfPropertyChange("IsDialogSaveOpen");
            }
        }
        public bool IsEditing
        {
            get => isEditing;
            set
            {
                isEditing = value;
                NotifyOfPropertyChange("IsEditing");
            }
        }
        public int NoteIndex
        {
            get => noteIndex; set
            {
                noteIndex = value;
                NotifyOfPropertyChange("NoteIndex");
            }
        }
        public void CloseDialog()
        {
            IsDialogOpen = false;
            IsEditing = false;
        }
        public void CloseSaveDialog()
        {
            IsDialogSaveOpen = false;
            IsEditing = false;
        }
        public NotesWindowViewModel()
        {

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
            IsDialogSaveOpen = false;
            IsEditing = false;
        }

        public void LoadNote(int noteIndex)
        {
            DBNote DBNote;
            using (var context = new StudentAppContext())
            {
                DBNote = context.DBNotes.Where(x => x.DBNoteId == Notes[noteIndex].NoteId).FirstOrDefault();
                NoteName = DBNote.NoteHeadline;
                NoteContent = DBNote.NoteText;

                //context.SaveChanges();
            }
            IsEditing = true;
            NoteIndex = DBNote.DBNoteId;
            IsDialogOpen = false;
        }

        public bool CanLoadNote(int noteIndex)
        {
            return noteIndex >= 0;
        }

        public void SaveNotes()
        {
            if (IsEditing)
            {
                using (var context = new StudentAppContext())
                {
                    DBNote DBNote = context.DBNotes.Where(x => x.DBNoteId == NoteIndex).FirstOrDefault();
                    DBNote.NoteHeadline = NoteName;
                    DBNote.NoteText = NoteContent;

                    context.SaveChanges();
                }

                NoteName = "";
                NoteContent = "";
            }
            else
            {
                IsDialogSaveOpen = true;
            }
            IsEditing = false;
        }

        public void OpenNotes()
        {
            IsDialogOpen = true;
        }

        public void DeleteNote()
        {
            //Usunięcie notatki z bazy
            using (var context = new StudentAppContext())
            {
                DBNote dbnote = context.DBNotes.FirstOrDefault(x => x.DBNoteId == noteIndex); // noteIndex zmienna wskazująca id wybranej notatki

                context.DBNotes.Remove(dbnote);
                context.SaveChanges();
            }


            NoteModel nm = Notes.FirstOrDefault(x => x.NoteId == noteIndex);
            Notes.Remove(nm);

            IsEditing = false;
            NoteContent = "";
            NoteName = "";
        }
    }
}
