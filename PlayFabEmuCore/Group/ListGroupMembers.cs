using PlayFab.GroupsModels;
using PlayFabEmuCore.BackEnd;

namespace PlayFabEmuCore;

internal partial class Group
{
    [HTTP("POST", "/Group/ListGroupMembers")]
    [HTTP("POST", "/Group/ListGroupMembers?{args}")]
    public static bool ListGroupMembers(HttpRequest req, ServerStruct serverStruct)
    {
        var request = JsonConvert.DeserializeObject<ListGroupMembersRequest>(req.Body);
        if (serverStruct.ReturnIfNull(request))
            return true;
        List<EntityMemberRole> entityMemberRoles = [];
        var group = DBManager.FabGroup.GetOne(x => x.Name == request.Group.Id);
        if (group != null)
        {
            foreach (var grouped in group.MembersAndRoles.GroupBy(x => x.Value))
            {
                EntityMemberRole entityMemberRole = new()
                {
                    Members = [],
                    RoleId = grouped.Key,
                    RoleName = group.Roles[grouped.Key]
                };

                foreach (var kv in grouped)
                {
                    entityMemberRole.Members.Add(new()
                    {
                        Key = new()
                        {
                            Id = kv.Key,
                            Type = "title_player_account"
                        },
                        Lineage = []
                    });
                }
                entityMemberRoles.Add(entityMemberRole);
            }
        }
        return serverStruct.SendSuccess<ListGroupMembersResponse>(new()
        { 
            Members = entityMemberRoles
        });
    }
}
