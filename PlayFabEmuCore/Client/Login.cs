using ModdableWebServer.Attributes;
using ModdableWebServer;
using NetCoreServer;
using Newtonsoft.Json;
using PlayFab.ClientModels;
using PlayFab.Internal;
using ModdableWebServer.Helper;
using PlayFab;
using PlayFabEmuCore.BackEnd;
using PlayFabEmuCore.Helpers;
using PlayFabEmuCore.Models;
using PlayFabEmuCore.Extensions;
using PlayFab.Json;

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
        Console.WriteLine(req.Body);
        var steam = JsonConvert.DeserializeObject<LoginWithSteamRequest>(req.Body);
        if (steam == null)
        {
            serverStruct.Response.MakeGetResponse(JsonConvert.SerializeObject(new PlayFabError()
            {
                HttpCode = 400,
                Error = PlayFabErrorCode.JsonParseError,
                ErrorMessage = "Json Parse Error",
                HttpStatus = "BadRequest",
            }));
            serverStruct.SendResponse();
            return true;
        }
        var ticket = AppTickets.GetTicket(Convert.FromHexString(steam.SteamTicket));
        var steam_id = ticket.SteamID.ToString();

        var user = DBManager.FabUser.GetOne(x=>x.TitleId == steam.TitleId && x.PlatformId == steam_id && x.PlatformType == "Steam");
        if (user == null || user.PlayFabId == FabId.Empty)
        {
            // if no user found create one.
            DBManager.FabUser.Create(user = new()
            { 
                PlayFabId = FabId.RandomId,
                GameId = FabId.RandomId,
                PlatformId = steam_id,
                PlatformType = "Steam",
                RandomId = FabId.RandomId,
                TitleAccountId = FabId.RandomId,
                TitleId = steam.TitleId
            });
        }

        var ret = new PlayFabJsonSuccess<LoginResult>()
        {
            data = new()
            {
                EntityToken = new()
                {
                    Entity = new()
                    {
                        Id = user.TitleAccountId,
                        Type = "title_player_account"
                    },
                    EntityToken = user.CreateSerializedEntityToken(),
                    TokenExpiration = DateTime.Now.AddDays(1)
                },
                SessionTicket = user.GenerateSessionTicket(),
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
                PlayFabId = user.PlayFabId
            },
            code = 200,
            status = "OK"
        };
        Console.WriteLine(PlayFabSimpleJson.SerializeObject(ret));
        /*
        var error = new PlayFabError()
        {
            HttpCode = 400,
            Error = PlayFabErrorCode.AccountNotFound,
            ErrorMessage = "User not found",
            HttpStatus = "BadRequest",
        };
        */

        serverStruct.Response.MakeGetResponse(PlayFabSimpleJson.SerializeObject(ret), "application/json");
        serverStruct.SendResponse();
        return true;
    }
}
