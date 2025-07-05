using PlayFab.ClientModels;
using System.Security.Cryptography;

namespace PlayFabEmuCore;

internal partial class Client
{
    [HTTP("POST", "/Client/GetPhotonAuthenticationToken?{args}")]
    public static bool GetPhotonAuthenticationToken(HttpRequest _, ServerStruct serverStruct)
    {
        return serverStruct.SendSuccess<GetPhotonAuthenticationTokenResult>(new()
        { 
            PhotonCustomAuthenticationToken = RandomNumberGenerator.GetString("0123456789abcdef", 32)
        });
    }
}
