using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;

namespace Winform_프로젝트
{
    public partial class Form1 : Form
    {
        static int chips = 0;
        public static int input = 0;

        static List<Card> userHand = new List<Card>();
        static List<Card> dealerHand = new List<Card>();
        private Bitmap m_bmpBackground;
        Control con;
        public Form1()
        {
            InitializeComponent();
            Bitmap house = new Bitmap("background.bmp");
            m_bmpBackground = new Bitmap(this.Width,this.Height);
            Font font = new Font("궁서체", 30);
            Graphics g = Graphics.FromImage(m_bmpBackground);
            g.DrawImage(house, new Rectangle(0, 0, this.Width,this.Height));
            g.DrawString("망했습니다 ㅠㅠ", font, Brushes.Black, 300, 320);
        }

        private Image DrawCard(Face face, Pattern pattern)
        {
            switch (pattern.ToString())
            {
                case "Heart":
                    switch (face.ToString())
                    {
                        case "Ace":  return Properties.Resources.하트A;
                        case "Two":  return Properties.Resources.하트2;
                        case "Three":return Properties.Resources.하트3;
                        case "Four": return Properties.Resources.하트4;
                        case "Five": return Properties.Resources.하트5;
                        case "Six":  return Properties.Resources.하트6;
                        case "Seven":return Properties.Resources.하트7;
                        case "Eight":return Properties.Resources.하트8;
                        case "Nine": return Properties.Resources.하트9;
                        case "Ten":  return Properties.Resources.하트10;
                        case "Jack": return Properties.Resources.하트J;
                        case "Queen":return Properties.Resources.하트Q;
                        case "King": return Properties.Resources.하트K;
                    }
                    break;
                case "Diamond":
                    switch (face.ToString())
                    {
                        case "Ace":  return Properties.Resources.다이아A;
                        case "Two":  return Properties.Resources.다이아2;
                        case "Three":return Properties.Resources.다이아3;
                        case "Four": return Properties.Resources.다이아4;
                        case "Five": return Properties.Resources.다이아5;
                        case "Six":  return Properties.Resources.다이아6;
                        case "Seven":return Properties.Resources.다이아7;
                        case "Eight":return Properties.Resources.다이아8;
                        case "Nine": return Properties.Resources.다이아9;
                        case "Ten":  return Properties.Resources.다이아10;
                        case "Jack": return Properties.Resources.다이아J;
                        case "Queen":return Properties.Resources.다이아Q;
                        case "King": return Properties.Resources.다이아K;
                    }
                    break;
                case "Spade":
                    switch (face.ToString())
                    {
                        case "Ace":  return Properties.Resources.스페이드A;
                        case "Two":  return Properties.Resources.스페이드2;
                        case "Three":return Properties.Resources.스페이드3;
                        case "Four": return Properties.Resources.스페이드4;
                        case "Five": return Properties.Resources.스페이드5;
                        case "Six":  return Properties.Resources.스페이드6;
                        case "Seven":return Properties.Resources.스페이드7;
                        case "Eight":return Properties.Resources.스페이드8;
                        case "Nine": return Properties.Resources.스페이드9;
                        case "Ten":  return Properties.Resources.스페이스10;
                        case "Jack": return Properties.Resources.스페이드J;
                        case "Queen":return Properties.Resources.스페이드Q;
                        case "King": return Properties.Resources.스페이드K;
                    }
                    break;
                case "Club":
                    switch (face.ToString())
                    {
                        case "Ace":  return Properties.Resources.클로버A;
                        case "Two":  return Properties.Resources.클로버2;
                        case "Three":return Properties.Resources.클로버3;
                        case "Four": return Properties.Resources.클로버4;
                        case "Five": return Properties.Resources.클로버5;
                        case "Six":  return Properties.Resources.클로버6;
                        case "Seven":return Properties.Resources.클로버7;
                        case "Eight":return Properties.Resources.클로버8;
                        case "Nine": return Properties.Resources.클로버9;
                        case "Ten":  return Properties.Resources.클로버10;
                        case "Jack": return Properties.Resources.클로버J;
                        case "Queen":return Properties.Resources.클로버Q;
                        case "King": return Properties.Resources.클로버K;
                    }
                    break;
            }
            
            return null;
        }
        private void ResetCard()
        {
            pictureBox1.Image = null;
            pictureBox2.Image = null;
            pictureBox3.Image = null;
            pictureBox4.Image = null;
            pictureBox5.Image = null;
            pictureBox11.Image = null;
            pictureBox12.Image = null;
            pictureBox13.Image = null;
            pictureBox14.Image = null;
            pictureBox15.Image = null;
        }
        private void GameStart()
        {
            textBox2.Text = null;
            button3.Enabled = true;
            button4.Enabled = true;
        }
        private void GameOver()
        {
            Update();
            AllClear();
            button3.Enabled=false;
            button4.Enabled = false;
        }
        private void AllClear()
        {
            userHand.Clear();
            dealerHand.Clear();
            Control.c = 0;
            Control.d = 0;

        }
        private void Busted()
        {
            button3.Enabled = false;
        }
     
