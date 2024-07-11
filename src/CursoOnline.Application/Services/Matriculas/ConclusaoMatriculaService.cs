using CursoOnline.Domain.Base;
using CursoOnline.Domain.Interfaces;

namespace CursoOnline.Application.Services.Matriculas
{
    public class ConclusaoMatriculaService
    {
        private readonly IMatriculaRepository _matriculaRepositor;
        public ConclusaoMatriculaService(IMatriculaRepository matriculaRepository)
        {
            _matriculaRepositor = matriculaRepository;
        }

        public void Concluir(int id, double notaAluno)
        {
            var _matricula  = _matriculaRepositor.ObterPorId(id);

            ValidadorDeRegra.Novo()
                .Quando(_matricula is null, MensagensValidacaoDeDominio.MatriculaNaoLocalizada)
                .DispararExcecaoSeExistir();

            _matricula.InformarNota(notaAluno);
        }

    }
}
