using Newtonsoft.Json;

namespace TapoPlugController.JsonResults
{
    public class HandshakeResult
    {
        [JsonProperty("error_code")]
        public int ErrorCode { get; set; }
        [JsonProperty("result")]
        public HandshakeResultKey Result { get; set; }
    }
    public class HandshakeResultKey
    {
        [JsonProperty("key")]
        public string Key { get; set; }
    }
}
