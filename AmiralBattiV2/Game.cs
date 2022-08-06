using AmiralBattiV2.ModelV3;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;

namespace AmiralBattiV2
{
    internal class Game
    {
        private static Control.ControlCollection _AddButton;
        public Control.ControlCollection AddButton { get { return _AddButton; } set { _AddButton = value; } }

        FirstPlayerCreateMap firstPlayerCreateMap;
        RivalPlayerCreateMap rivalPlayerCreateMap;

        public PictureBox _Bomba;
        public PictureBox _Carpi;
        public Label _fpSiraLbl;
        public Label _rpSiraLbl;
        public TextBox _ipAdress;
        public Button _ipButton;
        public Button _FPsecenekButton;
        public Button _RPsecenekButton;
        public bool AreYouFirstPlayer;
        public bool AreYouRivalPlayer; 
        public void AreYouFP(bool areYouFP)
        {
            AreYouFirstPlayer = areYouFP;
            AreYouRivalPlayer = !areYouFP;
            _rpSiraLbl.Text = "SIRA RAKİPTE";
        }
        public void AreYouRP(bool areYouRP)
        {
            AreYouRivalPlayer=areYouRP;
            AreYouFirstPlayer = !areYouRP;
            _fpSiraLbl.Text = "SIRA RAKİPTE";
        }

        public string IpAdress { get { return _IpAdress; } set { _IpAdress = value; } }
        public static string _IpAdress;

        TcpIp tcpIp = new TcpIp();
        public bool ConnectWithTcp()
        {
            bool soketConnected = false ;
            try
            {
                soketConnected = tcpIp.Connect(_IpAdress);

                firstPlayerCreateMap.EnableFirstButtons();
                rivalPlayerCreateMap.UnEnableRivalButtons();
                _ipAdress.Visible = false;
                _ipButton.Visible = false;
                _FPsecenekButton.Visible = false;
                _RPsecenekButton.Visible = false;
                MessageBox.Show("Karşı bilgisayara başarıyla bağlanıldı.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Karşı bilgisayara bağlanılırken hata oluştu.");
            }
            return soketConnected;
        }
        public bool WaitForConnect()                                    // eğer oyuncu rivalplayer ise bu metod çalışır ve metod sonunda WaitData() metodu çalıştırılarak karşı bilgisayarda hamle yapılan ilgili butonun bilgisi
        {                                                               // gelmesi beklenir.
            bool soketConnected = false;
            try
            {
                soketConnected = tcpIp.WaitConnect();
                rivalPlayerCreateMap.EnableRivalButtons();
                firstPlayerCreateMap.UnEnableFirstButtons();
                _ipAdress.Visible = false;
                _ipButton.Visible = false;
                _FPsecenekButton.Visible = false;
                _RPsecenekButton.Visible = false;
                MessageBox.Show("Bilgisayarınıza başarıyla bağlanıldı.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bilgisayarınıza bağlanılırken hata oluştu.");
            }
            return soketConnected;
        }
        
        public string FirstPlayerReady;
        public string RivalPlayerReady;

