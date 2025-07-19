using PlayFab.EconomyModels;

namespace PlayFabEmuCore;

internal partial class Catalog
{
    [HTTP("POST", "/Catalog/UpdateCatalogConfig")]
    [HTTP("POST", "/Catalog/UpdateCatalogConfig?{args}")]
    public static bool UpdateCatalogConfig(HttpRequest req, ServerStruct serverStruct)
    {
        var request = JsonConvert.DeserializeObject<UpdateCatalogConfigRequest>(req.Body);
        if (serverStruct.ReturnIfNull(request))
            return true;
        var token = serverStruct.GetSessionInfoFromServer();
        if (serverStruct.ReturnIfNull(token))
            return true;
        return serverStruct.SendSuccess<UpdateCatalogConfigResponse>(new());
    }
}
