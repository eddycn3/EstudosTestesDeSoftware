using CursoOnline.Domain.Base;
using CursoOnline.Domain.Validacoes;

namespace CursoOnline.Domain
{
    public partial class Aluno : Entidade
    {
        public string Nome { get; private set; }
        public string Email { get; private set; }
        public string Cpf { get; private set; }
        public PublicoAlvoEnum PublicoAlvo { get; private set; }

        public Aluno(string nome, string email, string cpf, PublicoAlvoEnum publicoAlvo)
        {
            ValidadorDeRegra.Novo()
                .Quando(string.IsNullOrEmpty(nome), MensagensValidacaoDeDominio.NomeInvalido)
                .Quando(!email.EmailEhValido(), MensagensValidacaoDeDominio.EmailInvalido)
                .Quando(!cpf.CepfEhValido() , MensagensValidacaoDeDominio.CpfInvalido)
                .DispararExcecaoSeExistir();

            Nome = nome;
            Email = email;
            Cpf = cpf;
            PublicoAlvo = publicoAlvo;
        }

        public void AlterarCpf(string cpf)
        {
            Cpf = cpf;
        }

        public void AlterarNome(string nome)
        {
            Nome = nome;
        }
    }



}

