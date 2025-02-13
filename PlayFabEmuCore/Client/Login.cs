using ModdableWebServer.Attributes;
using ModdableWebServer;
using NetCoreServer;
using Newtonsoft.Json;
using PlayFab.ClientModels;
using PlayFab.Internal;
using ModdableWebServer.Helper;
using PlayFab;

namespace PlayFabEmuCore.Client;

internal class Login
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

    [HTTP("POST", "/Client/LoginWithSteam?{args}")]
    public static bool LoginWithSteam(HttpRequest req, ServerStruct serverStruct)
    {
        var steam = JsonConvert.DeserializeObject<LoginWithSteamRequest>(req.Body);

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
                },
                SessionTicket = "",
                SettingsForUser = new()
                {
                    NeedsAttribution = false,
                    GatherDeviceInfo = false,
                    GatherFocusInfo = false
                },
                LastLoginTime = DateTime.Now,
                TreatmentAssignment = new()
                {
                    Variables = [],
                    Variants = [],
                },
                NewlyCreated = false,
                PlayFabId = "s"
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

        serverStruct.Response.MakeGetResponse(JsonConvert.SerializeObject(ret));
        serverStruct.SendResponse();
        return true;
    }
}
