using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using webTest.Serialization;

namespace webTest.Models
{
    public class Bill
    {
        [JsonIgnore] public int Id { get; set; }

        [JsonPropertyName("currency")] public string CurrencyName { get; set; }

        [JsonPropertyName("cash")]
        [JsonConverter(typeof(CashJsonConverter))]
        [Column(TypeName = "decimal(18,4)")]
        public decimal Cash { get; set; }

        [JsonIgnore] public string PurseId { get; set; }
    }
}