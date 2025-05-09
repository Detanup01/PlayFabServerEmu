﻿using PlayFab.MultiplayerModels;

namespace PlayFabEmuCore;

internal partial class Lobby
{
    [HTTP("POST", "/Lobby/SubscribeToLobbyResource")]
    public static bool SubscribeToLobbyResource(HttpRequest req, ServerStruct serverStruct)
    {
        var request = JsonConvert.DeserializeObject<SubscribeToLobbyResourceRequest>(req.Body);
        if (serverStruct.ReturnIfNull(request))
            return true;
        var token = serverStruct.GetEntityToken().GetFabEntityToken();
        if (serverStruct.ReturnIfNull(token))
            return true;;
        return serverStruct.SendSuccess<SubscribeToLobbyResourceResult>(new()
        {
            Topic = $"{request.SubscriptionVersion}~lobby~{request.Type}-{request.ResourceId}",
        });
    }
}
