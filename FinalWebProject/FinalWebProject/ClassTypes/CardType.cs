using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalWebProject.ClassTypes
{
    public class CardType
    {
        public string cardName { get; set; }
        public string cardAbility { get; set; }
        public int cardManaCost { get; set; }
        public int cardRarity { get; set; }
        public string cardImage { get; set; }

        public CardType(string name, string ability, int manaCost, int rarity, string image)
        {
            cardName = name;
            cardAbility = ability;
            cardManaCost = manaCost;
            cardRarity = rarity;
            cardImage = image;
        }
    }
}