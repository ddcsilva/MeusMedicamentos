using MeusMedicamentos.Domain.Enums;
using System;

namespace MeusMedicamentos.Domain.Entities
{
    public abstract class Entity
    {
        protected Entity()
        {
            Id = Guid.NewGuid();
            DataCriacao = DateTime.UtcNow;
            Status = EStatus.Ativo;
        }

        public Guid Id { get; protected set; }
        public DateTime DataCriacao { get; protected set; }
        public DateTime? DataModificacao { get; protected set; }
        public Guid UsuarioCriacaoId { get; protected set; }
        public Guid? UsuarioModificacaoId { get; protected set; }
        public EStatus Status { get; protected set; }

        public void SetStatus(EStatus status)
        {
            Status = status;
        }

        public void SetUsuarioCriacaoId(Guid usuarioId)
        {
            UsuarioCriacaoId = usuarioId;
        }

        public void SetUsuarioModificacaoId(Guid usuarioId)
        {
            UsuarioModificacaoId = usuarioId;
        }

        public void SetDataModificacao()
        {
            DataModificacao = DateTime.UtcNow;
        }
    }
}
