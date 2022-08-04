using Newtonsoft.Json;
using System.Collections.Generic;

namespace TapoPlugController.JsonResults
{
    public class EnergyConsumptionResult
    {
        [JsonProperty("result")]
        public EnergyConsumption ConsumptionInfo { get; set; }
        [JsonProperty("error_code")]
        public int ErrorCode { get; set; }
    }
    public class EnergyConsumption
    {
        [JsonProperty("today_runtime")]
        public int TodayRuntime { get; set; }
        [JsonProperty("month_runtime")]
        public int MonthRuntime { get; set; }
        [JsonProperty("today_energy")]
        public int TodayEnergy { get; set; }
        [JsonProperty("month_energy")]
        public int MonthEnergy { get; set; }
        [JsonProperty("local_time")]
        public string LocalTime { get; set; }
        [JsonProperty("past24h")]
        public List<int> Past24h { get; set; }
        [JsonProperty("past30d")]
        public List<int> Past30d { get; set; }
        [JsonProperty("past1y")]
        public List<int> Past1y { get; set; }
        [JsonProperty("past7d")]
        public List<List<int>> Past7d { get; set; }
        [JsonProperty("current_power")]
        public int CurrentPower { get; set; }
    }
}
