using PlayFab.GroupsModels;
using PlayFabEmuCore.BackEnd;

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
        List<GroupBlock> groupBlocks = [];
        var group = DBManager.FabGroup.GetOne(x => x.Name == request.Group.Id);
        if (group != null)
        {
            foreach (var item in group.Blocked)
            {
                groupBlocks.Add(new()
                {
                    Group = request.Group,
                    Entity = new()
                    {
                        Key = new()
                        {
                            Id = item,
                            Type = "title_player_account"
                        },
                        Lineage = []
                    },
                });
            }
        }
        return serverStruct.SendSuccess<ListGroupBlocksResponse>(new()
        { 
            BlockedEntities = groupBlocks
        });
    }
}
