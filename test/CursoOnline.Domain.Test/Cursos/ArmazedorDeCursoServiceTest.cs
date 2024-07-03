using Bogus;
using CursoOnline.Application.Dtos;
using CursoOnline.Application.Services;
using CursoOnline.Domain.Base;
using CursoOnline.Domain.Interfaces;
using CursoOnline.Domain.Test.Builders;
using CursoOnline.Domain.Test.Utils;
using Moq;


namespace CursoOnline.Domain.Test.Cursos
{
    public  class ArmazedorDeCursoServiceTest
    {
        public readonly CursoDto _cursoDto;
        private readonly ArmazedorDeCursoService _armazenadorDeCursos;
        private Mock<ICursoRepository> _cursoRepositoryMock;
        private readonly Mock<IPublicoAlvoConversor> _publicoAlvoConversorMock;

        public ArmazedorDeCursoServiceTest()
        {
            var fake = new Faker();
            _cursoDto = new CursoDto
            {
                Nome = fake.Random.Words(),
                Descricao = fake.Lorem.Paragraphs(),
                CargaHoraria = fake.Random.Double(50, 1000),
                PublicoAlvo = "Estudante",
                Valor = fake.Random.Double(1000,2000),
            };

            _cursoRepositoryMock = new Mock<ICursoRepository>();
            _publicoAlvoConversorMock = new Mock<IPublicoAlvoConversor>();
            _armazenadorDeCursos = new ArmazedorDeCursoService(_cursoRepositoryMock.Object, _publicoAlvoConversorMock.Object);
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
        public void NaoDeveAdicionarCursoComMesmoNomeDeOutroJaSalvo()
        {
            var _cursoJaSalvo = CursoBuilder.Novo().ComId(1).ComNome(_cursoDto.Nome).Build();
          
            _cursoRepositoryMock.Setup(r=> r.ObterPeloNome(_cursoDto.Nome)).Returns(_cursoJaSalvo);

            Assert.Throws<ExecaoDeDominio>(() => _armazenadorDeCursos.Armazenar(_cursoDto))
              .ComMensagem(MensagensValidacaoDeDominio.CursoJaExiste);
        }

        [Fact]
        public void DeveAlterarCurso()
        {
            _cursoDto.Id = 2332;
            var curso = CursoBuilder.Novo().Build();

            _cursoRepositoryMock.Setup(r => r.ObterPeloId(_cursoDto.Id)).Returns(curso);

            _armazenadorDeCursos.Armazenar(_cursoDto);

            Assert.Equal(_cursoDto.Nome, curso.Nome);
            Assert.Equal(_cursoDto.Valor, curso.Valor);
            Assert.Equal(_cursoDto.CargaHoraria, curso.CargaHoraria);
          
        }

        [Fact]
        public void NaoDeveAdicionarNoRepositorioSeCursoJaExiste()
        {

            _cursoDto.Id = 2332;
            var curso = CursoBuilder.Novo().Build();

            _cursoRepositoryMock.Setup(r => r.ObterPeloId(_cursoDto.Id)).Returns(curso);

            _armazenadorDeCursos.Armazenar(_cursoDto);

            _cursoRepositoryMock.Verify(v => v.Adicionar(It.IsAny<Curso>()), Times.Never);

        }

    }
}
