using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ApiRestEFInMemoJwt.Api.Controllers
{
    [ApiController]
    [Route("usuario")]
    public class UsuarioController : ControllerBase
    {
        [HttpPost("")]
        public IActionResult Create([FromBody] Usuario model, [FromServices] IUsuarioRepository repository)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            repository.Create(model);

            return Ok();
        }

        [HttpPost("login")]
        public IActionResult Create([FromBody] UsuarioLoginViewModel model, [FromServices] IUsuarioRepository repository)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            Usuario usuario = repository.Read(model.Email, model.Senha);

            if (usuario == null)
                return Unauthorized();

            usuario.Senha = string.Empty;

            return Ok(new { usuario, token = GenerateToken(usuario) });
        }

        private string GenerateToken(Usuario usuario)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("UmaChaveSuperMegaBigExtraPlusPlusMagnificamenteEExtremamentGiganteParaDificultraAIdentificacao");
            var descriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, usuario.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(5),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(descriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
