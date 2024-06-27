using Bogus;
using CursoOnline.Domain.Base;
using CursoOnline.Domain.Test.Builders;
using CursoOnline.Domain.Test.Utils;
using ExpectedObjects;

namespace CursoOnline.Domain.Test.Alunos
{
    public class AlunoTest
    {
        public readonly Faker _faker;
        public AlunoTest()
        {
            _faker = new Faker();
        }

        [Fact]
        public void DeveCriarAluno()
        {
            var alunoEsperado = new
            {
                Nome = "Eduardo C Neto",
                Cpf = "032.591.070-70",
                Email = "teste@teste.com.br",
                PublicoAlvo = PublicoAlvoEnum.Estudante,
            };

            var aluno = AlunoBuilder.Novo().Build();
            alunoEsperado.ToExpectedObject().ShouldMatch(aluno);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void NaoDeveAlunoTerNomeInvalido(string nomeInvalido)
        {

            Assert.Throws<ExecaoDeDominio>(() => AlunoBuilder.Novo().ComNome(nomeInvalido).Build())
                .ComMensagem(MensagensValidacaoDeDominio.NomeInvalido);
        }


        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("23455232232345455")]
        [InlineData("324")]
        public void NaoDeveAlunoTerCpfInvalido(string cpfInvalido)
        {
            Assert.Throws<ExecaoDeDominio>(()=> AlunoBuilder.Novo().ComCpf(cpfInvalido).Build())
                .ComMensagem(MensagensValidacaoDeDominio.CpfInvalido);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("email@email")]
        [InlineData("email_12@")]
        public void NaoDeveAlunoTerEmailInvalido(string emailInvalido)
        {
            Assert.Throws<ExecaoDeDominio>(() => AlunoBuilder.Novo().ComEmail(emailInvalido).Build())
                .ComMensagem(MensagensValidacaoDeDominio.EmailInvalido);
        }
    }
}
