using Caliburn.Micro;
using StudentAssistantApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.Security;
using System.Runtime.InteropServices;
using System.Linq;
using System.Threading.Tasks;

namespace StudentAssistantApp.ViewModels
{


    public class LoginFormViewModel : Screen
    {
        IWindowManager manager = new WindowManager();

        public SecureString SecurePassword { private get; set; }
        public SecureString NewSecurePassword1 { private get; set; }
        public SecureString NewSecurePassword2 { private get; set; }

        public string test_haslo;

        private string username = "";
        private string password;
        private bool isDialogOpen;
        private string newUser;
        private string newPassword1;
        private string newPassword2;
        private bool isDialogProgressOpen;


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
        public bool IsDialogProgressOpen
        {
            get { return isDialogProgressOpen; }
            set { isDialogProgressOpen = value; NotifyOfPropertyChange("IsDialogProgressOpen"); }
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

        public string NewUser
        {
            get { return newUser; }
            set
            {
                newUser = value;
                NotifyOfPropertyChange("NewUser");
            }
        }

        public string NewPassword1
        {
            get { return SecureStringToString(NewSecurePassword1); }
            set
            {
                newPassword1 = value;
                NotifyOfPropertyChange("NewPassword1");
            }
        }
        public string NewPassword2
        {
            get { return SecureStringToString(NewSecurePassword2); }
            set
            {
                newPassword2 = value;
                NotifyOfPropertyChange("NewPassword2");
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
        public async Task LogIn()
        {
            int ok = 1;
            IsDialogProgressOpen = true;
            await Task.Run(() => { 
            if (!String.IsNullOrEmpty(Username) && (!String.IsNullOrEmpty(Password)))
            {
                using (var context = new StudentAppContext())
                {
                    var users = context.DBUsers.ToList();
                    int flaga = 0;

                    for (int i = 0; i < users.Count; i++)
                    {
                        if (Username.Equals(users[i].Login))
                        {
                            flaga = i;
                            break;
                        }
                    }

                    //return context.DBTasks.ToList();


                    //hasło - savedPasswordHash wczytane z bazy na podstawie nazwy użytkownika/
                    byte[] hashBytes = Convert.FromBase64String(users[flaga].Password);
                    byte[] salt = new byte[16];

                    Array.Copy(hashBytes, 0, salt, 0, 16);

                    var pbkdf2 = new Rfc2898DeriveBytes(Password, salt, 10000);

                    byte[] hash = pbkdf2.GetBytes(20);


                    for (int i = 0; i < 20; i++)
                    {
                        if (hashBytes[i + 16] != hash[i])
                        {
                            ok = 0;
                        }
                    }
                }
            }
            });
            if (ok == 1)
            {
                await manager.ShowWindowAsync(new MainWindowViewModel());
                await TryCloseAsync();
            }
            else
            {
                Password = "";
            }
            IsDialogProgressOpen = false;
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



            if (!String.IsNullOrEmpty(NewUser))
            {

                bool flaga = false;
                using (var context = new StudentAppContext())
                {
                    var users = context.DBUsers.ToList();

                    for (int i = 0; i < users.Count; i++)
                    {
                        if (NewUser.Equals(users[i].Login))
                        {
                            flaga = true;
                            break;
                        }
                    }
                }

                if (NewPassword1.Equals(NewPassword2) && flaga == false)
                {
                    //test_haslo = CreatePasswordHash(NewPassword1);

                    var dbuser = new DBUser { Login = newUser, Password = CreatePasswordHash(NewPassword1) };

                    int userId;
                    using (var context = new StudentAppContext())
                    {
                        context.DBUsers.Add(dbuser);
                        context.SaveChanges();
                        var obiektChwilowy = context.DBUsers.OrderByDescending(x => x.DBUserId).FirstOrDefault();
                        userId = obiektChwilowy.DBUserId;
                    }
                    IsDialogOpen = false;
                }

            }
            //trzeba zapisać savedPasswordHash do bazy
        }
    }
}
