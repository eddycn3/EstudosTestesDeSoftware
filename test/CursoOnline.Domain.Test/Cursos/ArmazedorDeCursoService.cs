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
            if (cursoJaSalvo is not null)
                throw new ArgumentException("Nome do curso já consta no banco de dados");

            if (!Enum.TryParse(cursoDto.PublicoAlvo, out PublicoAlvoEnum publicoAlto))
                throw new ArgumentException("Publico Alvo Inválido");

            var curso = new Curso(cursoDto.Nome, cursoDto.CargaHoraria,publicoAlto, cursoDto.Valor, cursoDto.Descricao);

            _cursoRepository.Adicionar(curso);
        }

    }
}
