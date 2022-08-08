using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AmiralBattiV2
{
    internal class RivalPlayerCreateMap
    {
        private Control.ControlCollection AddButton;

        private static EventHandler _rpCoordinate;
        public RivalPlayerCreateMap(Control.ControlCollection addButton, EventHandler rpCoordinate)
        {
            AddButton = addButton;
            _rpCoordinate = rpCoordinate;
            rivalPlayerCreateMap();
            UnEnableRivalButtons();
        }
        public List<Button> dinamikRivalButtons = new List<Button>();
        public void rivalPlayerCreateMap()
        {
            int y = 0;
            int x = 800;
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    Button rivalPlayerbutton = new Button();
                    rivalPlayerbutton.Text = "";
                    rivalPlayerbutton.Name = $"rPB{i}{j}";
                    rivalPlayerbutton.Size = new Size(40, 40);
                    rivalPlayerbutton.FlatStyle = FlatStyle.Flat;
                    rivalPlayerbutton.BackColor = Color.FromArgb(0, 150, 255);
                    rivalPlayerbutton.Click += _rpCoordinate;
                    rivalPlayerbutton.Location = new Point(x, y);
                    dinamikRivalButtons.Add(rivalPlayerbutton);
                    AddButton.Add(rivalPlayerbutton);
                    x += 40;
                }
                y += 40;
                x = 800;
            }
        }

        public void EnableRivalButtons()
        {
            foreach (var dButton in dinamikRivalButtons)
            {
                if (dButton.BackgroundImage==null)
                {
                    //dButton.Enabled = true;
                    dButton.Invoke((MethodInvoker)(() => dButton.Enabled = true));

                }
            }
        }

        public void UnEnableRivalButtons()
        {
            foreach (var dButton in dinamikRivalButtons)
            {
                //dButton.Enabled = false;
                dButton.Invoke((MethodInvoker)(() => dButton.Enabled = false));

            }
        }

        public bool IsLoseRivalP(Image bomba)
        {
            bool isWin = false;
            int count = 0;
            foreach (var dButton in dinamikRivalButtons)
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
