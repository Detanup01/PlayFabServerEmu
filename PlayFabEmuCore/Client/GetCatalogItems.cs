using PlayFab.ClientModels;

namespace PlayFabEmuCore;

internal partial class Client
{
    [HTTP("POST", "/Client/GetCatalogItems?{args}")]
    public static bool GetCatalogItems(HttpRequest _, ServerStruct serverStruct)
    {
        return serverStruct.SendSuccess<GetCatalogItemsResult>(new());
    }
}
