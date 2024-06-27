using CursoOnline.Domain.Base;

namespace CursoOnline.Domain
{
    public class Curso : Entidade
    {

        public string Nome { get; private set; }
        public double CargaHoraria { get; private set; }
        public PublicoAlvoEnum PublicoAlvo { get; private set; }
        public double Valor { get; private set; }
        public string Descricao { get; private set; }

        public Curso(string nome, double cargaHoraria, PublicoAlvoEnum publicoAlvo, double valor, string descricao)
        {
            ValidadorDeRegra.Novo()
                .Quando(string.IsNullOrEmpty(nome), MensagensValidacaoDeDominio.NomeInvalido)
                .Quando(cargaHoraria < 1, MensagensValidacaoDeDominio.CargaHorariaInvalida)
                .Quando(valor < 100, MensagensValidacaoDeDominio.ValorInvalido)
                .DispararExcecaoSeExistir();
 
            Nome = nome;
            CargaHoraria = cargaHoraria;
            PublicoAlvo = publicoAlvo;
            Valor = valor;
            Descricao = descricao;
        }


        public void AlterarNome(string nomeEsperado)
        {
            ValidadorDeRegra.Novo()
               .Quando(string.IsNullOrEmpty(nomeEsperado), MensagensValidacaoDeDominio.NomeInvalido)
                            .DispararExcecaoSeExistir();

            Nome = nomeEsperado;
        }

        public void AlterarCargaHoraria(double cargaHorariaEsperada)
        {
            ValidadorDeRegra.Novo()
              .Quando(cargaHorariaEsperada < 1, MensagensValidacaoDeDominio.CargaHorariaInvalida)
              .DispararExcecaoSeExistir();

            CargaHoraria = cargaHorariaEsperada;
        }

        public void AlterarValor(double valorEsperada)
        {
            ValidadorDeRegra.Novo()
              .Quando(valorEsperada < 100, MensagensValidacaoDeDominio.ValorInvalido)
              .DispararExcecaoSeExistir();

            Valor = valorEsperada;
        }
    }
}
