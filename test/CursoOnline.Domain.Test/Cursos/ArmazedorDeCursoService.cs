using CursoOnline.Domain.Base;

namespace CursoOnline.Domain.Test.Cursos
{
    public class ArmazedorDeCursoService
    {
        private readonly ICursoRepository _cursoRepository;

        public ArmazedorDeCursoService(ICursoRepository cursoRepository)
        {
            _cursoRepository = cursoRepository;
        }

        public void Armazenar(CursoDto cursoDto)
        {
            var cursoJaSalvo = _cursoRepository.ObterPeloNome(cursoDto.Nome);

            ValidadorDeRegra.Novo()
                .Quando(cursoJaSalvo is not null, "Nome do curso já consta no banco de dados")
                .Quando(!Enum.TryParse(cursoDto.PublicoAlvo, out PublicoAlvoEnum publicoAlto), "Publico Alvo Inválido")
                .DispararExcecaoSeExistir();

            var curso = new Curso(cursoDto.Nome, cursoDto.CargaHoraria,publicoAlto, cursoDto.Valor, cursoDto.Descricao);

            _cursoRepository.Adicionar(curso);
        }

    }
}
