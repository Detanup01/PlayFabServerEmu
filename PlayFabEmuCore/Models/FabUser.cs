using PlayFab.ClientModels;

namespace PlayFabEmuCore.Models;

public class FabUser
{
    [LiteDB.BsonId]
    public int DataBaseId { get; set; }
    public FabId PlayFabId { get; set; } = FabId.Empty;
    public FabId GameId { get; set; } = FabId.Empty;
    public FabId TitleAccountId { get; set; } = FabId.Empty;
    public string TitleId { get; set; } = string.Empty;
    public FabId RandomId { get; set; } = FabId.Empty;
    public string PlatformId { get; set; } = string.Empty;
    public string PlatformType { get; set; } = string.Empty;
    public string DisplayName { get; set; } = string.Empty;
    public Dictionary<string, UserDataRecord> CustomData { get; set; } = [];
    public uint DataVersion { get; set; } = 0;
}
