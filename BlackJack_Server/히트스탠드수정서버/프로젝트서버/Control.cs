using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 프로젝트서버
{
    class Control
    {
        static Deck deck;
        static int chips = 0;
        static int usersum = 0;
        static int dealersum = 0;
        public static int input = 0;
        static List<Card> userHand = new List<Card>();
        static List<Card> dealerHand = new List<Card>();


        public string RecvData(byte[] msg)
        {
            string str = Encoding.Default.GetString(msg);
            string[] token = str.Split('\a');
            switch (token[0].Trim())
            {
                case "PACK_BLAKC_GAMESTART":   return GameStart(token[1]);
                case "PACK_BLAKC_BETTING":     return Bet(token[1]);
                case "PACK_BLAKC_HIT":         return Hit(token[1]);
                case "PACK_BLAKC_STAND":       return Stand();

            }
            return "";
        }

        public void AllClear()
        {
            userHand.Clear();
            dealerHand.Clear();
            deck = new Deck();
            deck.Shuffle();

        }

        public string GameStart(string msg)
        {
            AllClear();
            deck.Shuffle();
            chips = 100;
            string ackstring = null;
            if (chips > 0)
            {

                ackstring += "ACK_BLACK_GAMESTART_S\a";
                ackstring += deck.GetAmountOfRemainingCrads() + "#";
                ackstring += chips + "#";


                return ackstring;
            }
            ackstring += "ACK_BLACK_GAMESTART_F" + "\a";
            return ackstring;
        }

        public string Bet(string msg)
        {
            AllClear();
            string ackstring1 = null;
            //=========카드 나눠주기======================================
            //유저 카드 드로우 
            userHand.Add(deck.DrawACard());
            userHand.Add(deck.DrawACard());
            //딜러 카드 드로우
            dealerHand.Add(deck.DrawACard());
            dealerHand.Add(deck.DrawACard());

            usersum = userHand[0].Value + userHand[1].Value;
            dealersum = dealerHand[0].Value + dealerHand[1].Value;
            //카드가 A일때.
            if ((userHand[0].Face == Face.Ace||userHand[1].Face == Face.Ace) && usersum < 11 && usersum + 10 < 21)
            {
                if (userHand[0].Face == userHand[1].Face)
                {
                    usersum = 12;
                }
                usersum += 10;
            }

            if ((dealerHand[0].Face == Face.Ace || dealerHand[1].Face == Face.Ace) && dealersum < 11 && dealersum + 10 < 21)
            {
                if (dealerHand[0].Face == dealerHand[1].Face)
                {
                    dealersum = 12;
                }
                dealersum += 10;
            }

            if (usersum == 21 || dealersum == 21)
            {
                ackstring1 += "ACK_BLACK_BETTING" + "\a";
                if (usersum == 21)
                {
                    ackstring1 += "DEALER_LOSE" + "#";
                    ackstring1 += usersum + "#";
                }

                else if (dealersum == 21)
                {
                    ackstring1 += "DEALER_WIN" + "#";
                    ackstring1 += dealersum + "#";
                }

                else if (usersum == 21 && dealersum == 21)
                {
                    ackstring1 += "DEALER_SAME" + "#";
                    ackstring1 += usersum + "#";
                    ackstring1 += dealersum + "#";
                }
                return ackstring1;
            }

            ackstring1 += "ACK_BLACK_BETTING\a";
            ackstring1 += userHand[0].Face + "#";
            ackstring1 += userHand[0].Pattern + "#";
            ackstring1 += userHand[0].Value + "#";
            ackstring1 += userHand[1].Face + "#";
            ackstring1 += userHand[1].Pattern + "#";
            ackstring1 += userHand[1].Value + "#";
            ackstring1 += usersum + "#";
            ackstring1 += dealerHand[0].Face + "#";
            ackstring1 += dealerHand[0].Pattern + "#";
            ackstring1 += dealerHand[0].Value + "#";
            ackstring1 += dealerHand[1].Face + "#";
            ackstring1 += dealerHand[1].Pattern + "#";
            ackstring1 += dealerHand[1].Value + "#";
            //ackstring1 += userHand[0].Face + "#";
            //ackstring1 += userHand[0].Pattern + "#";
            //ackstring1 += userHand[1].Face + "#";
            //ackstring1 += userHand[1].Pattern + "#";
            //ackstring1 += usersum + "#";
            //ackstring1 += dealerHand[0].Face + "#";
            //ackstring1 += dealerHand[0].Pattern + "#";
            //ackstring1 += dealerHand[1].Face + "#";
            //ackstring1 += dealerHand[1].Pattern + "#";
            return ackstring1;

            //foreach (Card card in userHand)
            //{
            //    if (card.Face == Face.Ace)
            //    {
            //        card.Value += 10;
            //        break;
            //    }
            //}
            //textBox1.Text = "" + (userHand[0].Value + userHand[1].Value);

        }

        public string Hit(string msg)
        {
            userHand.Add(deck.DrawACard());
            string ackstring = null;
            int c = userHand.Count - 1;

            ackstring += "ACK_BLACK_HIT_S" + "\a";
            ackstring += userHand[c].Face + "#";
            ackstring += userHand[c].Pattern + "#";
            ackstring += userHand[c].Value + "#";
            int uservalue = 0;
            for (int i = 0; i < userHand.Count; i++)
            {
                uservalue += userHand[i].Value;
            }
            ackstring += uservalue + "#";
            return ackstring;

            //string ackstring = null;

            //userHand.Add(deck.DrawACard());
            //usersum += userHand[userHand.Count - 1].Value;
            //if (userHand[userHand.Count - 1].Face == Face.Ace && usersum < 11 && usersum + 10 < 21)
            //{
            //    usersum += 10;
            //}

            //if (usersum > 21)
            //{
            //    ackstring += "ACK_BLACK_HIT_S" + "\a";
            //    ackstring += "DEALER_WIN" + "#";
            //    ackstring += userHand[userHand.Count - 1].Face + "#";
            //    ackstring += userHand[userHand.Count - 1].Pattern + "#";
            //    ackstring += userHand[userHand.Count - 1].Value + "#";
            //    ackstring += usersum + "#";

            //    return ackstring;
            //}

            //int c = userHand.Count - 1;
            //ackstring += "ACK_BLACK_HIT_S" + "\a";
            //ackstring += userHand[userHand.Count - 1].Face + "#";
            //ackstring += userHand[userHand.Count - 1].Pattern + "#";
            //ackstring += userHand[userHand.Count - 1].Value + "#";
            //ackstring += usersum + "#";
            //return ackstring;
        }

        public string Stand()
        {
            int dealervalue = 0;
            int uservalue = 0;
            string ackstring = null;
            ackstring += "ACK_BLACK_STAND_S" + "\a";

            for (int i = 0; i < dealerHand.Count; i++)
            {
                dealervalue += dealerHand[i].Value;

            }

            for (int i = 0; i < userHand.Count; i++)
            {
                uservalue += userHand[i].Value;

            }

            if (dealervalue > 21)
            {
                ackstring += "DEALER_LOSE" + "#";
                ackstring += dealervalue + "@";

            }
            else if (dealervalue == 21)
            {
                ackstring += "DEALER_WIN" + "#";
                ackstring += dealervalue + "@";

            }
            else if (dealervalue < 21 && dealervalue >= 17)
            {
                if (uservalue > dealervalue && uservalue < 21)
                {
                    ackstring += "DEALER_LOSE" + "#";
                    ackstring += dealervalue + "@";

                }
                else if (uservalue == dealervalue)
                {
                    ackstring += "DEALER_SAME" + "#";
                    ackstring += dealervalue + "@";

                }
                else if (uservalue < dealervalue)
                {
                    ackstring += "DEALER_WIN" + "#";
                    ackstring += dealervalue + "@";

                }
                else if (uservalue > 21)
                {
                    ackstring += "DEALER_WIN" + "#";
                    ackstring += dealervalue + "@";

                }
            }
            else if (dealervalue < 17)
            {
                dealerHand.Add(deck.DrawACard());

                ackstring += "DEALER_DRAW" + "#";
                ackstring += dealerHand[dealerHand.Count - 1].Face + "@";
                ackstring += dealerHand[dealerHand.Count - 1].Pattern + "@";
                ackstring += dealerHand[dealerHand.Count - 1].Value + "@";
                ackstring += dealervalue + "@";

            }
            return ackstring;
            //string ackstring = null;
            //ackstring += "ACK_BLACK_STAND_S" + "\a";


            //if (dealersum > 21)
            //{
            //    ackstring += "DEALER_LOSE" + "#";
            //    ackstring += dealersum + "@";

            //}
            //else if (dealersum == 21)
            //{
            //    ackstring += "DEALER_WIN" + "#";
            //    ackstring += dealersum + "@";

            //}
            //else if (dealersum < 21 && dealersum >= 17)
            //{
            //    if (usersum > dealersum && usersum < 21)
            //    {
            //        ackstring += "DEALER_LOSE" + "#";
            //        ackstring += dealersum + "@";

            //    }
            //    else if (usersum == dealersum)
            //    {
            //        ackstring += "DEALER_SAME" + "#";
            //        ackstring += dealersum + "@";

            //    }
            //    else if (usersum < dealersum)
            //    {
            //        ackstring += "DEALER_WIN" + "#";
            //        ackstring += dealersum + "@";

            //    }
            //    else if (usersum > 21)
            //    {
            //        ackstring += "DEALER_WIN" + "#";
            //        ackstring += dealersum + "@";

            //    }
            //}
            //else if (dealersum < 17)
            //{
            //    dealerHand.Add(deck.DrawACard());
            //    if (dealerHand[dealerHand.Count - 1].Face == Face.Ace && dealersum < 11 && dealersum + 10 < 21)
            //    {
            //        dealersum += 10;
            //    }
            //    ackstring += "DEALER_DRAW" + "#";
            //    ackstring += dealerHand[dealerHand.Count - 1].Face + "@";
            //    ackstring += dealerHand[dealerHand.Count - 1].Pattern + "@";
            //    ackstring += dealerHand[dealerHand.Count - 1].Value + "@";
            //    ackstring += dealersum + "@";

            //}
            //return ackstring;
        }
    }
}
