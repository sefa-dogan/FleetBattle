using AmiralBattiV2.WelcomeAndServerScreen;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Media;

namespace AmiralBattiV2
{
    public partial class HomeScreen : Form
    {
        public HomeScreen()
        {
            InitializeComponent();
        }
        public int ServerPort { get; set; } = 39000;
        byte[] data = new byte[] { 0x00 };
        BindingList<ServerInfo> ServerList { get; } = new BindingList<ServerInfo>() { };
        string player, ip;
        SoundPlayer lobbySound;

        private void HomeScreen_Load(object sender, EventArgs e)
        {
            tabControl1.Size = new Size(this.Width, this.Height);
            dgwRoomList.DataSource = ServerList;
            lobbySound = new SoundPlayer();
            lobbySound.SoundLocation= @"C:\Users\sefad\Desktop\AmiralBattiV2_Copy\AmiralBattiV2\wolfteam_roomsong.wav";
            lobbySound.Play();
        }

        private void HomeScreen_SizeChanged(object sender, EventArgs e)
        {
            tabControl1.Size = new Size(this.Width, this.Height);
        }
        private void ClientWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            using (UdpClient clientUdpClient = new UdpClient())
            {
                clientUdpClient.EnableBroadcast = true;
                clientUdpClient.Client.ReceiveTimeout = 1500;
                IPEndPoint targetEndPoint = new IPEndPoint(IPAddress.Broadcast, ServerPort);

                BackgroundWorker backgroundWorker = sender as BackgroundWorker;

                var watch = new Stopwatch(); // broadcast yayın yapıldıktan sonra cevap beklenen max süre tanımlamak için oluşturuldu. KRONOMETRE
                do
                {
                    watch.Restart();
                    clientUdpClient.Send(data, data.Length, targetEndPoint);

                    do
                    {
                        IPEndPoint receiveEndPoint = new IPEndPoint(IPAddress.None, 0); //dummy

                        try
                        {
                            byte[] result = clientUdpClient.Receive(ref receiveEndPoint);
                            string serverName = Encoding.UTF8.GetString(result);
                            ServerInfo server = new ServerInfo { Name = serverName, IP = receiveEndPoint.Address.ToString() };
                            backgroundWorker.ReportProgress(0, server); //clientprogresschanged tetiklenir
                        }
                        catch (Exception)
                        {

                        }
                    }
                    while (!backgroundWorker.CancellationPending && watch.ElapsedMilliseconds < 1500);  // eğer kronometre 1500 ms den daha küçükse ve backgroundworker iptal edilmek istenmemişse devam et 
                } while (!backgroundWorker.CancellationPending);    // CancelAsync() metodu ile, ilgili backgroundworkera iptal isteği gelmemiş ise döngü devam eder

            }
        }
        Form1 f1;

        private void ServerWorker_DoWork(object sender, DoWorkEventArgs e)
        {

            using (UdpClient serverUdpClient = new UdpClient(ServerPort))
            {
                IPEndPoint remoteIPEndPoint = new IPEndPoint(IPAddress.None, 0); //dummy

                BackgroundWorker backgroundWorker = sender as BackgroundWorker;
                do       //buradaki do while döngüsü, oyuncunun olşturduğu oda bilgisini gönderir ve döngü devam ettiği taktirde eğer oda ismini değiştirirsek, odaları listele butonu aktif olan her oyuncu dinamik bir şekilde bu bilgi değişikliğini oda listesinde görecektir.                 
                {
                    var data = serverUdpClient.Receive(ref remoteIPEndPoint);
                    byte[] serverName = Encoding.UTF8.GetBytes(tbxRoomName.Text);
                    serverUdpClient.Send(serverName, serverName.Length, remoteIPEndPoint);

                } while (!backgroundWorker.CancellationPending);
            }
        }

        private void ClientWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            var server = e.UserState as ServerInfo;       // backgroundWorker.ReportProgress(0, server) satırı ile reportprogress kullanrak gönderilen server bilgisi e.UserState ile alınır. UserState server bilgisine karşılık gelir

            var currentServer = ServerList.ToList().FirstOrDefault(x => x.IP == server.IP);
            if (currentServer is null)
            {
                ServerList.Add(server);
            }
            else
            {
                currentServer.LastUpdate = DateTime.Now;
                currentServer.Name = server.Name;
                dgwRoomList.Refresh();
            }
        }

        private void RoomsListBtn_Click(object sender, EventArgs e)
        {
            if (ClientWorker.IsBusy)
            {
                ClientWorker.CancelAsync();     // çalışan backgroundworker ı iptal etme isteği gönderen metod
            }
            else
            {
                ClientWorker.RunWorkerAsync();
                Button button = (sender as Button);
                button.BackColor = Color.LightCoral;
                button.Text = "Listelemeyi durdur";

            }
        }
        private void CreateRoomBtn_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            if (ServerWorker.IsBusy)
            {
                ServerWorker.CancelAsync();
                f1.Invoke(new Action(()=> f1.Dispose()));
                button.Text = "Oda oluştur";
                button.BackColor = Color.Transparent;

            }
            else
            {
                ServerWorker.RunWorkerAsync(); // backgroundworker a ait olan RunWorkerAsync() metodu ile ilgi backgroundworker çalıştırılır.
                button.BackColor = Color.LightCoral;
                button.Text = "İptal server";

                player = "rivalplayer";
                GameScreenWorker.RunWorkerAsync();
            }
        }

        private void ServerWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            CreateRoomBtn.BackColor = SystemColors.Control;
            CreateRoomBtn.Text = "Oda oluştur";
            MessageBox.Show("Yeni bir oda oluşturmak için müsaitsiniz!");
        }

        private void ClientWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            RoomsListBtn.BackColor = SystemColors.Control;
            RoomsListBtn.Text = "Odaları Listele";
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblHour.Text = DateTime.Now.ToString();
            foreach (var oldServer in ServerList.ToList().Where(x => x.LastUpdate + TimeSpan.FromSeconds(5) < DateTime.Now))
            {
                ServerList.Remove(oldServer);
            }
        }

        private void dgwRoomList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            player = "firstplayer";

            if (dgwRoomList.CurrentRow.Cells[1].Value != null)
            {
                ip = dgwRoomList.CurrentRow.Cells[1].Value.ToString();
                GameScreenWorker.RunWorkerAsync();
            }
            else
            {
                MessageBox.Show("Lütfen bir oda seçiniz");
            }

        }
        SoundPlayer gameScreenSound;

        private void GameScreenWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            lobbySound.Stop();
            gameScreenSound = new SoundPlayer();
            gameScreenSound.SoundLocation = @"C:\Users\sefad\Desktop\AmiralBattiV2_Copy\AmiralBattiV2\gamesound.wav";
            gameScreenSound.Play();
            f1 = new Form1(player, ip);
            f1.ShowDialog();

            while (f1.IsDisposed)
            {
                gameScreenSound.Stop();
                break;
            }
        }

        private void GameScreenWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
        }
    }
}
