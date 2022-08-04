using Newtonsoft.Json;

namespace TapoPlugController.JsonParameters
{
    class GetDeviceInfoParameter
    {
        [JsonProperty("method")]
        public string Method = string.Empty;
        [JsonProperty("requestTimeMils")]
        public long MillisecondsSinceEpoch = 0;

        public GetDeviceInfoParameter(string method, long _time)
        {
            Method = method;
            MillisecondsSinceEpoch = _time;
        }
    }
}
