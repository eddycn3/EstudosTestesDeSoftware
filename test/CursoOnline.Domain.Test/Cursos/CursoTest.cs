using Bogus;
using CursoOnline.Domain.Base;
using CursoOnline.Domain.Test.Builders;
using CursoOnline.Domain.Test.Utils;
using ExpectedObjects;
using Xunit.Abstractions;

namespace CursoOnline.Domain.Test.Cursos
{
    public class CursoTest : IDisposable
    {
        private readonly ITestOutputHelper _output;
        public CursoTest(ITestOutputHelper output)
        {
            _output = output;
            _output.WriteLine("Construtor sendo executado");
        }

        [Fact]
        public void DeveCriarCurso()
        {
            var cursoEsperado = new
            {
                Nome = "Curso de pilotagem de Moto",
                CargaHoraria = (double)80,
                PublicoAlvo = PublicoAlvoEnum.Estudante,
                Valor = (double)950
            };

            var curso = CursoBuilder.Novo().Build();

            cursoEsperado.ToExpectedObject().ShouldMatch(curso);

        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void NaoDeveCursoTerUmNomeInvalido(string nomeInvalido)
        {

            Assert.Throws<ExecaoDeDominio>(() =>
            CursoBuilder.Novo().ComNome(nomeInvalido).Build()
            )
                .ComMensagem("Nome inválido!");
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-200)]
        public void NaoDeveCursoTerCargaHorariaMenorQue1(double cargaHorariaInvalida)
        {
            var cursoEsperado = new
            {
                Nome = "Curso de pilotagem de Moto",
                CargaHoraria = (double)cargaHorariaInvalida,
                PublicoAlvo = PublicoAlvoEnum.Estudante,
                Valor = (double)950
            };

            Assert.Throws<ExecaoDeDominio>(() => CursoBuilder.Novo().ComCargaHoraria(cargaHorariaInvalida).Build())
                .ComMensagem("Carga horária inválida!");
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-200)]
        public void NaoDeveCursoTerValorMenorQue1(double valorInvalido)
        {
            var cursoEsperado = new
            {
                Nome = "Curso de pilotagem de Moto",
                CargaHoraria = (double)80,
                PublicoAlvo = PublicoAlvoEnum.Estudante,
                Valor = valorInvalido
            };

            Assert.Throws<ExecaoDeDominio>(() => CursoBuilder.Novo().ComValor(valorInvalido).Build())
                .ComMensagem("Valor inválido!");

        }

        public void Dispose()
        {
            _output.WriteLine("Dispose sendo executado");
        }
    }
}