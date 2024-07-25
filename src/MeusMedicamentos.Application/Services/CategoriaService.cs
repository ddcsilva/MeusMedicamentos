using MeusMedicamentos.Application.DTOs;
using MeusMedicamentos.Application.Interfaces;
using MeusMedicamentos.Domain.Entities;
using MeusMedicamentos.Domain.Interfaces;
using AutoMapper;
using FluentValidation;
using MeusMedicamentos.Shared;

namespace MeusMedicamentos.Application.Services;

public class CategoriaService : ICategoriaService
{
    private readonly ICategoriaRepository _categoriaRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IValidator<Categoria> _validator;

    public CategoriaService(ICategoriaRepository categoriaRepository, IUnitOfWork unitOfWork, IMapper mapper, IValidator<Categoria> validator)
    {
        _categoriaRepository = categoriaRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _validator = validator;
    }

    public async Task<ApiResponse<IEnumerable<CategoriaDTO>>> ObterTodosAsync()
    {
        var categorias = await _categoriaRepository.ObterTodosAsync();
        var CategoriaDTOs = _mapper.Map<IEnumerable<CategoriaDTO>>(categorias);
        return new ApiResponse<IEnumerable<CategoriaDTO>>(CategoriaDTOs);
    }

    public async Task<ApiResponse<CategoriaDTO>> ObterPorIdAsync(int id)
    {
        var categoria = await _categoriaRepository.ObterPorIdAsync(id);
        if (categoria == null)
        {
            return new ApiResponse<CategoriaDTO>("Categoria não encontrada");
        }

        var CategoriaDTO = _mapper.Map<CategoriaDTO>(categoria);
        return new ApiResponse<CategoriaDTO>(CategoriaDTO);
    }

    public async Task<ApiResponse<CategoriaDTO>> AdicionarAsync(CriarCategoriaDTO CategoriaDTO)
    {
        var categoria = _mapper.Map<Categoria>(CategoriaDTO);

        var existente = await _categoriaRepository.ObterPorCondicaoAsync(c => c.Nome == categoria.Nome);
        if (existente.Any())
        {
            return new ApiResponse<CategoriaDTO>("Já existe uma categoria com o mesmo nome.");
        }

        var validationResult = await _validator.ValidateAsync(categoria);
        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
            return new ApiResponse<CategoriaDTO>(errors);
        }

        await _categoriaRepository.AdicionarAsync(categoria);
        var sucesso = await _unitOfWork.SalvarAlteracoesAsync();
        if (!sucesso)
        {
            return new ApiResponse<CategoriaDTO>("Erro ao salvar categoria.");
        }

        var CategoriaDTORetorno = _mapper.Map<CategoriaDTO>(categoria);
        return new ApiResponse<CategoriaDTO>(CategoriaDTORetorno);
    }

    public async Task<ApiResponse<string>> AtualizarAsync(EditarCategoriaDTO CategoriaDTO)
    {
        var categoria = await _categoriaRepository.ObterPorIdAsync(CategoriaDTO.Id);
        if (categoria == null)
        {
            return new ApiResponse<string>("Categoria não encontrada");
        }

        _mapper.Map(CategoriaDTO, categoria);

        var validationResult = await _validator.ValidateAsync(categoria);
        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
            return new ApiResponse<string>(errors);
        }

        await _categoriaRepository.AtualizarAsync(categoria);
        var sucesso = await _unitOfWork.SalvarAlteracoesAsync();
        if (!sucesso)
        {
            return new ApiResponse<string>("Erro ao atualizar categoria.");
        }

        return new ApiResponse<string>("Categoria atualizada com sucesso");
    }

    public async Task<ApiResponse<string>> RemoverAsync(int id)
    {
        var categoria = await _categoriaRepository.ObterPorIdAsync(id);
        if (categoria == null)
        {
            return new ApiResponse<string>("Categoria não encontrada");
        }

        await _categoriaRepository.RemoverAsync(categoria);
        var sucesso = await _unitOfWork.SalvarAlteracoesAsync();
        if (!sucesso)
        {
            return new ApiResponse<string>("Erro ao remover categoria.");
        }

        return new ApiResponse<string>("Categoria removida com sucesso");
    }
}
