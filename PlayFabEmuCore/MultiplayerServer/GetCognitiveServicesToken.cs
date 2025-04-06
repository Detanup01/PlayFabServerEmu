namespace PlayFabEmuCore.MultiplayerServer;

internal partial class MultiplayerServer
{
    [HTTP("POST", "/MultiplayerServer/GetCognitiveServicesToken?{args}")]
    public static bool GetCognitiveServicesToken(HttpRequest req, ServerStruct serverStruct)
    {
        return serverStruct.SendError(new()
        {
            Error = PlayFab.PlayFabErrorCode.MultiplayerServerUnavailable,
            HttpCode = 404,
            HttpStatus = "NotFound",
            ErrorMessage = "Nah"
        });
    }
}
