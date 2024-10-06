namespace Gs1Pt.SyncPt.Web.Api.Models
{
    public class ConnectionStrings
    {
        public required string SyncPtDb { get; set; }
    }

    public class Settings
    {
        public required JWTSettings JWTSettings { get; set; }
    }
    public class JWTSettings
    {
        public required string SecurityKey { get; set; }
        public required string ValidIssuer { get; set; }
        public required string ValidAudience { get; set; }
        public required int ExpiryInMinutes { get; set; }
    }
}