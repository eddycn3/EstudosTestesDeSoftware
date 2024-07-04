
using CursoOnline.Domain;

namespace CursoOnline.Domain.Test.Builders
{
    public class AlunoBuilder
    {
        private int _id;
        private string _nome = "Eduardo C Neto";
        private string _cpf = "032.591.070-70";
        private string _email = "teste@teste.com.br";
        private PublicoAlvoEnum _publicoAlvo = PublicoAlvoEnum.Estudante;

        public static AlunoBuilder Novo()
        {
            return new AlunoBuilder();
        }

        public Aluno Build()
        {
            var aluno = new Aluno(_nome, _email, _cpf, _publicoAlvo);

            if (_id > 0)
            {
                var propertyInfo = aluno.GetType().GetProperty("Id");
                propertyInfo?.SetValue(aluno, Convert.ChangeType(_id, propertyInfo.PropertyType), null);
            }

            return aluno;
        }

        public AlunoBuilder ComNome(string nome)
        {
            _nome = nome;
            return this;
        }

        public AlunoBuilder ComEmail(string email)
        {
            _email = email;
            return this;
        }

        public AlunoBuilder ComId(int id)
        {
            _id = id;
            return this;
        }

        public AlunoBuilder ComCpf(string cpf) 
        { 
            _cpf = cpf;
            return this;
        }

        public AlunoBuilder ComPublicoAlvo(PublicoAlvoEnum publicoAlvo)
        {
            _publicoAlvo = publicoAlvo;
            return this;
        }
    }

}

