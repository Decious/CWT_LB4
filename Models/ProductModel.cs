using System.ComponentModel;
using System.Text.Json.Serialization;

namespace Kaberdin_LB4.Models
{
    public class ProductModel
    {
        [DisplayName("Идентификатор")]
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [DisplayName("Название")]
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [DisplayName("Цена")]
        [JsonPropertyName("price")]
        public string Price { get; set; }

        [DisplayName("Описание")]
        [JsonPropertyName("description")]
        public string Description { get; set; }
    }
}
