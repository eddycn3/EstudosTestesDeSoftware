using CursoOnline.Domain.Base;

namespace CursoOnline.Domain
{
    public class Curso
    {
        public Curso(string nome, double cargaHoraria, PublicoAlvoEnum publicoAlvo, double valor, string descricao)
        {
            ValidadorDeRegra.Novo()
                .Quando(string.IsNullOrEmpty(nome), "Nome inválido!")
                .Quando(cargaHoraria < 1, "Carga horária inválida!")
                .Quando(valor < 1, "Valor inválido!")
                .DispararExcecaoSeExistir();
 
            Nome = nome;
            CargaHoraria = cargaHoraria;
            PublicoAlvo = publicoAlvo;
            Valor = valor;
            Descricao = descricao;
        }

        public string Nome { get; private set; }
        public double CargaHoraria { get; private set; }
        public PublicoAlvoEnum PublicoAlvo  { get; private set; }
        public double Valor { get; private set; }
        public string Descricao { get; private set; }

    }
}
