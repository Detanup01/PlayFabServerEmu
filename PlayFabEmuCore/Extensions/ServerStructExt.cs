using PlayFab;
using PlayFab.Internal;
using PlayFab.Json;
using PlayFabEmuCore.Models;
using System.Diagnostics.CodeAnalysis;

namespace PlayFabEmuCore.Extensions;

public static class ServerStructExt
{
    public static (FabId PlayFabId, FabId GameId, FabId TitleAccountId, string TitleId)? GetSessionInfoFromServer(this ServerStruct serverStruct)
    {
        string cred = string.Empty;
        var ftoken = GetPlayFabToken(serverStruct);
        if (ftoken != null)
            cred = ftoken.EntityCredentials.Replace("title_player_account!", string.Empty);
        var etoken = GetEntityToken(serverStruct);
        if (etoken != null)
            cred = etoken;
        if (string.IsNullOrEmpty(cred))
            return null;
        return FabUserExt.GetSessionInfo(cred);
    }

    public static FabEntityToken? GetPlayFabToken(this ServerStruct serverStruct)
    {
        if (!serverStruct.Headers.TryGetValue("x-authorization", out string? auth))
            return null;
        if (string.IsNullOrEmpty(auth))
            return null;
        return auth.GetFabEntityToken();
    }

    public static string? GetEntityToken(this ServerStruct serverStruct)
    {
        if (!serverStruct.Headers.TryGetValue("x-entitytoken", out string? entitytoken))
            return null;
        if (string.IsNullOrEmpty(entitytoken))
            return null;
        return entitytoken;
    }

    public static bool ReturnIfNull<T>(this ServerStruct serverStruct, [NotNullWhen(false)] T? typeObject)
    {
        if (typeObject != null)
            return false;
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

    public static bool SendSuccess<T>(this ServerStruct serverStruct, T data) where T : PlayFabResultCommon
    {
        serverStruct.Response.MakeGetResponse(PlayFabSimpleJson.SerializeObject(new PlayFabJsonSuccess<T>()
        {
            code = 200,
            status = "OK",
            data = data
        }), "application/json");
        serverStruct.SendResponse();
        return true;
    }

    public static bool SendSuccess<T>(this ServerStruct serverStruct) where T : PlayFabResultCommon, new()
    {
        serverStruct.Response.MakeGetResponse(PlayFabSimpleJson.SerializeObject(new PlayFabJsonSuccess<T>()
        {
            code = 200,
            status = "OK",
            data = new()
        }), "application/json");
        serverStruct.SendResponse();
        return true;
    }

    public static bool SendError(this ServerStruct serverStruct, PlayFabError error)
    {
        serverStruct.Response.MakeGetResponse(PlayFabSimpleJson.SerializeObject(error), "application/json");
        serverStruct.SendResponse();
        return true;
    }
}
