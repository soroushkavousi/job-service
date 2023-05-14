namespace JobService.Tools;

public class EnvironmentVariable
{
    public string Key { get; set; }
    public string Value { get; set; }
    public string Default { get; set; }
    public bool IsProvided { get; set; }

    public EnvironmentVariable(string key, string @default)
    {
        Key = key;
        Default = @default;

        IsProvided = TryReadValueFromSystem();
        if (IsProvided)
            return;

        Value = Default;
    }

    public static EnvironmentVariable RabbitMQHostname { get; } = new
    (
        key: "JOB_SERVICE_RABBITMQ_HOSTNAME",
        @default: "localhost"
    );
    public static EnvironmentVariable RabbitMQUsername { get; } = new
    (
        key: "JOB_SERVICE_RABBITMQ_USERNAME",
        @default: "guest"
    );
    public static EnvironmentVariable RabbitMQPassword { get; } = new
    (
        key: "JOB_SERVICE_RABBITMQ_PASSWORD",
        @default: "guest"
    );

    private bool TryReadValueFromSystem()
    {
        Value = Environment.GetEnvironmentVariable(Key, EnvironmentVariableTarget.Process);
        if (!string.IsNullOrWhiteSpace(Value))
            return true;

        Value = Environment.GetEnvironmentVariable(Key, EnvironmentVariableTarget.Machine);
        if (!string.IsNullOrWhiteSpace(Value))
            return true;

        Value = Environment.GetEnvironmentVariable(Key, EnvironmentVariableTarget.User);
        if (!string.IsNullOrWhiteSpace(Value))
            return true;

        return false;
    }
}
