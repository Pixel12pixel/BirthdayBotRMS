using BirthdayBotRMS.Models;
using Discord;
using System.Text.Json;
using System.IO;

namespace BirthdayBotRMS
{
    class JsonFiles
    {
        private const string pathConfig = "config.json";
        private const string path = "list.json";

        public void Write(List<User> users)
        {

            string jsonString = JsonSerializer.Serialize(users);
            //string jsonString = JsonConvert.SerializeObject(users, settings);
            Console.WriteLine(jsonString);

            File.WriteAllText(path, jsonString);
        }

        public List<User> Read()
        {

            if (!File.Exists(path)) return new List<User>();

            string jsonString = File.ReadAllText(path);

            if (jsonString == "") return new List<User>();

            List<User> users = JsonSerializer.Deserialize<List<User>>(jsonString);
            //List<User> users = JsonConvert.DeserializeObject<List<User>>(jsonString);
            return users;
        }

        public ConfigModel ReadConfig()
        {

            if (!File.Exists(pathConfig)) return new ConfigModel();

            string jsonStrng = File.ReadAllText(pathConfig);

            ConfigModel configModel = JsonSerializer.Deserialize<ConfigModel>(jsonStrng);
            //ConfigModel configModel = JsonConvert.DeserializeObject<ConfigModel>(jsonStrng);

            return configModel;
        }

        public void SaveConfig(ConfigModel config)
        {

            string jsonString = JsonSerializer.Serialize(config);
            //string jsonString = JsonConvert.SerializeObject(config);
            File.WriteAllText(pathConfig, jsonString);

        }
    }
}