        //클라이언트 HIT
        private void button3_Click(object sender, EventArgs e)//HIT
        {
            //클라이언트 HIT
            con.Bust += new BlackDel3(Busted);
            //con.ReturntoUserHand3 += new BlackDel2(UserDraw3);
            //con.ReturntoUserHand4 += new BlackDel2(UserDraw4);
            //con.ReturntoUserHand5 += new BlackDel2(UserDraw5);
            con.Hit();
            


                //카드가 A일때.
                //if (card.Face == Face.Ace && totalCardsValue < 11 && totalCardsValue + 10 < 21)
                //{
                //    totalCardsValue += 10;
                //}

            //버스트 당했을때
            //if (totalCardsValue > 21)
            //{
            //    MessageBox.Show("Busted!\n");
            //    chips -= input;
            //    GameOver();
            //}
            ////블랙잭
            //else if (totalCardsValue == 21)
            //{
            //    MessageBox.Show("Blackjack, You Won!");
            //    chips += input + (input / 2);
            //    textBox3.Text = "" + deck.GetAmountOfRemainingCrads();
            //    textBox4.Text = "" + chips;
            //    GameOver();
            //    return;
            //}
            //textBox3.Text = "" + deck.GetAmountOfRemainingCrads();
            //textBox4.Text = "" + chips;
            //Update();
        }

        
        //클라이언트 STAND
        private void button4_Click(object sender, EventArgs e)//STAND
        {
            pictureBox15.Image = DrawCard(dealerHand[0].Face, dealerHand[0].Pattern);
            //con.ReturntoDealerHand3 += new BlackDel2(DealerDraw3);
            //con.ReturntoDealerHand4 += new BlackDel2(DealerDraw4);
            //con.ReturntoDealerHand5 += new BlackDel2(DealerDraw5);
            con.Stand();

            //switch (Control.d)
            //{
            //    case 0: con.ReturntoDealerHand3 += new BlackDel2(DealerDraw3);  break;
            //    case 1: con.ReturntoDealerHand4 += new BlackDel2(DealerDraw4);  break;
            //    case 2: con.ReturntoDealerHand5 += new BlackDel2(DealerDraw5);  break;
            //}

            con.DGameOver += new BlackDel3(GameOver);
            
            //MessageBox.Show("Dealer");
            //pictureBox15.Image = DrawCard(dealerHand[0].Face, dealerHand[0].Pattern);
            //MessageBox.Show("Hidden : " + dealerHand[0].Face + dealerHand[0].Pattern);

            //int dealerCardsValue = 0;
            //foreach (Card card in dealerHand)
            //{
            //    dealerCardsValue += card.Value;
            //    if (card.Face == Face.Ace && dealerCardsValue < 11 && dealerCardsValue + 10 < 21)
            //    {
            //        dealerCardsValue += 10;
            //    }
            //}
            //textBox2.Text = "" + dealerCardsValue;
            //Update();

            //while (dealerCardsValue < 17)
            //{
            //    Thread.Sleep(1000);
            //    dealerHand.Add(deck.DrawACard());
            //    switch (dealerHand.Count-1)
            //    {
            //        case 2:
            //            pictureBox13.Image = DrawCard(dealerHand[2].Face, dealerHand[2].Pattern);  break;
            //        case 3:
            //            pictureBox12.Image = DrawCard(dealerHand[3].Face, dealerHand[3].Pattern);  break;
            //        case 4:
            //            pictureBox11.Image = DrawCard(dealerHand[4].Face, dealerHand[4].Pattern); break;
            //    }

            //    dealerCardsValue = 0;
            //    foreach (Card card in dealerHand)
            //    {
            //        dealerCardsValue += card.Value;
            //        if (card.Face == Face.Ace && dealerCardsValue < 11 && dealerCardsValue+10 < 21)
            //        {
            //            dealerCardsValue += 10;
            //        }
            //    }
            //    textBox2.Text = "" + dealerCardsValue;
            //    MessageBox.Show(""+dealerHand[dealerHand.Count - 1].Face+dealerHand[dealerHand.Count - 1].Pattern);
            //    Update();
            //}
            //textBox2.Text = "" + dealerCardsValue;


            ////딜러 버스트
            //if (dealerCardsValue > 21)
            //{
            //    MessageBox.Show("Dealer bust! You win!");
            //    chips += input;
            //    GameOver();
            //}
            //else
            //{
            //    int playerCardValue = 0;
            //    foreach (Card card in userHand)
            //    {
            //        playerCardValue += card.Value;
            //    }

            //    if (dealerCardsValue > playerCardValue)
            //    {
            //        MessageBox.Show("Dealer has "+ dealerCardsValue + " and player has "+ playerCardValue + "\n dealer wins!");
            //        chips -= input;
            //    }
            //    else if (dealerCardsValue < playerCardValue)
            //    {
            //        MessageBox.Show("Dealer has " + dealerCardsValue + " and player has " + playerCardValue + "\n player wins!");
            //        chips += input+(input/2);
            //    }
            //    else
            //    {
            //        MessageBox.Show("Dealer has " + dealerCardsValue + " and player has " + playerCardValue + "\n Drow!");
            //        chips += 0;
            //    }


            //}
            //textBox3.Text = "" + deck.GetAmountOfRemainingCrads();
            //textBox4.Text = "" + chips;
            //GameOver();
        }

