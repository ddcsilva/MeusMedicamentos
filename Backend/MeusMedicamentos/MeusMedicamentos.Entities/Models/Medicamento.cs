using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MeusMedicamentos.Entities.Models;

public class Medicamento
{
    [Column("MedicamentoId")]
    public Guid Id { get; set; }
    
    [Required(ErrorMessage = "O nome do medicamento é obrigatório.")]
    [MaxLength(60, ErrorMessage = "O nome do medicamento deve ter no máximo 60 caracteres.")]
    public string? Nome { get; set; }
    
    [ForeignKey(nameof(Categoria))]
    public Guid CategoriaId { get; set; }
    public Categoria? Categoria { get; set; }
}