using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using Caliburn.Micro;

namespace StudentAssistantApp.ViewModels
{
    public class MainWindowViewModel : Conductor<object>
    {

        public void LoadNotes()
        {
            ActivateItem(new NotesWindowViewModel());
        }

        public void LoadGrades()
        {
            ActivateItem(new GradesWindowViewModel());
        }

        public void LoadTasks()
        {
            ActivateItem(new TasksWindowViewModel());
        }

        public void LoadCalendar()
        {
            ActivateItem(new CalendarWindowViewModel());
        }

        public void LoadOptions()
        {
            ActivateItem(new OptionsWindowViewModel());
        }

    }
}
