using Newtonsoft.Json;
using PlayFab;

namespace PlayFabEmuCore;

public class ServerSettings
{
    public static ServerSettings Instance()
    {
        if (File.Exists("PlayFabEmu.config.json"))
        {
            ServerSettings? settings = JsonConvert.DeserializeObject<ServerSettings>(File.ReadAllText("PlayFabEmu.config.json"));
            if (settings != null)
            {
                return settings;
            }
        }
        ServerSettings ret = new();
        File.WriteAllText("PlayFabEmu.config.json", JsonConvert.SerializeObject(ret, Formatting.Indented));
        return ret;
    }


    public bool UseHTTPS { get; set; } = false;
    public SSLSetttings SSL { get; set; } = new();
    public string HostOn { get; set; } = "192.168.1.50";

    public class SSLSetttings
    {
        public string CertPath { get; set; } = "cert.crt";
        public string KeyPath { get; set; } = "cert.key";
        public bool VerifySSL_ServerSide { get; set; } = false;
    }

    public List<PlayFabApiSettings> PlayFabApiSettings { get; set; } = [new() { TitleId = "A247C" }];

}

