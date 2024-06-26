using CursoOnline.Domain.Base;

namespace CursoOnline.Domain
{
    public class Curso
    {
        public string Nome { get; private set; }
        public double CargaHoraria { get; private set; }
        public PublicoAlvoEnum PublicoAlvo { get; private set; }
        public double Valor { get; private set; }
        public string Descricao { get; private set; }

        public Curso(string nome, double cargaHoraria, PublicoAlvoEnum publicoAlvo, double valor, string descricao)
        {
            ValidadorDeRegra.Novo()
                .Quando(string.IsNullOrEmpty(nome), "Nome inválido!")
                .Quando(cargaHoraria < 1, "Carga horária inválida!")
                .Quando(valor < 100, "Valor inválido!")
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
               .Quando(string.IsNullOrEmpty(nomeEsperado), "Nome inválido!")
                            .DispararExcecaoSeExistir();

            Nome = nomeEsperado;
        }

        public void AlterarCargaHoraria(double cargaHorariaEsperada)
        {
            ValidadorDeRegra.Novo()
              .Quando(cargaHorariaEsperada < 1, "Carga horária inválida!")
              .DispararExcecaoSeExistir();

            CargaHoraria = cargaHorariaEsperada;
        }

        public void AlterarValor(double valorEsperada)
        {
            ValidadorDeRegra.Novo()
              .Quando(valorEsperada < 100, "Valor inválido!")
              .DispararExcecaoSeExistir();

            Valor = valorEsperada;
        }
    }
}
