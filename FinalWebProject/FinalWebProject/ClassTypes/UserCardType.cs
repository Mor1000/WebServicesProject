using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalWebProject.ClassTypes
{
    public class UserCardType
    {
        public string userName { get; set; }
        public int userCard { get; set; }
        public int userAmount { get; set; }
        public UserCardType(string user, int card, int amount)
        {
            userName = user;
            userCard = card;
            userAmount = amount;
        }
    }
}