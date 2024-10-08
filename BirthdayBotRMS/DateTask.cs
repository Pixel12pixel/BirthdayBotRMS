using BirthdayBotRMS.Models;
using Discord;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirthdayBotRMS
{
    class DateTask
    {

        private ulong channelId = 0;
        private ulong roleId = 0;


        public async Task TestDate(DiscordSocketClient _client)
        {
            await Task.Delay(5000);

            JsonFiles jsonFiles = new JsonFiles();

            ConfigModel model = new ConfigModel();

            while (true)
            {
                //await channel.SendMessageAsync($"<@{a}> have bd!");



                model = jsonFiles.ReadConfig();

                channelId = model.Channel;
                roleId = model.role;

                var channel = _client.GetChannel(channelId) as IMessageChannel;
                
                if (channel != null)
                {
                    List<User> users = jsonFiles.Read();

                    for (int i = 0; i < users.Count; i++)
                    {

                        if (users[i].Date.Month == DateOnly.FromDateTime(DateTime.Now).Month && users[i].Date.Day == DateOnly.FromDateTime(DateTime.Now).Day)
                        {
                            await channel.SendMessageAsync($"<@{users[i].Client}> **have** birthday!!");

                        }

                    }

                    
                
                }
                await Task.Delay(43200000);
                //await Task.Delay(1000);

            }
        
        }

    }
}
