
using Newtonsoft.Json;

namespace Model
{
    public class Patient
    {
        [JsonProperty("guid")]
        public Guid Guid { get; set; }

        [JsonProperty("fullname")]
        public string Fullname { get; set; }

        [JsonProperty("gender")]
        public int Gender { get; set; }

        [JsonProperty("birthday")]
        public DateTime Birthday { get; set; }
    }
}
