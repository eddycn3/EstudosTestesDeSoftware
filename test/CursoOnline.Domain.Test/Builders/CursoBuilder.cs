using Bogus;

namespace CursoOnline.Domain.Test.Builders
{
    public class CursoBuilder
    {
        private int _id;
        private string _nome = "Curso de pilotagem de Moto";
        private double _cargaHoraria = (double)80;
        private PublicoAlvoEnum _publicoAlvo = PublicoAlvoEnum.Estudante;
        private decimal _valor = 950.0M;
        private string _descricao = "Curso para pilotagem de motos";

        public static CursoBuilder Novo()
        {
            return new CursoBuilder();
        }

        public Curso Build()
        {

            var curso = new Curso(_nome, _cargaHoraria, _publicoAlvo, _valor, _descricao);
          
            if (_id > 0)
            {
                var propertyInfo = curso.GetType().GetProperty("Id");
                propertyInfo?.SetValue(curso,Convert.ChangeType(_id,propertyInfo.PropertyType),null);
            }

            return curso;

        }

        public CursoBuilder ComNome(string nome)
        {
            _nome = nome;
            return this;
        }

        public CursoBuilder ComValor(decimal valor)
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

        public CursoBuilder ComId(int id)
        {
           _id =  id;
            return this;
        }
    }
}
