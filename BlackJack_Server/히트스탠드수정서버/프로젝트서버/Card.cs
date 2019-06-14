using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 프로젝트서버
{
    public enum Pattern
    {
        Heart,
        Diamond,
        Spade,
        Club
    }

    public enum Face
    {
        Ace,
        Two,
        Three,
        Four,
        Five,
        Six,
        Seven,
        Eight,
        Nine,
        Ten,
        Jack,
        Queen,
        King,
    }

    class Card
    {
        public Pattern Pattern { get; set; }
        public Face Face { get; set; }
        public int Value { get; set; }

    }
}
