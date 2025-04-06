namespace PlayFabEmuCore.Models;

public class FabLobby
{
    [LiteDB.BsonId]
    public int DataBaseId { get; set; }
    public PlayFab.MultiplayerModels.Lobby Lobby { get; set; } = new();
}
