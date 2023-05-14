using RabbitMQ.Client;

namespace JobService.Tools;

public class RabbitMQClient : IDisposable
{
    public ConnectionFactory Factory { get; set; }
    public IConnection Connection { get; set; }
    public IModel Channel { get; set; }

    public RabbitMQClient(string hostname, string username, string password, int prefetchCount)
    {
        Factory = new ConnectionFactory { HostName = hostname, UserName = username, Password = password };
        Connection = Factory.CreateConnection();
        Console.WriteLine($"Connected to RabbitMQ '{hostname}'");
        Channel = Connection.CreateModel();
        Channel.BasicQos(prefetchSize: 0, prefetchCount: (ushort)prefetchCount, global: false);
    }

    public void Dispose()
    {
        Channel.Close();
        Connection.Close();
        GC.SuppressFinalize(this);
    }
}
