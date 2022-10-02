namespace Domain.Exceptions;

public class ValidationExceptionDetails
{
    [JsonPropertyName("status")]
    public int? Status { get; set; }

    [JsonPropertyName("detail")]
    public string? Detail { get; set; }

    [JsonPropertyName("message")]
    public string? Message { get; set; }

    [JsonPropertyName("instance")]
    public string? Instance { get; set; }

    public ICollection<int>? ErrorCodes { get; set; }
    public IDictionary<string, string[]>? Errors { get; }

    public ValidationExceptionDetails()
    {
        ErrorCodes = new List<int>();
        Errors = new Dictionary<string, string[]>();
    }
}