        //GameStart
        private void button5_Click(object sender, EventArgs e)
        {
            //CheckForIllegalCrossThreadCalls = false;
            //if(chips > 0)
            //{ 
            con = new Control();
                
            con.ReturntoDeck +=  new BlackDel(DeckPrint);
            con.ReturntoChips += new BlackDel(ChipPrint);
                con.GameStart();
                
                MessageBox.Show("♠♥♣♦ Blackjack Game 게임시작");
                button5.Enabled = false;
                ResetCard();
                GameOver();
            //deck = new Deck();
            //deck.Shuffle();
            // textBox3.Text = "" + deck.GetAmountOfRemainingCrads();
            // textBox4.Text = "" + chips;
            MessageBox.Show("베팅을 시작하십시오.");
                button6.Enabled= true;
                              
            //}
        }

        private void button6_Click(object sender, EventArgs e)
        {
            ResetCard();

            if (int.Parse(textBox5.Text) < 0)
            {
                MessageBox.Show("음수는 안된다");
                GameOver();
                return;
            }

            if (int.Parse(textBox5.Text) == 0)
            {
                MessageBox.Show("do betting!");
                GameOver();
                return;
            }

            //if (int.Parse(textBox5.Text) > chips)
            //{
            //    MessageBox.Show("너는 돈이 없다!");
            //    GameOver();
            //    return;
            //}

            //아니면 게임진행
            input = int.Parse(textBox5.Text);
            con.Betting();

            con.ReturntoUserHand1 += new BlackDel2(UserDraw1);
            con.ReturntoUserHand2 += new BlackDel2(UserDraw2);
            con.ReturntoUserHand3 += new BlackDel2(UserDraw3);
            con.ReturntoUserHand4 += new BlackDel2(UserDraw4);
            con.ReturntoUserHand5 += new BlackDel2(UserDraw5);
            con.ReturntoDealerHand1 += new BlackDel2(DealerDraw1);
            con.ReturntoDealerHand2 += new BlackDel2(DealerDraw2);
            con.ReturntoDealerHand3 += new BlackDel2(DealerDraw3);
            con.ReturntoDealerHand4 += new BlackDel2(DealerDraw4);
            con.ReturntoDealerHand5 += new BlackDel2(DealerDraw5);

            GameStart();

            ////유저의 핸드가 블랙잭
            //if (userHand[0].Value + userHand[1].Value == 21)
            //{
            //    MessageBox.Show("Blackjack, You Won!");
            //    chips += input + (input / 2);
            //    GameOver();
            //    return;
            //}

            ////딜러 블랙잭
            //if (dealerHand[0].Value + dealerHand[1].Value == 21)
            //{
            //    pictureBox15.Image = DrawCard(dealerHand[0].Face, dealerHand[0].Pattern);
            //    MessageBox.Show("Dealer Blackjack! You Lose.");
            //    chips -= input;
            //    GameOver();
            //    return;
            //}
        }
        
