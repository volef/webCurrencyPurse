using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace webTest.Models
{
    public class Purse
    {
        [JsonPropertyName("user_uid")] public string Id { get; set; }

        [JsonPropertyName("bills")] public List<Bill> Bills { get; set; } = new List<Bill>();
    }
}