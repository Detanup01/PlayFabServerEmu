using PlayFab.ClientModels;

namespace PlayFabEmuCore;

internal partial class Client
{
    [HTTP("POST", "/Client/AcceptTrade?{args}")]
    public static bool AcceptTrade(HttpRequest req, ServerStruct serverStruct)
    {
        foreach (var item in serverStruct.Parameters)
        {
            Console.WriteLine(item.Key + " " + item.Value);
        }
        _ = JsonConvert.DeserializeObject<AcceptTradeRequest>(req.Body);
        serverStruct.Response.MakeOkResponse(204);
        serverStruct.SendResponse();
        return true;
    }
}
