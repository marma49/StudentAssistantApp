using System;
using System.Collections.Generic;
using System.Text;

namespace StudentAssistantApp.Models
{
    public class DBUser
    {
        public int DBUserId { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public DateTime RegisterDate { get; set; }
        public DateTime LastTimeLogged { get; set; }
    }
}
