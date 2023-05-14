using JobService.Models;
using JobService.Tools;

namespace JobService.Operators;

public class JobOperator
{
    private readonly RabbitMQClient _rabbitMQClient;

    public JobQueueConsumer JobQueueConsumer { get; set; }
    private JobExecuter JobExecuter { get; set; }

    public JobOperator(RabbitMQClient rabbitMQClient)
    {
        _rabbitMQClient = rabbitMQClient;
    }

    public void ExecuteJobsInQueue()
    {
        JobExecuter = new JobExecuter();
        JobQueueConsumer = new JobQueueConsumer(_rabbitMQClient, HandleNewJob);
        JobQueueConsumer.Start();
    }

    public void HandleNewJob(Job job)
    {
        JobExecuter.ExecuteAsync(job).GetAwaiter().GetResult();
    }
}
