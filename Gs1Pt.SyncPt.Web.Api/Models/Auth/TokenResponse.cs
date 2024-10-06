using System.Text.Json.Serialization;

namespace Gs1Pt.SyncPt.Web.Api.Models.Auth
{
    public class TokenResponse
    {
        [JsonPropertyName("access_token")]
        public required string AccessToken { get; set; }
        [JsonPropertyName("expires_in")]
        public int ExpiresIn { get; set; }
        [JsonPropertyName("token_type")]
        public string TokenType { get; set; } = "Bearer";
    }
}
