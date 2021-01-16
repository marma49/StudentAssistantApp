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
        public void CloseDialog()
        {
            IsDialogOpen = false;
            isEditing = false;
            //NoteContent = "";
            //NoteName = "";
        }
        public void CloseSaveDialog()
        {
            IsDialogSaveOpen = false;
            isEditing = false;
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
            isEditing = false;
        }

        public void LoadNote(int index)
        {

            //NoteName = Notes[index].NoteName;
            //NoteContent = Notes[index].NoteContent;

            using (var context = new StudentAppContext())
            {
                DBNote DBNote = context.DBNotes.Where(x => x.DBNoteId == index + 1).FirstOrDefault();
                NoteName = DBNote.NoteHeadline;
                NoteContent = DBNote.NoteText;

                //context.SaveChanges();
            }

            isEditing = true;
            noteIndex = index;
            IsDialogOpen = false;
        }

        public void SaveNotes()
        {
            if (isEditing)
            {
                using (var context = new StudentAppContext())
                {
                    DBNote DBNote = context.DBNotes.Where(x => x.DBNoteId == noteIndex + 1).FirstOrDefault(); //Do poprawy idIndex
                    DBNote.NoteHeadline = NoteName;
                    DBNote.NoteText = NoteContent;

                    context.SaveChanges();
                }

                NoteName = "";
                NoteContent = "";
                isEditing = false;
            }
            else
            {
                IsDialogSaveOpen = true;
            }
            //await DialogHost.Show(DialogSave.DialogContent, "IdDialogSave");
        }

        public void OpenNotes()
        {
            IsDialogOpen = true;
            //await DialogHost.Show(DialogOpen.DialogContent, "IdDialogOpen");
        }
    }
}
