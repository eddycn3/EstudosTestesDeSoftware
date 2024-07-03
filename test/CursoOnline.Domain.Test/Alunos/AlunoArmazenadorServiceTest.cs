using Bogus;
using Bogus.Extensions.Brazil;
using CursoOnline.Application.Dtos;
using CursoOnline.Application.Services;
using CursoOnline.Domain.Base;
using CursoOnline.Domain.Interfaces;
using CursoOnline.Domain.Test.Builders;
using CursoOnline.Domain.Test.Utils;
using Moq;

namespace CursoOnline.Domain.Test.Alunos
{
    public class AlunoArmazenadorServiceTest
    {
        public AlunoDto _alunoDto;
        public ArmazenadorDeAlunoService _armazenadorDeAlunoService;
        public Mock<IAlunoRepository> _mockAlunoRepository;
        private readonly Mock<IPublicoAlvoConversor> _publicoAlvoConversorMock;

        public AlunoArmazenadorServiceTest()
        {
            var faker = new Faker();
            _alunoDto = new AlunoDto
            {
                Nome = faker.Person.FullName,
                Cpf = faker.Person.Cpf(),
                Email = faker.Person.Email,
                PublicoAlvo = "Estudante"
            };

            _mockAlunoRepository = new Mock<IAlunoRepository>();
            _publicoAlvoConversorMock = new Mock<IPublicoAlvoConversor>();
            _armazenadorDeAlunoService = new ArmazenadorDeAlunoService(_mockAlunoRepository.Object, _publicoAlvoConversorMock.Object);

        }


        [Fact]
        public void DeveAdicionarAluno()
        {
            _armazenadorDeAlunoService.Armazenar(_alunoDto);

            _mockAlunoRepository.Verify(c => c.Adicionar(
                It.Is<Aluno>(
                    a => a.Nome == _alunoDto.Nome &&
                    a.Cpf == _alunoDto.Cpf
                )));
        }

        [Fact]
        public void NaoDeveAdicionarAlunoComMesmoCpfDeOutroJaSalvo()
        {
            var alunoJaSalvo = AlunoBuilder.Novo().ComId(1).ComCpf(_alunoDto.Cpf).Build();

            _mockAlunoRepository.Setup(x=>x.ObterPorId(_alunoDto.Id)).Returns(alunoJaSalvo);

            Assert.Throws<ExecaoDeDominio>(() => _armazenadorDeAlunoService.Armazenar(_alunoDto))
                .ComMensagem(MensagensValidacaoDeDominio.AlunoJaExistente);
        }

        [Fact]
        public void DeveAlterarAluno()
        {
            _alunoDto.Id = 1;
            var aluno = AlunoBuilder.Novo().Build();

            _mockAlunoRepository.Setup(r => r.ObterPorId(_alunoDto.Id)).Returns(aluno);

            _armazenadorDeAlunoService.Armazenar(_alunoDto);

            Assert.Equal(_alunoDto.Nome, aluno.Nome);
            Assert.Equal(_alunoDto.Cpf, aluno.Cpf);
        }


        [Fact]
        public void NaoDeveAdicionarNoRepositorioSeAlunoJaExiste()
        {
            _alunoDto.Id = 1;
            var aluno = AlunoBuilder.Novo().Build();

            _mockAlunoRepository.Setup(r => r.ObterPorId(_alunoDto.Id)).Returns(aluno);

            _armazenadorDeAlunoService.Armazenar(_alunoDto);

            _mockAlunoRepository.Verify(v => v.Adicionar(It.IsAny<Aluno>()), Times.Never);
        }
    }
}
