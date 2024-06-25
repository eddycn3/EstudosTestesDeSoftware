using Bogus;

namespace CursoOnline.Domain.Test.Builders
{
    public class CursoBuilder
    {
        private string _nome = "Curso de pilotagem de Moto";
        private double _cargaHoraria = (double)80;
        private PublicoAlvoEnum _publicoAlvo = PublicoAlvoEnum.Estudante;
        private double _valor = (double)950;
        private string _descricao = "Curso para pilotagem de motos";

        public static CursoBuilder Novo()
        {
            return new CursoBuilder();
        }

        public Curso Build()
        {
            return new Curso(_nome, _cargaHoraria, _publicoAlvo, _valor, _descricao);
        }

        public CursoBuilder ComNome(string nome)
        {
            _nome = nome;
            return this;
        }

        public CursoBuilder ComValor(double valor)
        {
            _valor = valor;
            return this;
        }

        public CursoBuilder ComCargaHoraria(double cargaHoraria)
        {
            _cargaHoraria = cargaHoraria;
            return this;
        }

        public CursoBuilder ComDescricao(string descricao)
        {
            _descricao = descricao;
            return this;
        }
    }
}
