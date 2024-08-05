using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MeusMedicamentos.Entities.Models;

public class Categoria
{
    [Column("CategoriaId")]
    public Guid Id { get; set; }
    
    [Required(ErrorMessage = "O nome da categoria é obrigatório.")]
    [MaxLength(60, ErrorMessage = "O nome da categoria deve ter no máximo 60 caracteres.")]
    public string? Nome { get; set; }
    
    public ICollection<Medicamento>? Medicamentos { get; set; }
}