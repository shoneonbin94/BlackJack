using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Winform_프로젝트
{
    //컨트롤: 데이터 주고 & 받고
    //  1. 서버에 초기금액,덱을 받고          (게임스타트) 
    //  2. 서버에 나의 배팅금액을 주고        (베팅)
    //  3. 서버에 나의 카드상태를 받고        (베팅 이후)
    //  4. 서버에 딜러의 카드상태를 받고       (베팅 이후)
    //  5. 서버에 HIT, STAND를 요청하고      (HIT, STAND)
    //  5-1. 서버에 나의 카드상태를 받고       (HIT)
    //  5-2. 서버에 나의 카드상태를 주고       (STAND)
    //  6. 서버에 딜러의 카드상태를 받고       (STAND이후)
    //  7. 서버에 승패 여부를 받고            (게임끝)
    public delegate void BlackDel(string msg);  // 델리게이트선언
    public delegate void BlackDel2(string msg1, string msg2,string msg3);
    public delegate void BlackDel3();

    class Control 
    {
        public static int c = 0;
        public static int d = 0;
        public event BlackDel ReturntoDeck;         //이벤트 선언
        public event BlackDel ReturntoChips;
        public event BlackDel2 ReturntoUserHand1;
        public event BlackDel2 ReturntoUserHand2;
        public event BlackDel2 ReturntoUserHand3;
        public event BlackDel2 ReturntoUserHand4;
        public event BlackDel2 ReturntoUserHand5;

        public event BlackDel2 ReturntoDealerHand1;
        public event BlackDel2 ReturntoDealerHand2;
        public event BlackDel2 ReturntoDealerHand3;
        public event BlackDel2 ReturntoDealerHand4;
        public event BlackDel2 ReturntoDealerHand5;

        public event BlackDel3 DGameOver;
        public event BlackDel3 Bust;

        wbClient client;

        private void AllClear()
        {
            ReturntoDeck=null;         //이벤트 선언
            ReturntoChips=null;
            ReturntoUserHand1 = null;
            ReturntoUserHand2 = null;
            ReturntoUserHand3 = null;
            ReturntoUserHand4 = null;
            ReturntoUserHand5 = null;
            ReturntoDealerHand1 = null;
            ReturntoDealerHand2 = null;
            ReturntoDealerHand3 = null;
            ReturntoDealerHand4 = null;
            ReturntoDealerHand5 = null;
            c = 0;
            d = 0;
        }


        public Control()
        {
            client = new wbClient(RecvData);
            client.ClientStart("192.168.0.37");
        }

        // 데이터 받기
        public void RecvData(byte[] msg)
        {
            string str = Encoding.Default.GetString(msg);
            string[] token = str.Split('\a');
            switch (token[0].Trim())
            {
                case "ACK_BLACK_GAMESTART_S":   DeckPrint(token[1]);    break;
                case "ACK_BLACK_GAMESTART_F":   MessageBox.Show("잔액부족!!!"); break;
                case "ACK_BLACK_BETTING":   UserCadrPrint(token[1]); break;
                case "ACK_BLACK_HIT_S": RecvHit(token[1]); break;
                case "ACK_BLACK_STAND_S": RecvStand(token[1]); break;
            }
        }

        public void GameStart()
        {
            
            //전송
            string str = null;
            str += "PACK_BLAKC_GAMESTART"+"\a";

            byte[] msg = Encoding.Default.GetBytes(str);
            client.SendData(msg);
        }

        // 받은 데이터 출력(딜러카드,타플레이어카드, 메시지박스)
        private void DeckPrint(string msg)
        { 
            string[] token = msg.Split('#');
            string deck = token[0];
            string chips = token[1];

            ReturntoDeck(deck); //호출
            ReturntoChips(chips);         

        }

        public void Betting()
        {
            //베팅 금액 전송 int(input)
            //유저 카드 드로우 해달라 요청
            int bet = Form1.input;

            string str = null;
            str += "PACK_BLAKC_BETTING\a";
            str += bet;

            byte[] msg = Encoding.Default.GetBytes(str);
            client.SendData(msg);
        }

        public void UserCadrPrint(string msg)
        {
            string[] token = msg.Split('#');

            string userHandFace = token[0];
            string userHandPattern = token[1];
            string userHandValue = token[2];

            string userHandFace1 = token[3];
            string userHandPattern1 = token[4];
            string userHandValue1 = token[5];

            int usersum = int.Parse(token[6]);

            string dealerHandFace = token[7];
            string dealerHandPattern = token[8];
            string dealerHandValue = token[9];

            string dealerHandFace1 = token[10];
            string dealerHandPattern1 = token[11];
            string dealerHandValue1 = token[12];

            ReturntoUserHand1(userHandFace, userHandPattern,userHandValue);
            ReturntoUserHand2(userHandFace1, userHandPattern1, userHandValue1);
            ReturntoDealerHand1(dealerHandFace, dealerHandPattern, dealerHandValue);
            ReturntoDealerHand2(dealerHandFace1, dealerHandPattern1, dealerHandValue1);
        }

        //히트(클라이언트 히트요청,카드한장 데이터 받음)
        public void Hit()
        {
            //전송
            string str = null;
            str += "PACK_BLAKC_HIT\a";

            byte[] msg = Encoding.Default.GetBytes(str);
            client.SendData(msg);
        }

        public void RecvHit(string msg)
        {
            string[] token = msg.Split('#');
            Thread.Sleep(2000);
            string userHandFace = token[0];
            string userHandPattern = token[1];
            string userHandValue = token[2];
            int uservalue = int.Parse(token[3]);

            switch(c)
            {
                case 0: ReturntoUserHand3(userHandFace, userHandPattern, userHandValue); c++; break;
                case 1: ReturntoUserHand4(userHandFace, userHandPattern, userHandValue); c++; break;
                case 2: ReturntoUserHand5(userHandFace, userHandPattern, userHandValue); c = 0; break;
            }
            if(uservalue >21)
            {
                MessageBox.Show("Busted!");
                Bust();

            }
            
        }

        public void Stand()
        {
            //전송
            string str = null;
            str += "PACK_BLAKC_STAND\a";

            byte[] msg = Encoding.Default.GetBytes(str);
            client.SendData(msg);
        }

        public void RecvStand(string msg)
        {

            string[] token = msg.Split('#');

            switch(token[0])
            {
                case "DEALER_LOSE": DealerLose(token[1]); break;
                case "DEALER_WIN":  DealerWin(token[1]); break;
                case "DEALER_SAME": MessageBox.Show("비겼음"); DGameOver(); break;
                case "DEALER_DRAW": DealerDraw(token[1]); break;
            }
        }

        public void DealerLose(string msg)
        {
            string[] token = msg.Split('#');
            string dealervalue = token[0];

            MessageBox.Show("딜러의패: " + dealervalue);
            MessageBox.Show("You Win");

            DGameOver();
            AllClear();
        }

        public void DealerWin(string msg)
        {
            string[] token = msg.Split('#');
            string dealervalue = token[0];

            MessageBox.Show("딜러의패: " + dealervalue);
            MessageBox.Show("You Lose");
            DGameOver();
            AllClear();
        }

        public void DealerDraw(string msg)
        {
            string[] token = msg.Split('@');

            Thread.Sleep(2000);
            string DealerHandFace = token[0];
            string DealerHandPattern = token[1];
            string DealerHandValue = token[2];

            switch (d)
            {
                case 0: ReturntoDealerHand3(DealerHandFace, DealerHandPattern, DealerHandValue); d++; break;
                case 1: ReturntoDealerHand4(DealerHandFace, DealerHandPattern, DealerHandValue); d++; break;
                case 2: ReturntoDealerHand5(DealerHandFace, DealerHandPattern, DealerHandValue); d = 0; break;
            }
            string str = null;
            str = "PACK_BLAKC_STAND" + "\a";

            byte[] msgg = Encoding.Default.GetBytes(str);
            client.SendData(msgg);

        }
    }
}
