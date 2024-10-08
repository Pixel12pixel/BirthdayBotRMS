using BirthdayBotRMS.Models;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirthdayBotRMS
{
    internal class Commands
    {



        public static async Task HandleSlashCommand(SocketSlashCommand command)
        {
            string buff = "Command: '" + command.Data.Name + "' run by: " + command.User;
            //Program.MessageLog(buff);
            Console.WriteLine($"Command: {command.Data.Name} run by: {command.User}");
            switch (command.Data.Name)
            {
                case "setup":
                    setup(command);
                    break;
                case "add":
                    add(command);
                    break;
                default:
                    break;
            }
        }

        static private void add(SocketSlashCommand command)
        {
            JsonFiles jsonFiles = new JsonFiles();

            if (jsonFiles.ReadConfig == null) command.RespondAsync("Run '/setup' first!", ephemeral: true);
            

            string stringDate = (string)command.Data.Options.FirstOrDefault(option => option.Name == "date").Value;

            string[] formats = { "dd-MM-yyyy" };

            DateTime dateT;

            if (DateTime.TryParseExact(stringDate, formats, null, System.Globalization.DateTimeStyles.None, out dateT))
            {

                Console.WriteLine("test");
                List<User> users = jsonFiles.Read();

                User user = new User();
                user.Client = command.User.Id;
                user.Date = DateOnly.FromDateTime(dateT);

                users.Add(user);
                
                jsonFiles.Write(users);

                command.RespondAsync("Data added", ephemeral: true);

            }
            else
            {
                command.RespondAsync("Wrong format!! Try 'dd-MM-yyyy'", ephemeral: true);
                //command.FollowupAsync("Wrong format!! Try 'dd-MM-yyyy'");
            }

        }

        private static void setup(SocketSlashCommand command)
        {

            JsonFiles jsonFiles = new JsonFiles();

            //var guildUser = (SocketGuildUser)command.Data.Options.First().Value;

            var role = (SocketRole)command.Data.Options.FirstOrDefault(option => option.Name == "role").Value;
            var channel = (SocketGuildChannel)command.Data.Options.FirstOrDefault(option => option.Name == "channel").Value;

            ConfigModel config = new ConfigModel();
            config.Channel = channel.Id;
            config.role = role.Id;

            jsonFiles.SaveConfig(config);

            command.RespondAsync("Config Done!", ephemeral: true);

        }
    }
}
