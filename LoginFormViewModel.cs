using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.Security;
using System.Runtime.InteropServices;

namespace StudentAssistantApp.ViewModels
{


    public class LoginFormViewModel : Screen
    {
        IWindowManager manager = new WindowManager();

        public SecureString SecurePassword { private get; set; }



        private string username;
        private string password;
        private bool isDialogOpen;

        public string CreatePasswordHash(string haslo)
        {
            byte[] salt;

            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);
            var pbkdf2 = new Rfc2898DeriveBytes(haslo, salt, 10000);

            byte[] hash = pbkdf2.GetBytes(20);
            byte[] hashBytes = new byte[36];

            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);

            string savedPasswordHash = Convert.ToBase64String(hashBytes);

            return savedPasswordHash;
        }

        String SecureStringToString(SecureString value)
        {
            IntPtr valuePtr = IntPtr.Zero;
            try
            {
                valuePtr = Marshal.SecureStringToGlobalAllocUnicode(value);
                return Marshal.PtrToStringUni(valuePtr);
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(valuePtr);
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

        public string Username
        {
            get { return username; }
            set
            {
                username = value;
                NotifyOfPropertyChange("Username");
            }
        }
        public string Password
        {
            get { return SecureStringToString(SecurePassword); }
            set
            {
                password = value;
                NotifyOfPropertyChange("Password");
            }
        }

        public void CloseDialog()
        {
            IsDialogOpen = false;
            username = "";
            password = "";
        }

        public void Close()
        {
            TryCloseAsync();
        }

        public void LogIn()
        {


            //hasło - savedPasswordHash wczytane z bazy na podstawie nazwy użytkownika/
            byte[] hashBytes = Convert.FromBase64String(CreatePasswordHash("1234"));
            byte[] salt = new byte[16];

            Array.Copy(hashBytes, 0, salt, 0, 16);

            var pbkdf2 = new Rfc2898DeriveBytes(Password, salt, 10000);

            byte[] hash = pbkdf2.GetBytes(20);

            int ok = 1;
            for (int i = 0; i < 20; i++)
            {
                if (hashBytes[i + 16] != hash[i])
                {
                    ok = 0;
                }
            }

            if (ok == 1)
            {
                manager.ShowWindowAsync(new MainWindowViewModel());
                TryCloseAsync();
            }
            else
            {
                Password = "";
            }
        }


        public void AddAccount()
        {
            //if(SecureStringToString())

            //byte[] salt;

            //new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);
            //var pbkdf2 = new Rfc2898DeriveBytes(SecureStringToString(SecurePassword), salt, 10000);

            //byte[] hash = pbkdf2.GetBytes(20);
            //byte[] hashBytes = new byte[36];

            //Array.Copy(salt, 0, hashBytes, 0, 16);
            //Array.Copy(hash, 0, hashBytes, 16, 20);

            IsDialogOpen = false;

            //TryCloseAsync();

            //string savedPasswordHash = Convert.ToBase64String(hashBytes);

            //trzeba zapisać tablice salt oraz savedPasswordHash do bazy
        }
    }
}
