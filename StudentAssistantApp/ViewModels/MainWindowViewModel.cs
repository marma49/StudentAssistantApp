using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Windows;
using Caliburn.Micro;
using StudentAssistantApp.Views;

namespace StudentAssistantApp.ViewModels
{
    public class MainWindowViewModel : Conductor<object>
    {
        IWindowManager manager = new WindowManager();
        CancellationTokenSource cancellationToken = null;
        CancellationToken token;

        public MainWindowViewModel()
        {
            cancellationToken = new CancellationTokenSource();
            token = cancellationToken.Token;
        }
        public void LoadNotes()
        {
            ActivateItemAsync(new NotesWindowViewModel(), token);
        }

        public void LoadGrades()
        {
            ActivateItemAsync(new GradesWindowViewModel(), token);
        }

        public void LoadTasks()
        {
            ActivateItemAsync(new TasksWindowViewModel(), token);
        }

        public void LoadCalendar()
        {
            ActivateItemAsync(new CalendarWindowViewModel(), token);
        }

        public void LoadOptions()
        {
            ActivateItemAsync(new OptionsWindowViewModel(), token);
        }
        public void LogOut()
        {
            manager.ShowWindowAsync(new LoginFormViewModel());
            TryCloseAsync();
        }

    }
}
