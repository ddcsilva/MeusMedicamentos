using MeusMedicamentos.Domain.Enums;

namespace MeusMedicamentos.Domain.Entities
{
    public abstract class Entity
    {
        protected Entity()
        {
            DataCriacao = DateTime.UtcNow;
            Status = EStatus.Ativo;
        }

        public int Id { get; protected set; }
        public DateTime DataCriacao { get; protected set; }
        public DateTime? DataModificacao { get; protected set; }
        public string UsuarioCriacao { get; protected set; } = string.Empty;
        public string? UsuarioModificacao { get; protected set; }
        public EStatus Status { get; protected set; }

        public void SetStatus(EStatus status)
        {
            Status = status;
        }

        public void SetUsuario(string usuario)
        {
            UsuarioCriacao = usuario;
        }

        public void SetDataModificacao()
        {
            DataModificacao = DateTime.UtcNow;
        }
    }
}
