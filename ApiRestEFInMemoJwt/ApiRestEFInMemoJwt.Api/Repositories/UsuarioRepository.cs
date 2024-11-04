using ApiRestEFInMemoJwt.Api.Models;

namespace ApiRestEFInMemoJwt.Api.Repositories;

public interface IUsuarioRepository
{
    Usuario Read(string email, string senha);
    void Create (Usuario usuario);
}
public class UsuarioRepository : IUsuarioRepository
{
    public readonly DataContext _context;

    public UsuarioRepository(DataContext context)
    {
        _context = context;
    }
    public void Create(Usuario usuario)
    {
        usuario.Id = Guid.NewGuid();
        _context.Usuarios.Add(usuario);
        _context.SaveChanges();
    }

    public Usuario Read(string email, string senha)
    {
        return _context.Usuarios.SingleOrDefault(u => u.Email.Equals(email) && u.Senha.Equals(senha));
    }
}
