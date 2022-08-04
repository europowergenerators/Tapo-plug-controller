# Tapo-plug-controller

C# port of project by fishbigger https://github.com/fishbigger/TapoP100.
Usage is pretty much similar. 

```C#
using TapoPlugController.TapoDevices;
namespace DemoProgram
{
    class Program
    {
        static void Main(string[] args)
        {
            P110 _Plug = new P110("192.168.0.XXX", "username@domain.com", "password");
            _Plug.Handshake();
            _Plug.Login();
            _Plug.SetDeviceStatus(false); //Turn device off
            _Plug.SetDeviceStatus(true); //Turn device on
            var EnergyInfo = _Plug.GetEnergyInfo(); // Get Energy consumption info
        }
    }
}
```
