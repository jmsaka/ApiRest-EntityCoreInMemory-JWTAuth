namespace ApiRestEFInMemoJwt.Api.Models;

public class Tarefa
{
    public Guid Id { get; set; }
    public Guid UsuarioId { get; set; }
    public string Nome { get; set; }
    public bool Concluida { get; set; }
}
