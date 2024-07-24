using MeusMedicamentos.Application.Interfaces;
using MeusMedicamentos.Domain.Entities;
using MeusMedicamentos.Domain.Interfaces;
using AutoMapper;
using MeusMedicamentos.Application.DTOs;

namespace MeusMedicamentos.Application.Services;

public class CategoriaService : ICategoriaService
{
    private readonly ICategoriaRepository _categoriaRepository;
    private readonly IMapper _mapper;

    public CategoriaService(ICategoriaRepository categoriaRepository, IMapper mapper)
    {
        _categoriaRepository = categoriaRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CategoriaDTO>> ObterTodosAsync()
    {
        var categorias = await _categoriaRepository.ObterTodosAsync();
        return _mapper.Map<IEnumerable<CategoriaDTO>>(categorias);
    }

    public async Task<CategoriaDTO?> ObterPorIdAsync(int id)
    {
        var categoria = await _categoriaRepository.ObterPorIdAsync(id);
        return _mapper.Map<CategoriaDTO>(categoria);
    }

    public async Task AdicionarAsync(CriarCategoriaDTO categoriaDto)
    {
        var categoria = _mapper.Map<Categoria>(categoriaDto);
        await _categoriaRepository.AdicionarAsync(categoria);
        await _categoriaRepository.SalvarAlteracoesAsync();
    }

    public async Task AtualizarAsync(EditarCategoriaDTO categoriaDto)
    {
        var categoria = await _categoriaRepository.ObterPorIdAsync(categoriaDto.Id);
        if (categoria == null) throw new Exception("Categoria não encontrada");

        _mapper.Map(categoriaDto, categoria);
        await _categoriaRepository.AtualizarAsync(categoria);
        await _categoriaRepository.SalvarAlteracoesAsync();
    }

    public async Task RemoverAsync(int id)
    {
        var categoria = await _categoriaRepository.ObterPorIdAsync(id);
        if (categoria == null) throw new Exception("Categoria não encontrada");

        await _categoriaRepository.RemoverAsync(categoria);
        await _categoriaRepository.SalvarAlteracoesAsync();
    }
}
