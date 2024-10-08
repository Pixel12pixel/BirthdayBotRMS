using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirthdayBotRMS.Models
{
    class ConfigModel
    {

        public ulong Channel { get; set; }
        public ulong role { get; set; }

    }
}
