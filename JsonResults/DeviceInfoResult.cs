using Newtonsoft.Json;

namespace TapoPlugController.JsonResults
{
    public class DeviceInfoResult
    {
        [JsonProperty("result")]
        public Info DeviceInfo { get; set; }
        [JsonProperty("error_code")]
        public int ErrorCode { get; set; }
    }
    public class DefaultStates
    {
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("state")]
        public State State { get; set; }
    }
    public class Info
    {
        [JsonProperty("device_id")]
        public string DeviceId { get; set; }
        [JsonProperty("fw_ver")]
        public string FwVer { get; set; }
        [JsonProperty("hw_ver")]
        public string HwVer { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("model")]
        public string Model { get; set; }
        [JsonProperty("mac")]
        public string Mac { get; set; }
        [JsonProperty("hw_id")]
        public string HwId { get; set; }
        [JsonProperty("fw_id")]
        public string FwId { get; set; }
        [JsonProperty("oem_id")]
        public string OemId { get; set; }
        [JsonProperty("overheated")]
        public bool Overheated { get; set; }
        [JsonProperty("ip")]
        public string Ip { get; set; }
        [JsonProperty("time_diff")]
        public int TimeDiff { get; set; }
        [JsonProperty("ssid")]
        public string Ssid { get; set; }
        [JsonProperty("rssi")]
        public int Rssi { get; set; }
        [JsonProperty("signal_level")]
        public int SignalLevel { get; set; }
        [JsonProperty("latitude")]
        public int Latitude { get; set; }
        [JsonProperty("longitude")]
        public int Longitude { get; set; }
        [JsonProperty("lang")]
        public string Lang { get; set; }
        [JsonProperty("avatar")]
        public string Avatar { get; set; }
        [JsonProperty("region")]
        public string Region { get; set; }
        [JsonProperty("specs")]
        public string Specs { get; set; }
        [JsonProperty("nickname")]
        public string Nickname { get; set; }
        [JsonProperty("has_set_location_info")]
        public bool HasSetLocationInfo { get; set; }
        [JsonProperty("device_on")]
        public bool DeviceOn { get; set; }
        [JsonProperty("on_time")]
        public int OnTime { get; set; }
        [JsonProperty("default_states")]
        public DefaultStates DefaultStates { get; set; }
    }
    public class State
    {

    }
}
