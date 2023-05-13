using JobService.Extensions;
using Newtonsoft.Json;

namespace JobService.Models;

public class Configs
{
    public static Configs Instance { get; private set; }

    [JsonProperty("rabbitmq")]
    public RabbitMQConfigs RabbitMQ { get; set; }

    public static void Load()
    {
        var rawConfigs = File.ReadAllText("configs.json");
        Instance = rawConfigs.FromJson<Configs>();
    }

    public record RabbitMQConfigs(string Hostname, string Username, string Password);
}
