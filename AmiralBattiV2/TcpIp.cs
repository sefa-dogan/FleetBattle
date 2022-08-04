using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AmiralBattiV2
{
    internal class TcpIp
    {
        static Socket soket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);   //kurulan bağlandı bu sokette saklanır ve bu soketten işlem yapılır.
        TcpListener listen = new TcpListener(IPAddress.Any, PORT);
        const int PORT = 52000;

        public bool Connect(string ip)                          // IP adresini kullanarak rakip oyuncuya bağlanır ve bağlantı durumunu bool değer olarak döndürür
        {
            soket.Connect(new IPEndPoint(IPAddress.Parse(ip), PORT));
            return soket.Connected;
        }

        public bool WaitConnect()                               // IP adresi ile bağlanılmasını bekler ve bağlantı durumunu bool değer olarak döndürür
        {
            listen.Start();
            soket = listen.AcceptSocket();
            return soket.Connected;
        }

        public void SendData(string data)                       // rakip oyuncuya bilgi gönderir
        {
            soket.Send(Encoding.UTF8.GetBytes(data));
        }

        public string WaitData()                                // rakip oyuncudan gelecek olan bilgiyi bekler ve bilgiyi döndürür
        {
            byte[] data = new byte[256];
            soket.Receive(data);
            string gelenData = Encoding.UTF8.GetString(data).Split('\0')[0];

            return gelenData;
        }

        public int[] _WaitButtonData()                          // rakip oyuncudan gelecek olan buton bilgisini bekler ve butonun koordinatını dizi şeklinde döndürür
        {
            byte[] waitButtonData = new byte[256];
            soket.Receive(waitButtonData);
            string _X = (Encoding.UTF8.GetString(waitButtonData)).Split(',')[0];
            string _Y = (Encoding.UTF8.GetString(waitButtonData)).Split(',')[1];
            int X = Convert.ToInt16(_X);
            int Y = Convert.ToInt16(_Y);

            return new int[] { X, Y };
        }
    }
}
