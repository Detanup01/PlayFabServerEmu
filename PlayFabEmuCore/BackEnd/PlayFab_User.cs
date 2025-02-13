namespace PlayFabEmuCore.BackEnd;

public class PlayFab_User
{

    /// <summary>
    /// 16 Character long Id with 0-9, A-F
    /// </summary>
    public string PlayFabId { get; set; } = string.Empty;
    public string GameId { get; set; } = string.Empty;
    public string TitleAccountId { get; set; } = string.Empty;

    // should be able to remove?
    public string TitleId { get; set; } = string.Empty;

    public string RandomId { get; set; } = string.Empty;
}
