using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalWebProject.ClassTypes
{
    public class CardColorType
    {
        public int card { get; set; }
        public int color { get; set; }
        public CardColorType(int card,int color)
        {
            this.card = card;
            this.color = color;
        }
    }
}