using MeusMedicamentos.Domain.Entities;
using MeusMedicamentos.Domain.Validations;

namespace MeusMedicamentos.Domain.Tests;

public class CategoriaTests
{
    private readonly CategoriaValidator _validator;

    public CategoriaTests()
    {
        _validator = new CategoriaValidator();
    }

    [Fact]
    public void CriarCategoria_QuandoFornecidoCorretamente_DeveSerValido()
    {
        // Arrange
        var categoria = new Categoria("Medicamentos");

        // Act
        var resultado = _validator.Validate(categoria);

        // Assert
        Assert.True(resultado.IsValid);
    }

    [Fact]
    public void CriarCategoria_QuandoForVazio_DeveSerInvalido()
    {
        // Arrange
        var categoria = new Categoria(string.Empty);

        // Act
        var resultado = _validator.Validate(categoria);

        // Assert
        Assert.False(resultado.IsValid);
        Assert.Contains(resultado.Errors, e => e.PropertyName == "Nome" && e.ErrorMessage == "O campo Nome deve ser fornecido");
    }

    [Fact]
    public void CriarCategoria_QuandoTamanhoInvalido_DeveSerInvalido()
    {
        // Arrange
        var categoria = new Categoria("A");

        // Act
        var resultado = _validator.Validate(categoria);

        // Assert
        Assert.False(resultado.IsValid);
        Assert.Contains(resultado.Errors, e => e.PropertyName == "Nome" && e.ErrorMessage == "O campo Nome deve ter entre 2 e 100 caracteres");
    }
}