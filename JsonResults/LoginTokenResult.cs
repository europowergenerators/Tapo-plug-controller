using Newtonsoft.Json;

namespace TapoPlugController.JsonResults
{
    public class LoginTokenResult
    {
        [JsonProperty("error_code")]
        public int ErrorCode { get; set; }
        [JsonProperty("result")]
        public LoginTokenResultToken Result { get; set; }
    }
    public class LoginTokenResultToken
    {
        [JsonProperty("token")]
        public string Token { get; set; }
    }
}
