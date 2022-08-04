using Newtonsoft.Json;

namespace TapoPlugController.JsonParameters
{
    class LoginParameter
    {
        [JsonProperty("method")]
        public string Method = string.Empty;
        [JsonProperty("params")]
        public LoginParameterCredentials Credentials = null;
        [JsonProperty("requestTimeMils")]
        public long MillisecondsSinceEpoch = 0;

        public LoginParameter(string method, string _Username, string _Password, long _time)
        {
            Method = method;
            Credentials = new LoginParameterCredentials(_Username, _Password);
            MillisecondsSinceEpoch = _time;
        }
    }
    class LoginParameterCredentials
    {
        [JsonProperty("username")]
        public string username = string.Empty;
        [JsonProperty("password")]
        public string password = string.Empty;

        public LoginParameterCredentials(string _Username, string _Password)
        {
            username = _Username;
            password = _Password;
        }
    }
}
