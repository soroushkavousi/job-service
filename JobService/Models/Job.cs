namespace JobService.Models;

public class Job
{
    public ulong Id { get; set; }
    public float Duration { get; set; }

    public Job(ulong id, float duration)
    {
        Id = id;
        Duration = duration;
    }
}
