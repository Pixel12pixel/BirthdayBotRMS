using Discord;
using Discord.WebSocket;

namespace BirthdayBotRMS
{

    internal class Program
    {

        //Xenu is not authorized to change the grammar of comments!

        private DiscordSocketClient _client;

        static void Main(string[] args) => new Program().MainAsync().GetAwaiter().GetResult();

        public async Task MainAsync()
        {
            _client = new DiscordSocketClient(new DiscordSocketConfig
            {
                GatewayIntents = GatewayIntents.Guilds | GatewayIntents.GuildMessages | GatewayIntents.MessageContent
            });

            _client.Log += Log;
            //_client.MessageReceived += MessageReceived;
            _client.Ready += ReadyAsync;
            _client.SlashCommandExecuted += Commands.HandleSlashCommand;

            var token = "BOT TOKEN"; // bot token
            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();

            DateTask dateTask = new DateTask();

            Task.Run(() => dateTask.TestDate(_client));

            await Task.Delay(-1);
        }



        private async Task ReadyAsync()
        {
            Console.WriteLine("Bot is connected!");

            
            var guild = _client.GetGuild(00000000); //guild id
            
            var command = new SlashCommandBuilder()
                .WithName("add")
                .WithDescription("Add yourself to database")
                .AddOption("date", ApplicationCommandOptionType.String, "Date (dd-MM-yyyy)", isRequired: true)
                .Build();

            await guild.CreateApplicationCommandAsync(command);

            

            command = new SlashCommandBuilder()
                .WithName("setup")
                .WithDescription("Setup a bot")
                .AddOption("channel", ApplicationCommandOptionType.Channel, "Bot channel", isRequired: true)
                .AddOption("role", ApplicationCommandOptionType.Role, "Bot Birthday role", isRequired: true)
                .Build();

            await guild.CreateApplicationCommandAsync(command);
        }
        private Task Log(LogMessage msg)
        {
            string buff = "Discord System Log: " + msg.ToString();
            MessageLog(buff);
            //Console.WriteLine($"[{TimeOnly.FromDateTime(DateTime.UtcNow)}] Discord System Log: {msg.ToString()}");
            return Task.CompletedTask;
        }

        public static void MessageLog(string text)
        {

            Console.WriteLine($"[{TimeOnly.FromDateTime(DateTime.Now)}] {text}");

        }
    }
}


