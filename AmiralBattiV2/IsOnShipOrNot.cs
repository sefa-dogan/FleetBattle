using AmiralBattiV2.ModelV3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AmiralBattiV2
{
    internal class IsOnShipOrNot
    {
        int btnX, btnY;

        public bool isOnShipOrNot(Button btn, IPlayer player)
        {
            PlayerInfoV3Entities1 db = new PlayerInfoV3Entities1();

            bool control = false;
            btnX = btn.Location.X;
            btnY = btn.Location.Y;
            if (player is FirstPlayer)
            {
                for (int i = 0; i < 3; i++)
                {
                    var resultFirst = db.FirstPlayers.Find(i + 1);
                    if (btnX == resultFirst.X)
                    {
                        if (resultFirst.Y <= btnY && (btnY + btn.Height) <= (resultFirst.Y + resultFirst.Height))
                        {
                            //MessageBox.Show("Geminize ateş edildi!");
                            control = true;
                        }
                    }
                }
            }
            else if (player is RivalPlayer)
            {
                for (int i = 0; i < 3; i++)
                {
                    var resultRival = db.RivalPlayers.Find(i + 1);
                    if (btnX == resultRival.X)
                    {
                        if (resultRival.Y <= btnY && (btnY + btn.Height) <= (resultRival.Y + resultRival.Height))
                        {
                            //MessageBox.Show("Geminize ateş edildi!");
                            control = true;
                        }
                    }
                }
            }
            return control;
        }
    }
}
