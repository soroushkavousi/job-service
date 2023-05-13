using JobService.Models;
using JobService.Tools;

RabbitMQClient rabbitMQClient = null;
try
{
    Configs.Load();
    rabbitMQClient = new RabbitMQClient(
        Configs.Instance.RabbitMQ.Hostname,
        Configs.Instance.RabbitMQ.Username,
        Configs.Instance.RabbitMQ.Password
    );
    Console.WriteLine("Press any key to exit...");
    Console.ReadKey();
}
catch (Exception ex)
{
    Console.WriteLine(ex.ToString());
}
finally
{
    rabbitMQClient?.Dispose();
}
