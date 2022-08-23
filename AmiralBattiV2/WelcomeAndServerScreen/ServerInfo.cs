using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmiralBattiV2.WelcomeAndServerScreen
{
    internal class ServerInfo
    {
        public string Name { get; set; }
        public string IP { get; set; }

        public DateTime LastUpdate { get; set; } = DateTime.Now;
    }
}
