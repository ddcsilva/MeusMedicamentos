using AutoMapper;
using FluentValidation;
using MeusMedicamentos.Application.DTOs;
using MeusMedicamentos.Application.Interfaces;
using MeusMedicamentos.Domain.Entities;
using MeusMedicamentos.Domain.Interfaces;
using MeusMedicamentos.Domain.Notifications;
using MeusMedicamentos.Domain.Validations;
using MeusMedicamentos.Shared;

namespace MeusMedicamentos.Application.Services
{
    public class CategoriaService : BaseService, ICategoriaService
    {
        private readonly ICategoriaRepository _categoriaRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CategoriaService(ICategoriaRepository categoriaRepository, IUnitOfWork unitOfWork, IMapper mapper, IValidator<Categoria> validator, INotificadorErros notificador)
            : base(notificador)
        {
            _categoriaRepository = categoriaRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ApiResponse<IEnumerable<CategoriaDTO>>> ObterTodosAsync()
        {
            var categorias = await _categoriaRepository.ObterTodosAsync();
            var categoriaDTOs = _mapper.Map<IEnumerable<CategoriaDTO>>(categorias);
            return new ApiResponse<IEnumerable<CategoriaDTO>>(categoriaDTOs);
        }

        public async Task<ApiResponse<CategoriaDTO>> ObterPorIdAsync(int id)
        {
            var categoria = await _categoriaRepository.ObterPorIdAsync(id);
            if (categoria == null)
            {
                Notificar("Categoria não encontrada");
                return new ApiResponse<CategoriaDTO>("Categoria não encontrada");
            }

            var categoriaDTO = _mapper.Map<CategoriaDTO>(categoria);
            return new ApiResponse<CategoriaDTO>(categoriaDTO);
        }

        public async Task<ApiResponse<CategoriaDTO>> AdicionarAsync(CriarCategoriaDTO categoriaDTO)
        {
            var categoria = _mapper.Map<Categoria>(categoriaDTO);

            var existente = await _categoriaRepository.ObterPorCondicaoAsync(c => c.Nome == categoria.Nome);
            if (existente.Any())
            {
                Notificar("Já existe uma categoria com o mesmo nome.");
                return new ApiResponse<CategoriaDTO>("Já existe uma categoria com o mesmo nome.");
            }

            var validationResult = await new CategoriaValidator().ValidateAsync(categoria);
            if (!validationResult.IsValid)
            {
                Notificar(validationResult);
                return new ApiResponse<CategoriaDTO>(validationResult.Errors.Select(e => e.ErrorMessage).ToList());
            }

            await _categoriaRepository.AdicionarAsync(categoria);
            var sucesso = await _unitOfWork.SalvarAlteracoesAsync();
            if (!sucesso)
            {
                Notificar("Erro ao salvar categoria.");
                return new ApiResponse<CategoriaDTO>("Erro ao salvar categoria.");
            }

            var categoriaDtoRetorno = _mapper.Map<CategoriaDTO>(categoria);
            return new ApiResponse<CategoriaDTO>(categoriaDtoRetorno);
        }

        public async Task<ApiResponse<string>> AtualizarAsync(EditarCategoriaDTO categoriaDTO)
        {
            var categoria = await _categoriaRepository.ObterPorIdAsync(categoriaDTO.Id);
            if (categoria == null)
            {
                Notificar("Categoria não encontrada");
                return new ApiResponse<string>("Categoria não encontrada");
            }

            _mapper.Map(categoriaDTO, categoria);

            var validationResult = await new CategoriaValidator().ValidateAsync(categoria);
            if (!validationResult.IsValid)
            {
                Notificar(validationResult);
                return new ApiResponse<string>(validationResult.Errors.Select(e => e.ErrorMessage).ToList());
            }

            await _categoriaRepository.AtualizarAsync(categoria);
            var sucesso = await _unitOfWork.SalvarAlteracoesAsync();
            if (!sucesso)
            {
                Notificar("Erro ao atualizar categoria.");
                return new ApiResponse<string>("Erro ao atualizar categoria.");
            }

            return new ApiResponse<string>("Categoria atualizada com sucesso");
        }

        public async Task<ApiResponse<string>> RemoverAsync(int id)
        {
            var categoria = await _categoriaRepository.ObterPorIdAsync(id);
            if (categoria == null)
            {
                Notificar("Categoria não encontrada");
                return new ApiResponse<string>("Categoria não encontrada");
            }

            await _categoriaRepository.RemoverAsync(categoria);
            var sucesso = await _unitOfWork.SalvarAlteracoesAsync();
            if (!sucesso)
            {
                Notificar("Erro ao remover categoria.");
                return new ApiResponse<string>("Erro ao remover categoria.");
            }

            return new ApiResponse<string>("Categoria removida com sucesso");
        }
    }
}
