using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalWebProject.ClassTypes
{
    public class UserType
    {
        public string username { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public string birthdate { get; set; }
        public string country { get; set; }
        public bool blocked { get; set; }
        public string arenaName { get; set; }

        public UserType(string username,string password,string email,string birthdate,string country, bool blocked, string arenaName)
        {
            this.username = username;
            this.password = password;
            this.email = email;
            this.birthdate = birthdate;
            this.country = country;
            this.blocked = blocked;
            this.arenaName = arenaName;  
        }
    }
}