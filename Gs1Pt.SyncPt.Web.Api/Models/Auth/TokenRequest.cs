using System.ComponentModel.DataAnnotations;

namespace Gs1Pt.SyncPt.Web.Api.Models.Auth
{
    public class TokenRequest
    {
        [Required(ErrorMessage = "Email é obrigatório")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password é obrigatória")]
        public string Password { get; set; }
    }
}
