using PlayFab.Internal;
using PlayFab;
using PlayFab.ClientModels;
using PlayFabEmuCore.BackEnd;
using PlayFabEmuCore.Models;

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

    [HTTP("POST", "/Client/LoginWithSteam?{args}")]
    public static bool LoginWithSteam(HttpRequest req, ServerStruct serverStruct)
    {
        Console.WriteLine(req.Body);
        var steam = JsonConvert.DeserializeObject<LoginWithSteamRequest>(req.Body);
        if (serverStruct.ReturnIfNull(steam))
            return true;
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
        /*
        var error = new PlayFabError()
        {
            HttpCode = 400,
            Error = PlayFabErrorCode.AccountNotFound,
            ErrorMessage = "User not found",
            HttpStatus = "BadRequest",
        };
        */
        return serverStruct.SendSuccess<LoginResult>(new()
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
        });
    }
}
