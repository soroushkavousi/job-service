using JobService.Extensions;
using JobService.Models;
using JobService.Tools;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace JobService.Operators;

public class JobQueueConsumer
{
    private static readonly string _jobQueueName = "jobs";
    private readonly RabbitMQClient _rabbitMQClient;
    private readonly EventingBasicConsumer _consumer;
    private readonly Action<Job> _handleNewJob;

    public JobQueueConsumer(RabbitMQClient rabbitMQClient, Action<Job> handleNewJob)
    {
        _rabbitMQClient = rabbitMQClient;
        _handleNewJob = handleNewJob;
        _consumer = CreateRabbitMQConsumer();
    }

    private EventingBasicConsumer CreateRabbitMQConsumer()
    {
        var consumer = new EventingBasicConsumer(_rabbitMQClient.Channel);
        consumer.Received += HandleRabbitMQMessage;
        _rabbitMQClient.Channel.QueueDeclare(
            queue: _jobQueueName,
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null);
        return consumer;
    }

    public void Start()
    {
        Console.WriteLine("Start consuming jobs...");
        _rabbitMQClient.Channel.BasicConsume(_jobQueueName, false, _consumer);
    }

    private void HandleRabbitMQMessage(object sender, BasicDeliverEventArgs e)
    {
        var body = e.Body.ToArray();
        var json = Encoding.UTF8.GetString(body);
        var job = json.FromJson<Job>();
        Task.Run(() =>
        {
            _handleNewJob(job);
            _rabbitMQClient.Channel.BasicAck(deliveryTag: e.DeliveryTag, multiple: false);
        });
    }
}
