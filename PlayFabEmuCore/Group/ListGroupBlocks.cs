using PlayFab.GroupsModels;

namespace PlayFabEmuCore;

internal partial class Group
{
    [HTTP("POST", "/Group/ListGroupBlocks")]
    [HTTP("POST", "/Group/ListGroupBlocks?{args}")]
    public static bool ListGroupBlocks(HttpRequest req, ServerStruct serverStruct)
    {
        var request = JsonConvert.DeserializeObject<ListGroupBlocksRequest>(req.Body);
        if (serverStruct.ReturnIfNull(request))
            return true;
        return serverStruct.SendSuccess<ListGroupBlocksResponse>(new());
    }
}
