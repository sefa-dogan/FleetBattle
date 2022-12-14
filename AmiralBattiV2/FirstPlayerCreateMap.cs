using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AmiralBattiV2
{
    internal class FirstPlayerCreateMap
    {
        private static Control.ControlCollection AddButton;

        private static EventHandler _fpCoordinate;
        //public EventHandler fpCoordinate { get { return _fpCoordinate; } set { _fpCoordinate = value; } }
        public FirstPlayerCreateMap(Control.ControlCollection addButton, EventHandler fpCoordinate)
        {
            AddButton = addButton;
            _fpCoordinate = fpCoordinate;
            firstPlayerCreateMap();
            UnEnableFirstButtons();
        }
        public List<Button> dinamikFirstButtons = new List<Button>();
        public void firstPlayerCreateMap()
        {
            int y = 0;
            int x = 0;
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    Button firstPlayerbutton = new Button();
                    firstPlayerbutton.Text = "";
                    firstPlayerbutton.Name = $"fPB{i}{j}";
                    firstPlayerbutton.Size = new Size(40, 40);
                    firstPlayerbutton.FlatStyle = FlatStyle.Flat;
                    firstPlayerbutton.BackColor = Color.Transparent;
                    firstPlayerbutton.Click += _fpCoordinate;
                    firstPlayerbutton.Location = new Point(x, y);
                    dinamikFirstButtons.Add(firstPlayerbutton);
                    AddButton.Add(firstPlayerbutton);
                    x += 40;
                }
                y += 40;
                x = 0;
            }
        }
        private delegate void MyDelegate();               // void döndüren ve parametre almayan metodları temsil eden bir temsilci yani delegate oluşturuldu.

        public void EnableFirstButtons()
        {
            foreach (var dButton in dinamikFirstButtons)
            {
                if (dButton.BackgroundImage == null) // arka planı boş olan, bomba resmi olmayan butonların enable özellikleri aktifleştirilir
                {
                    //dButton.Enabled = true;
                    dButton.Invoke(new Action(() => dButton.Enabled = true));
                    //dButton.BeginInvoke(new Action(()=> dButton.Enabled=true));

                }
            }
        }
        public void UnEnableFirstButtons()
        {
            foreach (var dButton in dinamikFirstButtons)
            {
                //dButton.Enabled = false;
                dButton.Invoke(new Action(() => dButton.Enabled = false)); // burada kullanılan Invoke metodu delegate ile çalışacaktır.
                                                                             // Delegate' e temsil edeceği metod ataması yapmadığımız veya böyle bir metod
                                                                             // oluşturmadığımız için, yandaki kullanım ile (MyDelegate)(lambda ile oluşturulacak anonim metodun komutu),
                                                                             // anonim metodun türünü cast kullanımı yaparak, MyDelegate şeklinde
                                                                             // belirtiriz. Türünü belirttikten sonra anonim metodun komutunu yazarız.
                                                                             // Bu şekilde MyDelegate tipine uyan bir anonim metod, Invoke() aracılığıyla çalıştırılır.
            }
        }

        public bool IsLoseFirstP(Image bomba)
        {
            bool isWin = false;
            int count = 0;
            foreach (var dButton in dinamikFirstButtons)
            {
                if (dButton.BackgroundImage == bomba)
                {
                    count++;
                    if (count == 9)
                    {
                        isWin = true;
                    }
                }

            }
            return isWin;
        }
    }
}
