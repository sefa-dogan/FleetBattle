using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
//using AmiralBattiV2.Database;
using AmiralBattiV2.ModelV3;
using Microsoft.Win32;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Net;

namespace AmiralBattiV2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Game game;

        //RivalPlayer rivalPlayer = new RivalPlayer();
        //FirstPlayer firstPlayer = new FirstPlayer();
        //FirstPlayerCreateMap _firstPlayerCreateMap = new FirstPlayerCreateMap();
        //RivalPlayerCreateMap _rivalPlayerCreateMap = new RivalPlayerCreateMap();

        private void Form1_Load(object sender, EventArgs e)
        {

            /*
            //HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\Instance Names
            //Eğer bu bilgisayarda SQL SERVER veya SQLSERVEREXPRESS sürümü yüklendi ise yukarıda regedit bölümünde yüklü SQL SERVER instance'leri yer alacaktır.           
            string[] yuklusqller = (string[])Registry.LocalMachine.OpenSubKey("Software").OpenSubKey("Microsoft").OpenSubKey("Microsoft SQL Server").GetValue("InstalledInstances");
            //Eğer kullanıcının bilgisayarında SQLExpress yüklü değilse
            var yukluozellikler = (from s in yuklusqller
                                   where s.Contains("SQLEXPRESS")
                                   select s).FirstOrDefault();
            if (yukluozellikler == null)
            {
                DialogResult sonuc = MessageBox.Show("Programı kullanabilmek için SQLEXPRESS yüklenmelidir. Yüklemek istiyor musunuz?", "UYARI", MessageBoxButtons.YesNo);
                if (sonuc == DialogResult.Yes)
                {
                    string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\SQLEXPR.exe";
                    Process p = new Process();
                    p.StartInfo.FileName = path;
                    //Aşağıdaki argumanları (parametreleri) SQLEXPRESS setup dosyama göndererek kurulumu başlatırsam kullanıcıya kurulum yeri v.s gibi bilgileri sormayacak ve doğrudan kuruluma geçecektir.
                    p.StartInfo.Arguments = "/qb INSTANCENAME=\"SQLEXPRESS\" INSTALLSQLDIR=\"C:\\Program Files\\Microsoft SQL Server\" INSTALLSQLSHAREDDIR=\"C:\\Program Files\\Microsoft SQL Server\" INSTALLSQLDATADIR=\"C:\\Program Files\\Microsoft SQL Server\" ADDLOCAL=\"All\" SQLAUTOSTART=1 SQLBROWSERAUTOSTART=0 SQLBROWSERACCOUNT=\"NT AUTHORITY\\SYSTEM\" SQLACCOUNT=\"NT AUTHORITY\\SYSTEM\" SECURITYMODE=SQL SAPWD=\"\" SQLCOLLATION=\"SQL_Latin1_General_Cp1_CS_AS\" DISABLENETWORKPROTOCOLS=0 ERRORREPORTING=1 ENABLERANU=0";
                    //Process için pencere oluşturma.
                    p.StartInfo.CreateNoWindow = true;
                    //Process gizli çalışsın.
                    p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    p.Start();
                    //İşlem bitene kadar bekle
                    p.WaitForExit();
                }
                else
                {
                    //Eğer SQLEXPRESS'i kurmak istemiyorsa programı sonlandırıyorum.
                    this.Close();
                }
            }
            */

            game = new Game(Controls, bomba, carpi, firstPlayerSiraLbl, rivalPlayerSiraLbl, tBxIpAdress, connectIpbtn, FirstPlayerSecenekBtn, RivalPlayerSecenekBtn);
            //LoadTable();
        }
        /*
        private void Form1_Activated(object sender, EventArgs e)

        {
            CreateDB();
        }

        void CreateDB()
        {
            SqlConnection con = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=master;Integrated security=true");
            //Aşağıdaki sorgu SQLEXPRESS üzerinde bizim veritabanımızın (SETUPPROJESI) olup olmadığını kontrol ediyor ve eğer yoksa böyle bir veritabanı oluşturuyor.
            SqlCommand cmd = new SqlCommand("if not exists(select * from sys.databases where name = 'SETUPPROJESI') begin CREATE DATABASE SETUPPROJESI ON PRIMARY (NAME = SETUPPROJESI_Data,FILENAME = 'C:\\SETUPPROJESI.mdf',SIZE = 3MB,MAXSIZE = 10MB, FILEGROWTH = 10%) LOG ON (NAME = SETUPPROJESI_Log,FILENAME = 'C:\\SETUPPROJESI.ldf',SIZE = 1MB,MAXSIZE = 5MB,FILEGROWTH = 10%) end");
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            //Şimdi de veritabanımız içerisinde tablomuzun olup olmadığına bakalım ve eğer tablomuz yoksa tablomuzu oluşturalım ve verilerimizi atalım.
            con.ConnectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=SETUPPROJESI;Integrated security=true";
            //Aşağıdaki sorgu Kullanicilar tablosunun olup olmadığına bakmakta ve eğer yoksa oluşturarak içerisine kayıtları eklemektedir.
            cmd.CommandText = "if not exists(select * from sys.tables where name = 'Kullanicilar') begin create table Kullanicilar(ID int identity(1,1) primary key,Ad varchar(20),Soyad varchar(20) ) insert into Kullanicilar (Ad,Soyad) values('Erol','ORTATEPE') insert into Kullanicilar (Ad,Soyad) values('Elvan','EFE') insert into Kullanicilar (Ad,Soyad) values('Yasin','YILMAZ') insert into Kullanicilar (Ad,Soyad) values('İbrahim','ELMAS') insert into Kullanicilar (Ad,Soyad) values('Yasin','YILMAZ') insert into Kullanicilar (Ad,Soyad) values('Cengiz','EFE') insert into Kullanicilar (Ad,Soyad) values('Olcay','BÜYÜKÇAPAR') end";
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
        */
        /*
        private void LoadTable()
        {
            using (PlayerInfoV3Entities1 model = new PlayerInfoV3Entities1())
            {
                FirstPlayer_dgw1.DataSource = model.FirstPlayers.ToList();
                RivalPlayer_dgw1.DataSource = model.RivalPlayers.ToList();

            }
            //PlayerInfoV2Entities model = new PlayerInfoV2Entities();
            //var veri=model.FirstPlayerInfoes.ToList();
        }
        */
        private void FirstPlayer_rBtn5brm_Click(object sender, EventArgs e)
        {
            game.firstPlayerPicture = FirstPlayer_pBx5brm;
        }

        private void FirstPlayer_rBtn3brm_Click(object sender, EventArgs e)
        {
            game.firstPlayerPicture = FirstPlayer_pBx3brm;
        }

        private void FirstPlayer_rBtn1brm_Click(object sender, EventArgs e)
        {
            game.firstPlayerPicture = FirstPlayer_pBx1brm;
        }

        private void RivalPlayer_rBtn5brm_Click(object sender, EventArgs e)
        {
            game.rivalPlayerPicture = RivalPlayer_pBx5brm;
        }

        private void RivalPlayer_rBtn3brm_Click(object sender, EventArgs e)
        {
            game.rivalPlayerPicture = RivalPlayer_pBx3brm;
        }

        private void RivalPlayer_rBtn1brm_Click(object sender, EventArgs e)
        {
            game.rivalPlayerPicture = RivalPlayer_pBx1brm;
        }

        private void readyFirstPlayer_Click(object sender, EventArgs e)
        {

            PlayerInfoV3Entities1 db = new PlayerInfoV3Entities1();
            var updatedValue1 = db.FirstPlayers.Find(1);
            updatedValue1.ShipName = FirstPlayer_pBx5brm.Name;
            updatedValue1.X = FirstPlayer_pBx5brm.Location.X;
            updatedValue1.Y = FirstPlayer_pBx5brm.Location.Y;
            updatedValue1.Width = FirstPlayer_pBx5brm.Width;
            updatedValue1.Height = FirstPlayer_pBx5brm.Height;


            var updatedValue2 = db.FirstPlayers.Find(2);
            updatedValue2.ShipName = FirstPlayer_pBx3brm.Name;
            updatedValue2.X = FirstPlayer_pBx3brm.Location.X;
            updatedValue2.Y = FirstPlayer_pBx3brm.Location.Y;
            updatedValue2.Width = FirstPlayer_pBx3brm.Width;
            updatedValue2.Height = FirstPlayer_pBx3brm.Height;

            var updatedValue3 = db.FirstPlayers.Find(3);
            updatedValue3.ShipName = FirstPlayer_pBx1brm.Name;
            updatedValue3.X = FirstPlayer_pBx1brm.Location.X;
            updatedValue3.Y = FirstPlayer_pBx1brm.Location.Y;
            updatedValue3.Width = FirstPlayer_pBx1brm.Width;
            updatedValue3.Height = FirstPlayer_pBx1brm.Height;

            db.SaveChanges();

            //LoadTable();

            FirstPlayer_rBtn5brm.Enabled = false;
            FirstPlayer_rBtn3brm.Enabled = false;
            FirstPlayer_rBtn1brm.Enabled = false;
            FirstPlayer_rBtn5brm.Visible = false;
            FirstPlayer_rBtn3brm.Visible = false;
            FirstPlayer_rBtn1brm.Visible = false;


            FirstPlayer_pBx5brm.Visible = false;
            FirstPlayer_pBx3brm.Visible = false;
            FirstPlayer_pBx1brm.Visible = false;

            readyFirstPlayer.Visible = false;
            game.FirstPlayerReady = "hazir";
            game.firstPlayerPicture = null;
            game.isGameStarted();
        }


        private void readyRivalPlayer_Click(object sender, EventArgs e)
        {
            PlayerInfoV3Entities1 db = new PlayerInfoV3Entities1();
            var updatedValue1 = db.RivalPlayers.Find(1);
            updatedValue1.ShipName = RivalPlayer_pBx5brm.Name;
            updatedValue1.X = RivalPlayer_pBx5brm.Location.X;
            updatedValue1.Y = RivalPlayer_pBx5brm.Location.Y;
            updatedValue1.Width = RivalPlayer_pBx5brm.Width;
            updatedValue1.Height = RivalPlayer_pBx5brm.Height;

            var updatedValue2 = db.RivalPlayers.Find(2);
            updatedValue2.ShipName = RivalPlayer_pBx3brm.Name;
            updatedValue2.X = RivalPlayer_pBx3brm.Location.X;
            updatedValue2.Y = RivalPlayer_pBx3brm.Location.Y;
            updatedValue2.Width = RivalPlayer_pBx3brm.Width;
            updatedValue2.Height = RivalPlayer_pBx3brm.Height;

            var updatedValue3 = db.RivalPlayers.Find(3);
            updatedValue3.ShipName = RivalPlayer_pBx1brm.Name;
            updatedValue3.X = RivalPlayer_pBx1brm.Location.X;
            updatedValue3.Y = RivalPlayer_pBx1brm.Location.Y;
            updatedValue3.Width = RivalPlayer_pBx1brm.Width;
            updatedValue3.Height = RivalPlayer_pBx1brm.Height;
            db.SaveChanges();

            //LoadTable();
            game.rivalPlayerPicture = null;

            //readyFirstPlayer.Visible = false;
            readyRivalPlayer.Visible = false;

            RivalPlayer_rBtn5brm.Enabled = false;
            RivalPlayer_rBtn3brm.Enabled = false;
            RivalPlayer_rBtn1brm.Enabled = false;
            RivalPlayer_rBtn5brm.Visible = false;
            RivalPlayer_rBtn3brm.Visible = false;
            RivalPlayer_rBtn1brm.Visible = false;

            RivalPlayer_pBx5brm.Visible = false;
            RivalPlayer_pBx3brm.Visible = false;
            RivalPlayer_pBx1brm.Visible = false;

            //FirstPlayer_pBx5brm.Visible = false;
            //FirstPlayer_pBx3brm.Visible = false;
            //FirstPlayer_pBx1brm.Visible = false;
            game.RivalPlayerReady = "hazir";
            game.isGameStarted();
            //StartedGame();
        }

        //private void StartedGame()
        //{
        //    game.isStarted = true;
        //    label1.Text = "Oyun Başladı";
        //    game.SiraKontrol();

        //}

        private void FirstPlayerSecenekBtn_Click(object sender, EventArgs e)
        {
            tBxIpAdress.Visible = true;
            connectIpbtn.Visible = true;

            findIpAdressbtn.Visible = false;
            lblIpAdress.Visible = false;
            rivalPlayerIpAdress.Visible = false;
            FirstPlayerSecenekBtn.Enabled = false;
            RivalPlayerSecenekBtn.Enabled = false;
            game.AreYouFP(true);
        }

        private void RivalPlayerSecenekBtn_Click(object sender, EventArgs e)
        {

            RivalPlayerSecenekBtn.Enabled = false;
            FirstPlayerSecenekBtn.Enabled = false;
            game.AreYouRP(true);
            bool state = game.WaitForConnect();              // bilgisayara bağlanılmasını bekler.

            RivalPlayer_pBx1brm.Visible = state;
            RivalPlayer_pBx1brm.Enabled = state;

            RivalPlayer_pBx3brm.Visible = state;
            RivalPlayer_pBx3brm.Enabled = state;

            RivalPlayer_pBx5brm.Visible = state;
            RivalPlayer_pBx5brm.Enabled = state;

            RivalPlayer_rBtn1brm.Visible = state;
            RivalPlayer_rBtn1brm.Enabled = state;

            RivalPlayer_rBtn3brm.Visible = state;
            RivalPlayer_rBtn3brm.Enabled = state;

            RivalPlayer_rBtn5brm.Visible = state;
            RivalPlayer_rBtn5brm.Enabled = state;

            readyRivalPlayer.Visible = state;
            readyRivalPlayer.Enabled = state;

        }

        private void connectIpbtn_Click(object sender, EventArgs e)
        {
            game.IpAdress = tBxIpAdress.Text;
            bool state = game.ConnectWithTcp();              // karşı bilgisayara bağlanmaya çalışır.

            FirstPlayer_pBx1brm.Visible = state;
            FirstPlayer_pBx1brm.Enabled = state;

            FirstPlayer_pBx3brm.Visible = state;
            FirstPlayer_pBx3brm.Enabled = state;

            FirstPlayer_pBx5brm.Visible = state;
            FirstPlayer_pBx5brm.Enabled = state;

            FirstPlayer_rBtn1brm.Visible = state;
            FirstPlayer_rBtn1brm.Enabled = state;

            FirstPlayer_rBtn3brm.Visible = state;
            FirstPlayer_rBtn3brm.Enabled = state;

            FirstPlayer_rBtn5brm.Visible = state;
            FirstPlayer_rBtn5brm.Enabled = state;

            readyFirstPlayer.Visible = state;
            readyFirstPlayer.Enabled = state;
        }

        private void findIpAdressbtn_Click(object sender, EventArgs e)
        {
            string bilgisayarAdi = Dns.GetHostName();
            rivalPlayerIpAdress.Text = Dns.GetHostByName(bilgisayarAdi).AddressList[0].ToString();
        }
    }
}
