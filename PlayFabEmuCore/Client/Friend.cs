using ModdableWebServer.Attributes;
using ModdableWebServer.Helper;
using ModdableWebServer;
using NetCoreServer;
using Newtonsoft.Json;
using PlayFab.ClientModels;
using PlayFab.Internal;
using PlayFab;

namespace PlayFabEmuCore.Client;

internal class Friend
{
    [HTTP("POST", "/Client/AddFriend?{args}")]
    public static bool AddFriend(HttpRequest req, ServerStruct serverStruct)
    {
        foreach (var item in serverStruct.Parameters)
        {
            Console.WriteLine(item.Key + " " + item.Value);
        }
        _ = JsonConvert.DeserializeObject<AddFriendRequest>(req.Body);

        var ret = new PlayFabJsonSuccess<AddFriendResult>()
        { 
            
        
        };

        serverStruct.Response.MakeOkResponse(204);
        serverStruct.SendResponse();
        return true;
    }
}
