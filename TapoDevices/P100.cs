using RestSharp;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;
using System.Linq;

namespace TapoPlugController.TapoDevices
{
    public class P100
    {
        protected string _IPAddress = string.Empty;
        protected string _Guid = Guid.NewGuid().ToString();
        protected string _EncodedUsername = string.Empty;
        protected string _EncodedPassword = string.Empty;
        protected string _Cookie = string.Empty;
        protected string _Token = string.Empty;
        protected string _PublicPEMKey = string.Empty;
        protected Cipher.TPLinkCipher _Cipher = null;
        protected RSA _KeyPair = null;

        public P100(string IPAddress, string Username, string Password)
        {
            _IPAddress = IPAddress;

            EncryptCredentials(Username, Password);
            CreateKeyPair();
        }

        protected void EncryptCredentials(string Username, string Password)
        {
            _EncodedPassword = Cipher.TPLinkCipher.MimeEncoder(Encoding.UTF8.GetBytes(Password));
            _EncodedUsername = SHA_Digest_Username(Username).ToLower();
            _EncodedUsername = Cipher.TPLinkCipher.MimeEncoder(Encoding.UTF8.GetBytes(_EncodedUsername));
        }

        protected string SHA_Digest_Username(string Username)
        {
            using (var SHA1 = new SHA1Managed())
            {
                byte[] DigestedArray = SHA1.ComputeHash(Encoding.UTF8.GetBytes(Username));
                List<byte> _Processed = new List<byte>();
                for (int i = 0; i < DigestedArray.Length; i++)
                {
                    _Processed.Add((byte)(DigestedArray[i] & 255));
                }
                return BitConverter.ToString(_Processed.ToArray()).Replace("-", string.Empty);
            }
        }

        protected void CreateKeyPair()
        {
            _KeyPair = RSA.Create(1024);
            _PublicPEMKey = ExportPEMKey(_KeyPair, true);
        }
        protected string ExportPEMKey(RSA Keys, bool Public)
        {
            string Type = Public ? "PUBLIC" : "PRIVATE";
            string Header = $"-----BEGIN {Type} KEY-----";
            string Footer = $"-----END {Type} KEY-----";
            string PEMKey = Convert.ToBase64String(Public ? Keys.ExportSubjectPublicKeyInfo() : Keys.ExportRSAPrivateKey());
            return $"{Header}\n{PEMKey}\n{Footer}";
        }

        protected Cipher.TPLinkCipher DecodeHandshakeKey(string Key)
        {
            byte[] decode = Convert.FromBase64String(Key);
            var DecryptedData = _KeyPair.Decrypt(decode, RSAEncryptionPadding.Pkcs1);
            List<byte> ByteList1 = new List<byte>();
            List<byte> ByteList2 = new List<byte>();
            for (int i = 0; i < 16; i++)
            {
                ByteList1.Add(DecryptedData[i]);
                ByteList2.Add(DecryptedData[i + 16]);
            }
            return new Cipher.TPLinkCipher(ByteList1.ToArray(), ByteList2.ToArray());
        }

        public void Handshake()
        {
            RestClient Client = new RestClient($"http://{_IPAddress}");
            RestRequest Request = new RestRequest("app", Method.Post);
            Request.RequestFormat = DataFormat.Json;
            Request.AddHeader("Connection", "Keep-Alive");
            Request.AddJsonBody(new { method = "handshake", @params = new { key = _PublicPEMKey, requestTimeMils = GetTimeStamp() } });
            RestResponse<string> response = Client.Execute<string>(Request);
            JsonResults.HandshakeResult _ResultJSON = JsonConvert.DeserializeObject<JsonResults.HandshakeResult>(response.Content);
            _Cipher = DecodeHandshakeKey(_ResultJSON.Result.Key);
            _Cookie = response.Headers.FirstOrDefault(X => X.Name == "Set-Cookie").Value.ToString().Split(";")[0];
        }

        public void Login()
        {
            RestClient Client = new RestClient($"http://{_IPAddress}");
            RestRequest Request = new RestRequest("app", Method.Post);
            Request.RequestFormat = DataFormat.Json;
            Request.AddHeader("Cookie", _Cookie);
            Request.AddHeader("Connection", "Keep-Alive");
            string Payload = JsonConvert.SerializeObject(new JsonParameters.LoginParameter("login_device", _EncodedUsername, _EncodedPassword, GetTimeStamp()));
            string EncryptedPayload = _Cipher.Encrypt(Payload);
            Request.AddJsonBody(new { method = "securePassthrough", @params = new { request = EncryptedPayload } });
            RestResponse<string> response = Client.Execute<string>(Request);
            JsonResults.EncryptedResult _ResultJSON = JsonConvert.DeserializeObject<JsonResults.EncryptedResult>(response.Content);
            JsonResults.LoginTokenResult _TokenJSON = JsonConvert.DeserializeObject<JsonResults.LoginTokenResult>(_Cipher.Decrypt(_ResultJSON.Result.Response));
            _Token = _TokenJSON.Result.Token;
        }

        public void SetDeviceStatus(bool _DeviceStatusOn)
        {
            RestClient Client = new RestClient($"http://{_IPAddress}");
            RestRequest Request = new RestRequest($"app?token={_Token}", Method.Post);
            Request.RequestFormat = DataFormat.Json;
            Request.AddHeader("Cookie", _Cookie);
            Request.AddHeader("Connection", "Keep-Alive");
            string Payload = JsonConvert.SerializeObject(new JsonParameters.ToggleDeviceState("set_device_info", _DeviceStatusOn, _Guid, GetTimeStamp()));
            string EncryptedPayload = _Cipher.Encrypt(Payload);
            Request.AddJsonBody(new { method = "securePassthrough", @params = new { request = EncryptedPayload } });
            RestResponse<string> response = Client.Execute<string>(Request);
            JsonResults.EncryptedResult _ResultJSON = JsonConvert.DeserializeObject<JsonResults.EncryptedResult>(response.Content);
        }

        public JsonResults.DeviceInfoResult GetDeviceInfo()
        {
            RestClient Client = new RestClient($"http://{_IPAddress}");
            RestRequest Request = new RestRequest($"app?token={_Token}", Method.Post);
            Request.RequestFormat = DataFormat.Json;
            Request.AddHeader("Cookie", _Cookie);
            Request.AddHeader("Connection", "Keep-Alive");
            string Payload = JsonConvert.SerializeObject(new JsonParameters.GetDeviceInfoParameter("get_device_info", GetTimeStamp()));
            string EncryptedPayload = _Cipher.Encrypt(Payload);
            Request.AddJsonBody(new { method = "securePassthrough", @params = new { request = EncryptedPayload } });
            RestResponse<string> response = Client.Execute<string>(Request);
            JsonResults.EncryptedResult _ResultJSON = JsonConvert.DeserializeObject<JsonResults.EncryptedResult>(response.Content);
            JsonResults.DeviceInfoResult _DeviceInfo = JsonConvert.DeserializeObject<JsonResults.DeviceInfoResult>(_Cipher.Decrypt(_ResultJSON.Result.Response));
            return _DeviceInfo;
        }
        protected long GetTimeStamp()
        {
            return (long)Math.Round(DateTime.Now.Subtract(new DateTime(1970, 1, 1, 0, 0, 0)).TotalMilliseconds);
        }
    }
}
