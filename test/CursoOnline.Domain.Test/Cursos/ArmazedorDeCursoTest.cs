using Bogus;
using CursoOnline.Domain.Test.Builders;
using CursoOnline.Domain.Test.Utils;
using Moq;


namespace CursoOnline.Domain.Test.Cursos
{
    public  class ArmazedorDeCursoTest
    {
        public readonly CursoDto _cursoDto;
        private readonly ArmazedorDeCursoService _armazenadorDeCursos;
        private Mock<ICursoRepository> _cursoRepositoryMock;

        public ArmazedorDeCursoTest()
        {
            var fake = new Faker();
            _cursoDto = new CursoDto
            {
                Nome = fake.Random.Words(),
                Descricao = fake.Lorem.Paragraphs(),
                CargaHoraria = fake.Random.Double(1,100),
                PublicoAlvo = "Estudante",
                Valor = fake.Random.Double(200,5000),
            };

            _cursoRepositoryMock = new Mock<ICursoRepository>();
            _armazenadorDeCursos = new ArmazedorDeCursoService(_cursoRepositoryMock.Object);
        }

        [Fact]
        public void DeveAdicionarCurso()
        {
            _armazenadorDeCursos.Armazenar(_cursoDto);

            _cursoRepositoryMock.Verify(v => v.Adicionar(
                It.Is<Curso>(
                    c => c.Nome == _cursoDto.Nome &&
                    c.Descricao == _cursoDto.Descricao
                    )
                ));
        }

        [Fact]
        public void NaoDeveInformarPublicoAlvoInvalido()
        {
            _cursoDto.PublicoAlvo = "Médico";

            Assert.Throws<ArgumentException>(() => _armazenadorDeCursos.Armazenar(_cursoDto))
                .ComMensagem("Publico Alvo Inválido");
        }

        [Fact]
        public void NaoDeveAdicionarCursoComMesmoNomeDeOutroJaSalvo()
        {
            var _cursoJaSalvo = CursoBuilder.Novo().ComNome(_cursoDto.Nome).Build();
          
            _cursoRepositoryMock.Setup(r=> r.ObterPeloNome(_cursoDto.Nome)).Returns(_cursoJaSalvo);

            Assert.Throws<ArgumentException>(() => _armazenadorDeCursos.Armazenar(_cursoDto))
              .ComMensagem("Nome do curso já consta no banco de dados");
        }

    }
}
