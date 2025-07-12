using PlayFab.Internal;
using PlayFab;
using PlayFab.ClientModels;

namespace PlayFabEmuCore;

internal partial class Client
{
    [HTTP("POST", "/Client/LoginWithCustomID?{args}")]
    public static bool LoginWithCustomID(HttpRequest req, ServerStruct serverStruct)
    {
        foreach (var item in serverStruct.Parameters)
        {
            Console.WriteLine(item.Key + " " + item.Value);
        }
        _ = JsonConvert.DeserializeObject<LoginWithCustomIDRequest>(req.Body);

        var ret = new PlayFabJsonSuccess<LoginResult>()
        {
            data = new()
            {
                EntityToken = new()
                { 
                    Entity = new()
                    { 
                        Id = "CUSTOMID",
                        Type = "title_player_account"
                    }
                }
            },
            code = 200,
            status = "OK"
        };

        var error = new PlayFabError()
        {
            HttpCode = 400,
            Error = PlayFabErrorCode.AccountNotFound,
            ErrorMessage = "User not found",
            HttpStatus = "BadRequest",
        };

        serverStruct.Response.MakeOkResponse(204);
        serverStruct.SendResponse();
        return true;
    }
}
