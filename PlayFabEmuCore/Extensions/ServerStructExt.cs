using PlayFab;
using PlayFab.Internal;
using PlayFab.Json;
using System.Diagnostics.CodeAnalysis;

namespace PlayFabEmuCore.Extensions;

public static class ServerStructExt
{
    public static string GetEntityToken(this ServerStruct serverStruct)
    {
        string? value = null;
        if (serverStruct.Headers.ContainsKey("x-entitytoken"))
        {
            if (serverStruct.Headers.TryGetValue("x-entitytoken", out value))
                return string.Empty;
        }
        if (serverStruct.Headers.ContainsKey("x-authorization"))
        {
            if (serverStruct.Headers.TryGetValue("x-authorization", out value))
                return string.Empty;
        }
        return value ?? string.Empty;
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
