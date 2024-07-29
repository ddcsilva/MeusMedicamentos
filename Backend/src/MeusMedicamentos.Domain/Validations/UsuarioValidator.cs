using FluentValidation;
using MeusMedicamentos.Domain.Entities;

namespace MeusMedicamentos.Domain.Validations
{
    public class UsuarioValidator : AbstractValidator<Usuario>
    {
        public UsuarioValidator()
        {
            RuleFor(u => u.Nome)
                .NotEmpty().WithMessage("O campo {PropertyName} deve ser fornecido")
                .Length(2, 100).WithMessage("O campo {PropertyName} deve ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(u => u.Email)
                .NotEmpty().WithMessage("O campo {PropertyName} deve ser fornecido")
                .EmailAddress().WithMessage("O campo {PropertyName} deve ser um endereço de e-mail válido");

            RuleFor(u => u.UserName)
                .NotEmpty().WithMessage("O campo {PropertyName} deve ser fornecido")
                .Length(2, 50).WithMessage("O campo {PropertyName} deve ter entre {MinLength} e {MaxLength} caracteres");
        }
    }
}