        public void isGameStarted()
        {
            if (AreYouFirstPlayer)
            {
                firstPlayerCreateMap.UnEnableFirstButtons();

                tcpIp.SendData(FirstPlayerReady);
                MessageBox.Show("Rakip oyuncu bekleniyor...");
                string rivalPlayerReady = tcpIp.WaitData();
                if (FirstPlayerReady == "hazir" && rivalPlayerReady == "hazir")
                {
                    isStarted = true;
                    MessageBox.Show("Oyun başladı!");

                    string firstTurn = FirstTurn();
                    SendTurnData(firstTurn);
                    if (firstTurn== "sirafirstplayerin")
                    {
                        MessageBox.Show("İlk hamle hakkı senin!");
                        rivalPlayerCreateMap.EnableRivalButtons();

                    }
                    else if (firstTurn == "sirarivalplayerin")
                    {
                        rivalPlayerCreateMap.UnEnableRivalButtons();
                        WaitButtonData();
                    }
                }
            }
            else if (AreYouRivalPlayer)
            {
                rivalPlayerCreateMap.UnEnableRivalButtons();

                tcpIp.SendData(RivalPlayerReady);
                MessageBox.Show("Rakip oyuncu bekleniyor...");
                string firstPlayerReady = tcpIp.WaitData();
                if (RivalPlayerReady == "hazir" && firstPlayerReady == "hazir")
                {
                    isStarted = true;
                    MessageBox.Show("Oyun başladı!");
                    string firstTurn = WaitTurnData();
                    if (firstTurn== "sirarivalplayerin")
                    {
                        MessageBox.Show("İlk hamle hakkı senin!");
                        firstPlayerCreateMap.EnableFirstButtons();
                    }
                    else if(firstTurn == "sirafirstplayerin")
                    {
                        firstPlayerCreateMap.UnEnableFirstButtons();
                        WaitButtonData();

                    }
                }
            }
        }
        public string FirstTurn()
        {
            Random random = new Random();

            int number = random.Next(0,2);
            if (number==0)                              //eğer rastgele üretilen değer 0 ise ilk sıra firstplayer ın olacaktır.
            {
                return "sirafirstplayerin";
            }
            else if (number==1)                         //eğer rastgele üretilen değer 1 ise ilk sıra rivalplayer ın olacaktır.
            {
                return "sirarivalplayerin";
            }
            return null;
        }

        public Game(Control.ControlCollection controlButton, PictureBox Bomba, PictureBox Carpi, Label fpSiraLbl, Label rpSiraLbl, TextBox ipAdress, Button ipButton, Button FPsecenekButton, Button RPsecenekButton)
        {
            AddButton = controlButton;
            firstPlayerCreateMap = new FirstPlayerCreateMap(_AddButton, new EventHandler(FirstPlayerCoordinate));
            rivalPlayerCreateMap = new RivalPlayerCreateMap(_AddButton, new EventHandler(RivalPlayerCoordinate));
            _Bomba = Bomba;
            _Carpi = Carpi;
            _fpSiraLbl = fpSiraLbl;
            _rpSiraLbl = rpSiraLbl;

            _ipAdress=ipAdress;
            _ipButton = ipButton;
            _FPsecenekButton = FPsecenekButton;
            _RPsecenekButton = RPsecenekButton;
        }

        static bool isTurnFirstPlayer = true;
        static bool isTurnRivalPlayer = false;
        public bool isStarted { get { return _isStarted; } set { _isStarted = value; } }
        private static bool _isStarted;

        Button firstPlayerDinamikButton;
        private static PictureBox _firstPlayerPicture;
        public PictureBox firstPlayerPicture
        {
            get { return _firstPlayerPicture; }
            set { _firstPlayerPicture = value; }
        }

        int firstPlayerDinamikButtonX, firstPlayerDinamikButtonY, firstPlayerPictureX, firstPlayerPictureY, firstPlayerPictureAltSinir;
        List<PictureBox> firstPictures = new List<PictureBox>();

