using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalWebProject.ClassTypes
{
    public class CardKindType
    {
        public int cardId;
        public int cardKind;
        public CardKindType(int cardId,int cardKind)
        {
            this.cardId = cardId;
            this.cardKind = cardKind;
        }
    }
}