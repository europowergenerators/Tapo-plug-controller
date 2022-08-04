using Newtonsoft.Json;

namespace TapoPlugController.JsonResults
{
    public class EncryptedResult
    {
        [JsonProperty("error_code")]
        public int ErrorCode { get; set; }
        [JsonProperty("result")]
        public EncryptedResultResponse Result { get; set; }
    }
    public class EncryptedResultResponse
    {
        [JsonProperty("response")]
        public string Response { get; set; }
    }
}
