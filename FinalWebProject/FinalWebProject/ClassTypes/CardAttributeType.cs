using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalWebProject.ClassTypes
{
    public class CardAttributeType
    {
        public int card { get; set; }
        public int power { get; set; }
        public int toughness { get; set; }
        public CardAttributeType(int card, int power, int toughness)
        {
            this.card = card;
            this.power = power;
            this.toughness = toughness;
        }
    }
}