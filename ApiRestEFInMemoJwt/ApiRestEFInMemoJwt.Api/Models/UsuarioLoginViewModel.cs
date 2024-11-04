using System.ComponentModel.DataAnnotations;

namespace ApiRestEFInMemoJwt.Api.Models
{
    public class UsuarioLoginViewModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Senha { get; set; }
    }
}
