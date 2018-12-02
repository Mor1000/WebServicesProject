using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalWebProject.ClassTypes
{
    public class DeckType
    {
        public string deckName { get; set; }
        public int deckFormat { get; set; }
        public string deckCreationDate { get; set; }
        public string deckDescription { get; set; }

        public DeckType(string name, int format, string creationDate, string description)
        {
            deckName = name;
            deckCreationDate = creationDate;
            deckFormat = format;
            deckDescription = description;
        }
    }
}