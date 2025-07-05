namespace PlayFabEmuCore.Models;

public class FabTitle
{
    [LiteDB.BsonId]
    public int DataBaseId { get; set; }
    public string TitleId { get; set; } = string.Empty;
    public Dictionary<string, string> TitleData { get; set; } = [];
}