        public void FirstPlayerCoordinate(object sender, EventArgs e)
        {
            firstPlayerDinamikButton = (sender as Button);
            firstPlayerDinamikButtonX = firstPlayerDinamikButton.Location.X;
            firstPlayerDinamikButtonY = firstPlayerDinamikButton.Location.Y;

            if (AreYouRivalPlayer)
            {
                SendButtonData(firstPlayerDinamikButtonX.ToString(), firstPlayerDinamikButtonY.ToString()); // firstplayer a, kendi haritasını kontrol edip, atış gemiye mi yapılmış bunu kontrol etmesi için hamle yapılan buton bilgileri gönderilir.

                string turnInfo = WaitTurnData();               //firstplayer ın, atışın isabet edip etmediği bilgisini "seninsiran" veya "benimsiram" şekilinde sıra bilgisi olarak göndermesi beklenir
                if (turnInfo == "RPkazandi")
                {
                    firstPlayerDinamikButton.BackgroundImage = _Bomba.Image;        // eğer firstplayerdan "seninsiran" bilgisi gelirse bu, isabet ettirildiği anlamına gelir ve hamle yapılmış olan butonun arkaplanına bomba resmi yerleştirilir.
                    firstPlayerDinamikButton.Enabled = false;
                    firstPlayerCreateMap.UnEnableFirstButtons();
                    rivalPlayerCreateMap.UnEnableRivalButtons();
                    _rpSiraLbl.Text = "KAZANDIN!";
                    _rpSiraLbl.Font = new Font("Microsoft Sans Serif", 50);
                    _rpSiraLbl.Visible = true;
                    MessageBox.Show("Tebrikler! Kazandın.");
                }
                else if (turnInfo == "seninsiran")
                {
                    isTurnRivalPlayer = true;
                    isTurnFirstPlayer = false;
                    firstPlayerCreateMap.EnableFirstButtons();

                    firstPlayerDinamikButton.BackgroundImage = _Bomba.Image;        // eğer firstplayerdan "seninsiran" bilgisi gelirse bu, isabet ettirildiği anlamına gelir ve hamle yapılmış olan butonun arkaplanına bomba resmi yerleştirilir.
                    firstPlayerDinamikButton.Enabled = false;
                    _rpSiraLbl.Visible = true;
                    _fpSiraLbl.Visible = false;
                }
                else if (turnInfo == "benimsiram")
                {
                    isTurnFirstPlayer = true;
                    isTurnRivalPlayer = false;
                    firstPlayerCreateMap.UnEnableFirstButtons();

                    firstPlayerDinamikButton.BackgroundImage = _Carpi.Image;       // eğer firstplayerdan "benimsiram" bilgisi gelirse bu, atışın isabet etmediği anlamına gelir ve hamle yapılmış olan butonun arkaplanına çarpı resmi yerleştirilir.
                    firstPlayerDinamikButton.Enabled = false;
                    _rpSiraLbl.Visible = false;
                    _fpSiraLbl.Visible = true;
                    WaitButtonData();
                }
            }
            else if (AreYouFirstPlayer)
            {
                try
                {
                    firstPlayerPictureX = firstPlayerDinamikButtonX;
                    firstPlayerPictureY = firstPlayerDinamikButtonY + (firstPlayerDinamikButton.Height / 2) - (_firstPlayerPicture.Height / 2);
                    firstPlayerPictureAltSinir = firstPlayerPictureY + _firstPlayerPicture.Size.Height;

                    if (firstPlayerPictureY < 0 || firstPlayerPictureAltSinir > 400)
                    {
                        throw new OutOfMapException();
                    }

                    foreach (var pic in firstPictures)
                    {
                        if (pic.Location.X == firstPlayerPictureX)          // x' ler eşit ise y bilgileri kontrol edilmek üzere içerideki if bloguna girilir.
                        {
                            if (pic.Location.Y <= firstPlayerPictureY && firstPlayerPictureY < pic.Location.Y + pic.Height)   // bu blokta, yerleştirilmiş ve konum bilgileri depolanmış olan resimlerin konum bilgilerine ulaşıldıktan sonra istenilen resim üzerine resim yerleştirilmesinin önüne geçilir
                            {                                                                                                 // bu işlem, yerleştirilmek istenen resimin sol üst ve sol alt noktalarını ayrı ayrı kontrol ederek gerçekleştirilir.
                                throw new OverlappingImageException();
                                break;                                                                                          //bu if bloğunda ilk olarak yerleştirilmek istenen resimin üst noktası kontrol edilir.
                            }
                            else if (pic.Location.Y < (firstPlayerPictureY + _firstPlayerPicture.Height) && (firstPlayerPictureY + _firstPlayerPicture.Height) <= pic.Location.Y + pic.Height)
                            {
                                throw new OverlappingImageException();
                            }
                        }
                    }
                    _firstPlayerPicture.Location = new Point(firstPlayerPictureX, firstPlayerPictureY);
                    firstPictures.Add(_firstPlayerPicture);
                }
                catch (NullReferenceException)
                {
                    if (isStarted)
                    {
                        IsOnShipOrNot isOn = new IsOnShipOrNot();                       // bu blokta basılan butona herhangi bir geminin karşılık gelip gelmediğini kontrol ediyorum
                        var result = isOn.isOnShipOrNot(firstPlayerDinamikButton, new FirstPlayer());
                        isTurnRivalPlayer = result;
                        isTurnFirstPlayer = !result;

                        if (isTurnRivalPlayer)
                        {
                            firstPlayerDinamikButton.BackgroundImage = _Bomba.Image;
                            firstPlayerDinamikButton.Enabled = false;
                            _rpSiraLbl.Visible = true;
                            _fpSiraLbl.Visible = false;
                            rivalPlayerCreateMap.UnEnableRivalButtons();

                            bool isWinFP = firstPlayerCreateMap.IsLoseFirstP(_Bomba.Image);
                            if (isWinFP)
                            {
                                firstPlayerCreateMap.UnEnableFirstButtons();
                                rivalPlayerCreateMap.UnEnableRivalButtons();
                                SendTurnData("RPkazandi");
                                MessageBox.Show("Oyunu rakip oyuncu kazandı!");
                            }
                            else
                            {
                                SendTurnData("seninsiran");
                                WaitButtonData();
                            }
                        }
                        else
                        {
                            firstPlayerDinamikButton.BackgroundImage = _Carpi.Image;
                            firstPlayerDinamikButton.Enabled = false;
                            _rpSiraLbl.Visible = false;
                            _fpSiraLbl.Visible = true;
                            rivalPlayerCreateMap.EnableRivalButtons();
                            SendTurnData("benimsiram");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Lütfen yerleştirilecek gemiyi seçiniz!");
                    }
                }
                catch (OutOfMapException)
                {
                    MessageBox.Show("Lütfen gemiyi haritanın dışına çıkmayacak şekilde yerleştiriniz");
                }
                catch (OverlappingImageException)
                {
                    MessageBox.Show("Gemileri birbirlerinin üzerine gelmeyecek şekilde yerleştiriniz!");
                }
            }
        }
        //**********************************************************RAKİP OYUNCUNUN DİNAMİK BUTON EVENTİ BAŞLANGIÇ****************************************      SIRA FİRSPLAYER IN OLDUĞU ZAMAN BURASI ÇALIŞIR.
        private static PictureBox _rivalPlayerPicture;
        public PictureBox rivalPlayerPicture
        {
            get { return _rivalPlayerPicture; }
            set { _rivalPlayerPicture = value; }
        }

        Button rivalPlayerDinamikButton;
        int rivalPlayerDinamikButtonX, rivalPlayerDinamikButtonY, rivalPlayerPictureX, rivalPlayerPictureY, rivalPlayerPictureAltSinir;
        List<PictureBox> rivalPictures = new List<PictureBox>();

        public void RivalPlayerCoordinate(object sender, EventArgs e)
        {
            rivalPlayerDinamikButton = (sender as Button);
            rivalPlayerDinamikButtonX = rivalPlayerDinamikButton.Location.X;
            rivalPlayerDinamikButtonY = rivalPlayerDinamikButton.Location.Y;
            if (AreYouFirstPlayer)
            {
                SendButtonData(rivalPlayerDinamikButtonX.ToString(), rivalPlayerDinamikButtonY.ToString()); // rivalplayer a, kendi haritasını kontrol edip, atış gemiye mi yapılmış bunu kontrol etmesi için hamle yapılan buton bilgileri gönderilir.

                string turnInfo = WaitTurnData();               //rivalplayer ın, atışın isabet edip etmediği bilgisini "seninsiran" veya "benimsiram" şekilinde sıra bilgisi olarak göndermesi beklenir

                if (turnInfo == "FPkazandi")
                {
                    rivalPlayerDinamikButton.BackgroundImage = _Bomba.Image;
                    rivalPlayerDinamikButton.Enabled = false;
                    firstPlayerCreateMap.UnEnableFirstButtons();
                    rivalPlayerCreateMap.UnEnableRivalButtons();
                    _fpSiraLbl.Text = "KAZANDIN!";
                    _fpSiraLbl.Font = new Font("Microsoft Sans Serif", 50);
                    _fpSiraLbl.Visible = true;
                    MessageBox.Show("Tebrikler! Kazandın.");
                }
                else if (turnInfo == "seninsiran")
                {
                    isTurnFirstPlayer = true;
                    isTurnRivalPlayer = false;
                    rivalPlayerCreateMap.EnableRivalButtons();

                    rivalPlayerDinamikButton.BackgroundImage = _Bomba.Image;        // eğer rivalplayerdan "seninsiran" bilgisi gelirse bu, isabet ettirildiği anlamına gelir ve hamle yapılmış olan butonun arkaplanına bomba resmi yerleştirilir.
                    rivalPlayerDinamikButton.Enabled = false;
                    _rpSiraLbl.Visible = false;
                    _fpSiraLbl.Visible = true;
                }
                else if (turnInfo == "benimsiram")
                {
                    isTurnFirstPlayer = false;
                    isTurnRivalPlayer = true;
                    rivalPlayerCreateMap.UnEnableRivalButtons();

                    rivalPlayerDinamikButton.BackgroundImage = _Carpi.Image;       // eğer rivalplayerdan "benimsiram" bilgisi gelirse bu, atışın isabet etmediği anlamına gelir ve hamle yapılmış olan butonun arkaplanına çarpı resmi yerleştirilir.
                    rivalPlayerDinamikButton.Enabled = false;
                    _rpSiraLbl.Visible = true;
                    _fpSiraLbl.Visible = false;
                    WaitButtonData();
                }

            }
            else if (AreYouRivalPlayer)
            {
                try
                {
                    rivalPlayerPictureX = rivalPlayerDinamikButtonX;
                    rivalPlayerPictureY = rivalPlayerDinamikButtonY + (rivalPlayerDinamikButton.Height / 2) - (_rivalPlayerPicture.Height / 2);
                    rivalPlayerPictureAltSinir = rivalPlayerPictureY + _rivalPlayerPicture.Size.Height;

                    if (rivalPlayerPictureY < 0 || rivalPlayerPictureAltSinir > 400)
                    {
                        throw new OutOfMapException();
                    }

                    foreach (var pic in rivalPictures)
                    {
                        if (pic.Location.X == rivalPlayerPictureX)          // x' ler eşit ise y bilgileri kontrol edilmek üzere içerideki if bloguna girilir.
                        {
                            if (pic.Location.Y <= rivalPlayerPictureY && rivalPlayerPictureY < pic.Location.Y + pic.Height)   // bu blokta, yerleştirilmiş ve konum bilgileri depolanmış olan resimlerin konum bilgilerine ulaşıldıktan sonra istenilen resim üzerine resim yerleştirilmesinin önüne geçilir
                            {                                                                                                 // bu işlem, yerleştirilmek istenen resimin sol üst ve sol alt noktalarını ayrı ayrı kontrol ederek gerçekleştirilir.
                                throw new OverlappingImageException();
                                break;                                                                                          //bu if bloğunda ilk olarak yerleştirilmek istenen resimin üst noktası kontrol edilir.
                            }
                            else if (pic.Location.Y < (rivalPlayerPictureY + _rivalPlayerPicture.Height) && (rivalPlayerPictureY + _rivalPlayerPicture.Height) <= pic.Location.Y + pic.Height)
                            {
                                throw new OverlappingImageException();
                            }
                        }
                    }
                    _rivalPlayerPicture.Location = new Point(rivalPlayerPictureX, rivalPlayerPictureY);
                    rivalPictures.Add(_rivalPlayerPicture);
                }
                catch (NullReferenceException)
                {
                    if (isStarted)                  // bu blokta basılan butona herhangi bir geminin karşılık gelip gelmediğini kontrol ediyorum
                    {
                        IsOnShipOrNot isOn = new IsOnShipOrNot();
                        var result1 = isOn.isOnShipOrNot(rivalPlayerDinamikButton, new RivalPlayer());
                        isTurnFirstPlayer = result1;
                        isTurnRivalPlayer = !result1;
                        if (isTurnFirstPlayer)
                        {
                            rivalPlayerDinamikButton.BackgroundImage = _Bomba.Image;
                            rivalPlayerDinamikButton.Enabled = false;
                            _rpSiraLbl.Visible = false;
                            _fpSiraLbl.Visible = true;
                            firstPlayerCreateMap.UnEnableFirstButtons();

                            bool isWinFP = rivalPlayerCreateMap.IsLoseRivalP(_Bomba.Image);    // oyuncunun bütün gemileri bombalayıp bombalamadığı kontrol edilir. Duruma göre oyunu durdurup kazanan belirlenir.
                            if (isWinFP)
                            {
                                firstPlayerCreateMap.UnEnableFirstButtons();
                                rivalPlayerCreateMap.UnEnableRivalButtons();
                                SendTurnData("FPkazandi");
                                MessageBox.Show("Oyunu rakip oyuncu kazandı!");
                            }
                            else
                            {
                                SendTurnData("seninsiran");
                                WaitButtonData();
                            }
                        }
                        else
                        {
                            rivalPlayerDinamikButton.BackgroundImage = _Carpi.Image;
                            rivalPlayerDinamikButton.Enabled = false;
                            _rpSiraLbl.Visible = true;
                            _fpSiraLbl.Visible = false;
                            firstPlayerCreateMap.EnableFirstButtons();
                            SendTurnData("benimsiram");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Yerleştirilecek gemiyi seçiniz");
                    }
                }
                catch (OutOfMapException)
                {
                    MessageBox.Show("Gemiyi haritanın dışına çıkmayacak şekilde yerleştiriniz!");
                }
                catch (OverlappingImageException)
                {
                    MessageBox.Show("Gemileri birbirlerinin üzerine gelmeyecek şekilde yerleştiriniz!");
                }
            }

        }

        public void SendButtonData(string X, string Y)
        {
            tcpIp.SendData(X + "," + Y);
        }

        public void WaitButtonData()                              // rakip oyuncudan, hamle yaptığı butonun bilgisi beklenir.
        {
            int[] locationButton = tcpIp._WaitButtonData();
            int X = locationButton[0];
            int Y = locationButton[1];
            if (AreYouRivalPlayer)
            {
                foreach (var button in rivalPlayerCreateMap.dinamikRivalButtons)
                {
                    if (button.Location.X == X && button.Location.Y == Y)
                    {
                        RivalPlayerCoordinate(button, new EventArgs());
                        break;
                    }
                }
            }
            else if (AreYouFirstPlayer)
            {
                foreach (var button in firstPlayerCreateMap.dinamikFirstButtons)
                {
                    if (button.Location.X == X && button.Location.Y == Y)
                    {
                        FirstPlayerCoordinate(button, new EventArgs());
                        break;
                    }
                }

            }
        }
        public void SendTurnData(string data)
        {
            tcpIp.SendData(data);
        }

        public string WaitTurnData()                    
        {
            string turn = tcpIp.WaitData();
            return turn;
        }
    }
}
