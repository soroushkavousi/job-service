using JobService.Tools;

RabbitMQClient rabbitMQClient = null;
try
{
    rabbitMQClient = new RabbitMQClient(
        EnvironmentVariable.RabbitMQHostname.Value,
        EnvironmentVariable.RabbitMQUsername.Value,
        EnvironmentVariable.RabbitMQPassword.Value,
        int.Parse(EnvironmentVariable.RabbitMQPrefetchCount.Value)
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
