
using CursoOnline.Domain;

namespace CursoOnline.Domain.Test.Builders
{
    public class AlunoBuilder
    {
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
            return new Aluno(_nome, _email, _cpf, _publicoAlvo);
        }

        public AlunoBuilder ComNome(string nome)
        {
            _nome = nome;
            return this;
        }

        internal AlunoBuilder ComCpf(string cpf)
        {
            _cpf= cpf;
            return this;
        }

        public AlunoBuilder ComEmail(string email)
        {
            _email = email;
            return this;
        }
    }

}

