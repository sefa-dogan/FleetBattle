using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using AmiralBattiV2.ModelV3;
using Microsoft.Win32;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Net;
using System.Media;

namespace AmiralBattiV2
{
    public partial class Form1 : Form
    {
        public Form1(string whoAmI, string ip)
        {
            InitializeComponent();
            _whoAmI = whoAmI;
            if (whoAmI == "firstplayer")
            {
                RivalPlayerSecenekBtn.Visible = false;
                _ipAdress = ip;

                ship_brm1 = FirstPlayer_pBx1brm.Location;
                ship_brm3 = FirstPlayer_pBx3brm.Location;
                ship_brm5 = FirstPlayer_pBx5brm.Location;
            }
            else if (whoAmI == "rivalplayer")
            {
                ship_brm1 = RivalPlayer_pBx1brm.Location;
                ship_brm3 = RivalPlayer_pBx3brm.Location;
                ship_brm5 = RivalPlayer_pBx5brm.Location;
            }

        }

        Game game;
        string _whoAmI;
        string _ipAdress;
        Point ship_brm1;
        Point ship_brm3;
        Point ship_brm5;
        private void Form1_Load(object sender, EventArgs e)
        {
            game = new Game(Controls, bomba, carpi, firstPlayerSiraLbl, rivalPlayerSiraLbl, RivalPlayerSecenekBtn);
            if (_whoAmI == "firstplayer")
            {
                ConnectToRivalPlayer();
            }
        }

        private void ConnectToRivalPlayer()
        {
            game.AreYouFP(true);
            game.IpAdress = _ipAdress;
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
            if (!(ship_brm1 == FirstPlayer_pBx1brm.Location || ship_brm3 == FirstPlayer_pBx3brm.Location || ship_brm5 == FirstPlayer_pBx5brm.Location))
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
            else
            {
                MessageBox.Show("Bütün gemileri haritaya yerleştirin!");
            }

        }


        private void readyRivalPlayer_Click(object sender, EventArgs e)
        {
            if (!(ship_brm1 == RivalPlayer_pBx1brm.Location || ship_brm3 == RivalPlayer_pBx3brm.Location || ship_brm5 == RivalPlayer_pBx5brm.Location))
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

                game.rivalPlayerPicture = null;
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

                game.RivalPlayerReady = "hazir";
                game.isGameStarted();
            }
            else
            {
                MessageBox.Show("Bütün gemileri haritaya yerleştirin!");
            }
            
        }

        private void RivalPlayerSecenekBtn_Click(object sender, EventArgs e)
        {

            RivalPlayerSecenekBtn.Enabled = false;
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
    }
}
