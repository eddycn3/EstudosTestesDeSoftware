using CursoOnline.Domain.Base;
using CursoOnline.Domain.Test.Utils;

namespace CursoOnline.Domain.Test.PublicoAlvo
{
    public class PublicoAlvoConversorTest
    {
        [Theory]
        [InlineData(PublicoAlvoEnum.Empreendedor, "Empreendedor")]
        [InlineData(PublicoAlvoEnum.Estudante, "Estudante")]
        [InlineData(PublicoAlvoEnum.Universitario, "Universitario")]
        [InlineData(PublicoAlvoEnum.Empregado, "Empregado")]
        public void DeveConverterPublicoAlvoValido(PublicoAlvoEnum publicoAlvo, string publicoAlvoString)
        {
            var publicoAlvoConvertido = (new PublicoAlvoConversor()).Converter(publicoAlvoString);

            Assert.Equal(publicoAlvo, publicoAlvoConvertido);
        }

        [Fact]
        public void NaoDeveConverterPublicoAlvoInvalido()
        {
            var publicoAlvoInvalido = "Invalido";

            Assert.Throws<ExecaoDeDominio>(() => (new PublicoAlvoConversor()).Converter(publicoAlvoInvalido))
                .ComMensagem(MensagensValidacaoDeDominio.PublicoAlvoInvalido);
        }
    }
}
