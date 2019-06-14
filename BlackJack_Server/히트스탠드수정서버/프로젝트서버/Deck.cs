using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 프로젝트서버
{
    class Deck
    {
        private List<Card> cards;

        public Deck()
        {
            this.Initialize();
            
        }

        public void Initialize()
        {
            cards = new List<Card>();
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 13; j++)
                {
                    cards.Add(new Card() { Pattern = (Pattern)i, Face = (Face)j });

                    if (j <= 8)
                        cards[cards.Count - 1].Value = j + 1;
                    else
                        cards[cards.Count - 1].Value = 10;
                }
            }
        }
        public void Shuffle()
        {
            Random rng = new Random();
            int n = cards.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                Card card = cards[k];
                cards[k] = cards[n];
                cards[n] = card;
            }
        }

        public Card DrawACard()
        {
            if (cards.Count <= 0)
            {
                this.Initialize();
                this.Shuffle();
            }

            Card cardToReturn = cards[cards.Count - 1];
            cards.RemoveAt(cards.Count - 1);
            return cardToReturn;
        }

        public int GetAmountOfRemainingCrads()
        {
            return cards.Count;
        }
    }
}