        private void DeckPrint(string msg)
        {
            CheckForIllegalCrossThreadCalls = false;
            textBox3.Text = "" + msg;          
        }

        private void ChipPrint(string msg)
        {
            textBox4.Text = "" + msg;
        }

        private void UserDraw1(string msg1,string msg2, string msg3)
        {
            
            Card card = new Card();
            
            card.Face = (Face)Enum.Parse(typeof(Face), msg1);
            card.Pattern = (Pattern)Enum.Parse(typeof(Pattern), msg2);
            card.Value = int.Parse(msg3);
            userHand.Add(card);
            textBox1.Text = "" + UserSumValue();
            pictureBox1.Image = DrawCard(userHand[0].Face, userHand[0].Pattern);
        }

        private void UserDraw2(string msg1, string msg2,string msg3)
        {
            Card card = new Card();

            card.Face = (Face)Enum.Parse(typeof(Face), msg1);
            card.Pattern = (Pattern)Enum.Parse(typeof(Pattern), msg2);
            card.Value = int.Parse(msg3);
            userHand.Add(card);
            textBox1.Text = "" + UserSumValue();
            pictureBox2.Image = DrawCard(userHand[1].Face, userHand[1].Pattern);
        }

        private void UserDraw3(string msg1, string msg2, string msg3)
        {
            Card card = new Card();

            card.Face = (Face)Enum.Parse(typeof(Face), msg1);
            card.Pattern = (Pattern)Enum.Parse(typeof(Pattern), msg2);
            card.Value = int.Parse(msg3);
            userHand.Add(card);
            textBox1.Text = "" + UserSumValue();
            pictureBox3.Image = DrawCard(userHand[2].Face, userHand[2].Pattern);
        }

        private void UserDraw4(string msg1, string msg2, string msg3)
        {
            Card card = new Card();

            card.Face = (Face)Enum.Parse(typeof(Face), msg1);
            card.Pattern = (Pattern)Enum.Parse(typeof(Pattern), msg2);
            card.Value = int.Parse(msg3);
            userHand.Add(card);
            textBox1.Text = "" + UserSumValue();
            pictureBox4.Image = DrawCard(userHand[3].Face, userHand[3].Pattern);
        }

