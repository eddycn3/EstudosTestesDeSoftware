using CursoOnline.Application.Dtos;
using CursoOnline.Domain;
using CursoOnline.Domain.Base;
using CursoOnline.Domain.Interfaces;

namespace CursoOnline.Application.Services
{
    public class ArmazenadorDeMatriculaService
    {
        private readonly IAlunoRepository _alunoRepository;
        private readonly ICursoRepository _cursoRepository;
        private readonly IMatriculaRepository _matriculaRepository;

        public ArmazenadorDeMatriculaService(IAlunoRepository alunoRepository, ICursoRepository cursoRepository, IMatriculaRepository matriculaRepository)
        {
            _alunoRepository = alunoRepository;
            _cursoRepository = cursoRepository;
            _matriculaRepository = matriculaRepository;
        }

        public void Criar(MatriculaDto matriculaDto)
        {
            var aluno = _alunoRepository.ObterPorId(matriculaDto.AlunoId);
            var curso = _cursoRepository.ObterPeloId(matriculaDto.CursoId);

            ValidadorDeRegra.Novo()
                 .Quando(aluno is null, MensagensValidacaoDeDominio.AlunoNaoEncontrado)
                 .Quando(curso is null, MensagensValidacaoDeDominio.CursoNaoEncontrado)
                 .DispararExcecaoSeExistir();

            var matricula = new Matricula(aluno, curso, matriculaDto.ValorMatricula);

            if (matriculaDto.Id == 0)
                _matriculaRepository.Adicionar(matricula);
        }
    }
}
