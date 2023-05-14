using JobService.Models;

namespace JobService.Operators;

public class JobExecuter
{
    public async Task ExecuteAsync(Job job)
    {
        Console.WriteLine($"Job [{job.Id}] started...");
        var durationInMilliseconds = (int)job.Duration * 1000;
        await Task.Delay(durationInMilliseconds);
        Console.WriteLine($"Job [{job.Id}] done.");
    }
}
