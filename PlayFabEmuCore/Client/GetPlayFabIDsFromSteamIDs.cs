using PlayFab.ClientModels;

namespace PlayFabEmuCore;

internal partial class Client
{
    [HTTP("POST", "/Client/GetPlayFabIDsFromSteamIDs?{args}")]
    public static bool GetPlayFabIDsFromSteamIDs(HttpRequest req, ServerStruct serverStruct)
    {
        var request = JsonConvert.DeserializeObject<GetPlayFabIDsFromSteamIDsRequest>(req.Body);
        if (serverStruct.ReturnIfNull(request))
            return true;
        var token = serverStruct.GetSessionInfoFromServer();
        if (serverStruct.ReturnIfNull(token))
            return true;
        if (request.SteamStringIDs.Count == 0)
            return serverStruct.SendError(new()
            { 
                HttpCode = 400,
                HttpStatus = "BadRequest",
                Error = PlayFab.PlayFabErrorCode.InvalidParams,
                ErrorMessage = "Invalid input paramters",
                ErrorDetails =
                {
                    { "", ["One of the following properties must be defined: SteamIDs, SteamStringIDs"] }
                }
            });
        return serverStruct.SendSuccess<GetPlayFabIDsFromSteamIDsResult>(new()
        {
            Data = []
        });
    }
}
