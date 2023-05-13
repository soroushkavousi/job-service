using Newtonsoft.Json;

namespace JobService.Extensions;

public static class StringExtensions
{
    public static T FromJson<T>(this string json)
    {
        if (json == null)
            return default;

        return JsonConvert.DeserializeObject<T>(json);
    }
}
