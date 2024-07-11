using CursoOnline.Domain.Test.Builders;
using CursoOnline.Domain;
using ExpectedObjects;
using CursoOnline.Domain.Base;
using CursoOnline.Domain.Test.Utils;
using CursoOnline.Application.Dtos;
using CursoOnline.Application.Services;
using CursoOnline.Domain.Interfaces;
using Moq;

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
                ValorMatricula = 950.0M
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

        [Fact]
        public void NaoDeveCriarMatriculaComValorMaiorQueValorDoCurso()
        {

            var aluno = AlunoBuilder.Novo().Build();
            var curso = CursoBuilder.Novo().ComValor(100.0M).Build();
            var valorMaior = 200.0M;

            Assert.Throws<ExecaoDeDominio>(() => MatriculaBuilder.Novo().ComCurso(curso).ComValor(valorMaior).Build())
                .ComMensagem(MensagensValidacaoDeDominio.ValorMatriculaMaiorQueValorCurso);
        }


        [Fact]
        public void NaoDeveCriarMatriculaDeAlunoECursoComPublicoAlvoDiferentes()
        {
            var curso = CursoBuilder.Novo().ComId(1).ComPublicoAlvo(PublicoAlvoEnum.Estudante).Build();

            var aluno = AlunoBuilder.Novo().ComId(1).ComPublicoAlvo(PublicoAlvoEnum.Universitario).Build();
        
            Assert.Throws<ExecaoDeDominio>(() => MatriculaBuilder.Novo().ComCurso(curso).ComAluno(aluno).Build())
                .ComMensagem(MensagensValidacaoDeDominio.PublicoAlvoDiferente);

        }

        [Fact]
        public void DeveInformarNotaDoAlunoParaMatricula()
        {
            double notaAluno = 9.0;

            var matricula  = MatriculaBuilder.Novo().ComValor(950).Build();

            matricula.InformarNota(notaAluno);

            Assert.Equal(notaAluno, matricula.NotaAluno);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(11)]
        public void NaoDeveInformarNotaDeAlunoInvalida(double notaInvalida)
        {
            var matricula = MatriculaBuilder.Novo().ComValor(950.0M).Build();
            Assert.Throws<ExecaoDeDominio>(() => matricula.InformarNota(notaInvalida))
                 .ComMensagem(MensagensValidacaoDeDominio.NotaDeAlunoInvalida);
        }

        [Fact]
        public void DeveIndicarQueCursoFoiConcluido()
        {
            const double notaAluno = 9.5;
            var matricula = MatriculaBuilder.Novo().Build();

            matricula.InformarNota(notaAluno);

            Assert.True(matricula.CursoConcluido);
        }

        [Fact]
        public void DeveCancelarMatricula()
        {
            var matricula = MatriculaBuilder.Novo().Build();

            matricula.Cancelar();

            Assert.True(matricula.Cancelada);
        }


        [Fact]
        public void NaoDeveInformarNotaQuandoMatriculaCancelada()
        {
            var notaAluna = 3.0;
            var matricula = MatriculaBuilder.Novo().Build();

            matricula.Cancelar();

            Assert.Throws<ExecaoDeDominio>(() => matricula.InformarNota(notaAluna))
                .ComMensagem(MensagensValidacaoDeDominio.MatriculaCancelada);
        }


        [Fact]
        public void NaoDeveCancelarQuandoMatriculaEstiverConcluida()
        {
            var matricula = MatriculaBuilder.Novo().ComConcluido(true).Build();

            Assert.Throws<ExecaoDeDominio>(() => matricula.Cancelar())
               .ComMensagem(MensagensValidacaoDeDominio.MatriculaConcluida);
        }
      

    }
}
