using CursoOnline.Domain.Test.Builders;
using CursoOnline.Domain;
using ExpectedObjects;
using CursoOnline.Domain.Base;
using CursoOnline.Domain.Test.Utils;

namespace CursoOnline.Domain.Test.Matriculas
{
    public class MatriculaTest
    {
        [Fact]
        public void DeveCriarMatricula()
        {
            var matriculaEsperada = new
            {
                Aluno = AlunoBuilder.Novo().Build(),
                Curso = CursoBuilder.Novo().Build(),
                ValorMatricula = 1000.0M
            };

            var matricula = new Matricula(matriculaEsperada.Aluno, matriculaEsperada.Curso, matriculaEsperada.ValorMatricula);

            matriculaEsperada.ToExpectedObject().ShouldMatch(matricula);
        }
        [Fact]
        public void NaoDeveCriarMatriculaSemAluno()
        {
            Aluno aluno = null;

            Assert.Throws<ExecaoDeDominio>(() => MatriculaBuilder.Novo().ComAluno(aluno).Build())
                .ComMensagem(MensagensValidacaoDeDominio.MatriculaSemAluno);
        }

        public void NaoDeveCriarMatriculaSemCurso()
        {
            Curso curso = null;

            Assert.Throws<ExecaoDeDominio>(() => MatriculaBuilder.Novo().ComCurso(curso).Build())
                .ComMensagem(MensagensValidacaoDeDominio.MatriculaSemCurso);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(0.1)]
        [InlineData(-100)]
        public void NaoDeveCriarMatriculaComValorDeMatriculaInvalido(decimal valor)
        {
            Assert.Throws<ExecaoDeDominio>(() => MatriculaBuilder.Novo().ComValor(valor).Build())
                .ComMensagem(MensagensValidacaoDeDominio.ValorMatriculaInvalido);

        }

        public void NaoDeveCriarMatriculaComValorMaiorQueValorDoCurso()
        {

            var aluno = AlunoBuilder.Novo().Build();
            var curso = CursoBuilder.Novo().ComValor(50.0).Build();
            var valorMaior = 200.0M;

            Assert.Throws<ExecaoDeDominio>(() => MatriculaBuilder.Novo().ComCurso(curso).ComValor(valorMaior))
                .ComMensagem(MensagensValidacaoDeDominio.ValorMatriculaMaiorQueValorCurso);
        }

        public void NaoDeveCriarMatriculaComAlunoDePublicoAlvoDiferenteDoPublicoAlvoDoCurso()
        {
            var aluno = AlunoBuilder.Novo().Build();
            var curso = CursoBuilder.Novo().Build();
        }
    }
}
