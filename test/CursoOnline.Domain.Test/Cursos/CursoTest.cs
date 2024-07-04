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
        private readonly Faker _faker;
        public CursoTest(ITestOutputHelper output)
        {
            _output = output;
            _output.WriteLine("Construtor sendo executado");
            _faker = new Faker();
        }

        [Fact]
        public void DeveCriarCurso()
        {
            var cursoEsperado = new
            {
                Nome = "Curso de pilotagem de Moto",
                CargaHoraria = (double)80,
                PublicoAlvo = PublicoAlvoEnum.Estudante,
                Valor = 950.0M
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
                .ComMensagem(MensagensValidacaoDeDominio.NomeInvalido);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-200)]
        public void NaoDeveCursoTerCargaHorariaInvalida(double cargaHorariaInvalida)
        {
            Assert.Throws<ExecaoDeDominio>(() => CursoBuilder.Novo().ComCargaHoraria(cargaHorariaInvalida).Build())
                .ComMensagem(MensagensValidacaoDeDominio.CargaHorariaInvalida);
        }

        [Theory]
       [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-200)]
        [InlineData(50)]
        public void NaoDeveCursoTerValorInvalido(decimal valorInvalido)
        {
            Assert.Throws<ExecaoDeDominio>(() => CursoBuilder.Novo().ComValor(valorInvalido).Build())
                .ComMensagem(MensagensValidacaoDeDominio.ValorInvalido);
        }

        [Fact]
        public void DeveAlterarNome()
        {
            var nomeEsperado = _faker.Person.FullName;
            var curso = CursoBuilder.Novo().Build();
            curso.AlterarNome(nomeEsperado);

            Assert.Equal(nomeEsperado, curso.Nome);
        }


        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void NaoDeveAlterarCursoComUmNomeInvalido(string nomeInvalido)
        {
            var curso = CursoBuilder.Novo().Build();

            Assert.Throws<ExecaoDeDominio>(() => curso.AlterarNome(nomeInvalido))
                .ComMensagem(MensagensValidacaoDeDominio.NomeInvalido);
        }

        [Fact]
        public void DeveAlterarCargaHoraria()
        {
            var cargaHorariaEsperada = _faker.Random.Double(1,360);
            var curso = CursoBuilder.Novo().Build();

            curso.AlterarCargaHoraria(cargaHorariaEsperada);

            Assert.Equal(cargaHorariaEsperada, curso.CargaHoraria);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-200)]
        public void NaoDeveAlterarCursoComCargaHorariaInvalida(double cargaHorariaInvalida)
        {
            var curso = CursoBuilder.Novo().Build();

            Assert.Throws<ExecaoDeDominio>(() => curso.AlterarCargaHoraria(cargaHorariaInvalida))
                .ComMensagem(MensagensValidacaoDeDominio.CargaHorariaInvalida);
        }

        [Fact]
        public void DeveAlterarValor()
        {
            var valorEsperada = _faker.Random.Decimal(100, 2000);
            var curso = CursoBuilder.Novo().Build();

            curso.AlterarValor(valorEsperada);

            Assert.Equal(valorEsperada, curso.Valor);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-200)]
        [InlineData(50)]
        public void NaoDeveAlterarCursoComValorInvalido(decimal cargaHorariaInvalida)
        {
            var curso = CursoBuilder.Novo().Build();

            Assert.Throws<ExecaoDeDominio>(() => curso.AlterarValor(cargaHorariaInvalida))
                .ComMensagem(MensagensValidacaoDeDominio.ValorInvalido);
        }

        public void Dispose()
        {
            _output.WriteLine("Dispose sendo executado");
        }
    }
}