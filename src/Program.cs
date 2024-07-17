using Newtonsoft.Json;
using Bot;

string json = File.ReadAllText("config.json");

Config config = JsonConvert.DeserializeObject<Config>(json);

AIBot Bot = new(config.TelegramAPI);

Bot.StartPooling();

Thread.Sleep(-1);


public class Config
{
    public string TelegramAPI {get;set;} = "your api";
    public string OpenAIServer {get;set;} = "openai.com";

    public string GPTToken{get;set;} = "sk-";
}
