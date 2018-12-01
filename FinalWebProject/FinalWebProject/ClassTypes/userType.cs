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
        public int country { get; set; }
        public bool isValid { get; set; }
        public string arenaName { get; set; }

        public UserType(string username, string password)
        {
            this.username = username;
            this.password = password;
        }
        public UserType(string username,string password,string email,string birthdate,int country, bool isValid, string arenaName)
        {
            this.username = username;
            this.password = password;
            this.email = email;
            this.birthdate = birthdate;
            this.country = country;
            this.isValid = isValid;
            this.arenaName = arenaName;  
        }
    }
}