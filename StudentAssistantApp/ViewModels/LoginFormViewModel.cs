using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentAssistantApp.ViewModels
{
    public class LoginFormViewModel : Screen
    {
        IWindowManager manager = new WindowManager();


        public void Close()
        {
            TryCloseAsync();
        }

        public void LogIn()
        {
            manager.ShowWindowAsync(new MainWindowViewModel());
            TryCloseAsync();
        }
    }
}
