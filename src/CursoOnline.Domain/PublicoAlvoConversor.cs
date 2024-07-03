using CursoOnline.Domain.Base;
using CursoOnline.Domain.Interfaces;

namespace CursoOnline.Domain
{
    public class PublicoAlvoConversor : IPublicoAlvoConversor
    {
        public PublicoAlvoEnum Converter(string publicoAlvo)
        {
            ValidadorDeRegra.Novo()
               .Quando(!Enum.TryParse(publicoAlvo, out PublicoAlvoEnum publicoAlto), MensagensValidacaoDeDominio.PublicoAlvoInvalido)
               .DispararExcecaoSeExistir();

            return publicoAlto;
        }
    }
}
