namespace MeusMedicamentos.Domain.Entities
{
    public sealed class Categoria : Entity
    {
        public Categoria(string nome)
        {
            Nome = nome;
        }

        public string Nome { get; set; } = string.Empty;

        public Usuario UsuarioCriacao { get; set; } = null!;
        public Usuario? UsuarioModificacao { get; set; }
    }
}
