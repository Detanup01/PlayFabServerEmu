using ModdableWebServer;
using ModdableWebServer.Attributes;
using ModdableWebServer.Helper;
using NetCoreServer;

namespace PlayFabEmuCore.Test;

public class TestAPI
{
    [HTTP("POST", "/{test}?{args}")]
    public static bool PostTestEverything(HttpRequest _, ServerStruct serverStruct)
    {
        foreach (var item in serverStruct.Parameters)
        {
            Console.WriteLine(item.Key + " " + item.Value);
        }


        serverStruct.Response.MakeOkResponse(200);
        serverStruct.SendResponse();
        return true;
    }
}