        private void UserDraw5(string msg1, string msg2, string msg3)
        {
            Card card = new Card();

            card.Face = (Face)Enum.Parse(typeof(Face), msg1);
            card.Pattern = (Pattern)Enum.Parse(typeof(Pattern), msg2);
            card.Value = int.Parse(msg3);
            userHand.Add(card);
            textBox1.Text = "" + UserSumValue();
            pictureBox5.Image = DrawCard(userHand[4].Face, userHand[4].Pattern);
        }

        private void DealerDraw1(string msg1, string msg2, string msg3)
        {
            
            Card card = new Card();

            card.Face = (Face)Enum.Parse(typeof(Face), msg1);
            card.Pattern = (Pattern)Enum.Parse(typeof(Pattern), msg2);
            card.Value = int.Parse(msg3);
            dealerHand.Add(card);
            pictureBox15.Image = Properties.Resources.뒷면;
        }

        private void DealerDraw2(string msg1, string msg2, string msg3)
        {
            Card card = new Card();

            card.Face = (Face)Enum.Parse(typeof(Face), msg1);
            card.Pattern = (Pattern)Enum.Parse(typeof(Pattern), msg2);
            card.Value = int.Parse(msg3);
            dealerHand.Add(card);
            //textBox2.Text = "" + DealerSumValue();
            pictureBox14.Image = DrawCard(dealerHand[1].Face, dealerHand[1].Pattern);
        }

        private void DealerDraw3(string msg1, string msg2, string msg3)
        {
            Card card = new Card();

            card.Face = (Face)Enum.Parse(typeof(Face), msg1);
            card.Pattern = (Pattern)Enum.Parse(typeof(Pattern), msg2);
            card.Value = int.Parse(msg3);
            dealerHand.Add(card);
            textBox2.Text = "" + DealerSumValue();
            pictureBox13.Image = DrawCard(dealerHand[2].Face, dealerHand[2].Pattern);
        }

        private void DealerDraw4(string msg1, string msg2, string msg3)
        {
            Card card = new Card();

            card.Face = (Face)Enum.Parse(typeof(Face), msg1);
            card.Pattern = (Pattern)Enum.Parse(typeof(Pattern), msg2);
            card.Value = int.Parse(msg3);
            dealerHand.Add(card);
            textBox2.Text = "" + DealerSumValue();
            pictureBox12.Image = DrawCard(dealerHand[3].Face, dealerHand[3].Pattern);
        }

        private void DealerDraw5(string msg1, string msg2, string msg3)
        {
            Card card = new Card();

            card.Face = (Face)Enum.Parse(typeof(Face), msg1);
            card.Pattern = (Pattern)Enum.Parse(typeof(Pattern), msg2);
            card.Value = int.Parse(msg3);
            dealerHand.Add(card);
            textBox2.Text = "" + DealerSumValue();
            pictureBox11.Image = DrawCard(dealerHand[4].Face, dealerHand[4].Pattern);
        }

        private int UserSumValue()
        {
            int sum = 0;
            foreach(Card card in userHand)
            {
                sum += card.Value;
            }
            return sum;
        }

        private int DealerSumValue()
        {
            int sum = 0;
            foreach (Card card in dealerHand)
            {
                sum += card.Value;
            }
            return sum;
        }

        //베팅버튼

        public void DealerBlackJack()
        {
            if (dealerHand[0].Value + dealerHand[1].Value == 21)
            {
                pictureBox15.Image = DrawCard(dealerHand[0].Face, dealerHand[0].Pattern);
                MessageBox.Show("Dealer Blackjack! You Lose.");
                chips -= input;
                GameOver();
                return;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SoundPlayer sound = new SoundPlayer(@"C:\Users\손언빈\Desktop\단기프로젝트(비트고급)\블랙잭 클라이언트\스탠드까지클라\Winform 프로젝트\블랙잭노래.wav");
            sound.Play();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.DrawImage(this.m_bmpBackground, 0, 0);
        }

    }
}