using Newtonsoft.Json;

namespace PlayFabEmuCore.Models;

internal class FabEntityToken
{
    [JsonProperty(PropertyName = "i")]
    public string CreatedAt { get; set; } = string.Empty;

    [JsonProperty(PropertyName = "idp")]
    public string IdentityPlatform { get; set; } = string.Empty;

    [JsonProperty(PropertyName = "e")]
    public string ExpAt { get; set; } = string.Empty;

    [JsonProperty(PropertyName = "fi")]
    public string CreatedAt2 { get; set; } = string.Empty;

    // 11 Char long.
    [JsonProperty(PropertyName = "tid")]
    public string UnkownId { get; set; } = string.Empty;

    [JsonProperty(PropertyName = "idi")]
    public string IdentityId { get; set; } = string.Empty;

    // No idea if real name
    [JsonProperty(PropertyName = "h")]
    public string Host { get; set; } = "internal";

    // No idea if real name
    [JsonProperty(PropertyName = "ec")]
    public string EntityCredentials { get; set; } = string.Empty;

    // No idea if real name | AKA Title Acount Id
    [JsonProperty(PropertyName = "e")]
    public string EntityId { get; set; } = string.Empty;

    // No idea if real name
    [JsonProperty(PropertyName = "et")]
    public string EntityType { get; set; } = "title_player_account";
}
