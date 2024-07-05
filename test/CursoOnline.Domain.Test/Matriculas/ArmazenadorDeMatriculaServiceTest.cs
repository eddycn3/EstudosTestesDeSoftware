using CursoOnline.Application.Dtos;
using CursoOnline.Application.Services;
using CursoOnline.Domain.Base;
using CursoOnline.Domain.Interfaces;
using CursoOnline.Domain.Test.Builders;
using CursoOnline.Domain.Test.Utils;
using Moq;

namespace CursoOnline.Domain.Test.Matriculas
{
    public class ArmazenadorDeMatriculaServiceTest
    {
        public readonly Mock<IAlunoRepository> _alunoMockRepositoryMock;
        public readonly Mock<ICursoRepository> _cursoMockRepositoryMock;
        public readonly Mock<IMatriculaRepository> _matriculaRepositoryMock;
        private readonly MatriculaDto _matriculaDto;
        private readonly ArmazenadorDeMatriculaService _armazenadorMatricula;
        private readonly Aluno _aluno;
        private readonly Curso _curso;

        public ArmazenadorDeMatriculaServiceTest()
        {
            _alunoMockRepositoryMock = new Mock<IAlunoRepository>();
            _cursoMockRepositoryMock = new Mock<ICursoRepository>();
            _matriculaRepositoryMock = new Mock<IMatriculaRepository>();

            _aluno = AlunoBuilder.Novo().ComPublicoAlvo(PublicoAlvoEnum.Estudante).ComId(1).Build();
            _alunoMockRepositoryMock.Setup(r => r.ObterPorId(_aluno.Id)).Returns(_aluno);

            _curso = CursoBuilder.Novo().ComPublicoAlvo(PublicoAlvoEnum.Estudante).ComId(2).Build();
            _cursoMockRepositoryMock.Setup(r => r.ObterPeloId(_curso.Id)).Returns(_curso);

            _matriculaDto = new MatriculaDto { AlunoId = _aluno.Id, CursoId = _curso.Id, ValorMatricula = 950.0M };

            _armazenadorMatricula = new ArmazenadorDeMatriculaService(_alunoMockRepositoryMock.Object, _cursoMockRepositoryMock.Object, _matriculaRepositoryMock.Object);
        }

        [Fact]
        public void DeveNotificarQuandoAlunoNaoForEncontrado()
        {
            Aluno alunoInvalido = null;
            _alunoMockRepositoryMock.Setup(r => r.ObterPorId(_matriculaDto.AlunoId)).Returns(alunoInvalido);

            Assert.Throws<ExecaoDeDominio>(() => _armazenadorMatricula.Criar(_matriculaDto))
                .ComMensagem(MensagensValidacaoDeDominio.AlunoNaoEncontrado);
        }


        [Fact]
        public void DeveNotificarQuandoCursoNaoForEncontrado()
        {
            Curso cursoInvalido = null;
            _cursoMockRepositoryMock.Setup(r => r.ObterPeloId(_matriculaDto.CursoId)).Returns(cursoInvalido);

            Assert.Throws<ExecaoDeDominio>(() => _armazenadorMatricula.Criar(_matriculaDto))
                .ComMensagem(MensagensValidacaoDeDominio.CursoNaoEncontrado);
        }

        [Fact]
        public void DeveAdicionarMatricula()
        {
            _armazenadorMatricula.Criar(_matriculaDto);

            _matriculaRepositoryMock.Verify(v => v.Adicionar(It.Is<Matricula>(m => m.Aluno == _aluno && m.Curso == _curso)));
        }
    }
}
