using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirthdayBotRMS.Models
{
    internal class User
    {

        public ulong Client { get; set; }

        public DateOnly Date { get; set; }

    }
}
