using Newtonsoft.Json;
using RestSharp;

namespace TapoPlugController.TapoDevices
{
    public class P110 : P100
    {
        public P110(string IPAddress, string Username, string Password) : base(IPAddress, Username, Password) { }

        public JsonResults.EnergyConsumptionResult GetEnergyInfo()
        {
            RestClient Client = new RestClient($"http://{_IPAddress}");
            RestRequest Request = new RestRequest($"app?token={_Token}", Method.Post);
            Request.RequestFormat = DataFormat.Json;
            Request.AddHeader("Cookie", _Cookie);
            Request.AddHeader("Connection", "Keep-Alive");
            string Payload = JsonConvert.SerializeObject(new JsonParameters.GetDeviceInfoParameter("get_energy_usage", GetTimeStamp()));
            string EncryptedPayload = _Cipher.Encrypt(Payload);
            Request.AddJsonBody(new { method = "securePassthrough", @params = new { request = EncryptedPayload } });
            RestResponse<string> response = Client.Execute<string>(Request);
            JsonResults.EncryptedResult _ResultJSON = JsonConvert.DeserializeObject<JsonResults.EncryptedResult>(response.Content);
            var _Response = _Cipher.Decrypt(_ResultJSON.Result.Response);
            JsonResults.EnergyConsumptionResult _EnergyConsumption = JsonConvert.DeserializeObject<JsonResults.EnergyConsumptionResult>(_Cipher.Decrypt(_ResultJSON.Result.Response));
            return _EnergyConsumption;
        }
    }
}
