using Newtonsoft.Json;

namespace TapoPlugController.JsonParameters
{
    class ToggleDeviceState
    {
        [JsonProperty("method")]
        public string Method = string.Empty;
        [JsonProperty("params")]
        public ToggleDeviceStateParameter DeviceState = null;
        [JsonProperty("requestTimeMils")]
        public long MillisecondsSinceEpoch = 0;
        [JsonProperty("terminalUUID")]
        public string TerminalUUID = string.Empty;

        public ToggleDeviceState(string _Method, bool _DeviceOn, string _TerminalUUID, long _Time)
        {
            Method = _Method;
            DeviceState = new ToggleDeviceStateParameter(_DeviceOn);
            MillisecondsSinceEpoch = _Time;
            TerminalUUID = _TerminalUUID;
        }
    }
    class ToggleDeviceStateParameter
    {
        [JsonProperty("device_on")]
        public bool DeviceOn = false;

        public ToggleDeviceStateParameter(bool _DeviceOn)
        {
            DeviceOn = _DeviceOn;
        }
    }
}
