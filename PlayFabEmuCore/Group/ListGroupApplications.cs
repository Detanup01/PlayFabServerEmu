using PlayFab.GroupsModels;
using PlayFabEmuCore.BackEnd;

namespace PlayFabEmuCore;

internal partial class Group
{
    [HTTP("POST", "/Group/ListGroupApplications")]
    [HTTP("POST", "/Group/ListGroupApplications?{args}")]
    public static bool ListGroupApplications(HttpRequest req, ServerStruct serverStruct)
    {
        var request = JsonConvert.DeserializeObject<ListGroupApplicationsRequest>(req.Body);
        if (serverStruct.ReturnIfNull(request))
            return true;
        List<GroupApplication> groupApplications = [];
        var group = DBManager.FabGroup.GetOne(x => x.Name == request.Group.Id);
        if (group != null)
        {
            foreach (var item in group.Applications)
            {
                groupApplications.Add(new()
                {
                    Group = request.Group,
                    Entity = new()
                    { 
                        Key = new()
                        { 
                            Id = item.Key,
                            Type = "title_player_account"
                        },
                        Lineage = []
                    },
                    Expires = item.Value
                });
            }
        }
        return serverStruct.SendSuccess<ListGroupApplicationsResponse>(new()
        { 
            Applications = groupApplications
        });
    }
}
