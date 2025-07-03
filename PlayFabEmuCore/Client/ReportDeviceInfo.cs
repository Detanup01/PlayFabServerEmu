using PlayFab.ClientModels;

namespace PlayFabEmuCore;

internal partial class Client
{
    [HTTP("POST", "/Client/ReportDeviceInfo?{args}")]
    public static bool ReportDeviceInfo(HttpRequest _, ServerStruct serverStruct)
    {
        return serverStruct.SendSuccess<EmptyResponse>(new());
    }
}
