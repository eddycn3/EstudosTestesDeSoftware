namespace CursoOnline.Domain.Base
{
    public class MensagensValidacaoDeDominio
    {
        public const string NomeInvalido = "Nome inválido!";
        public const string CargaHorariaInvalida = "Carga horária inválida!";
        public const string ValorInvalido = "Valor inválido!";
        public const string CursoJaExiste = "Curso já consta no banco de dados";
        public const string PublicoAlvoInvalido = "Publico Alvo Inválido";
        public const string CpfInvalido = "Cpf Inválido";
        public const string EmailInvalido = "Email Inválido";
        public const string AlunoJaExistente = "Aluno já consta no banco de dados";
        public const string ValorMatriculaInvalido = "Valor da matrícula é inválido";
        public const string MatriculaSemAluno = "A matricula deve ter um aluno";
        public const string MatriculaSemCurso = "A matricula deve ter um curso";
        public const string ValorMatriculaMaiorQueValorCurso = "Valor da matricula não pode ser maior que o valor do Curso";
        public const string PublicoAlvoDiferente = "Publico alvo diferente";
        public const string AlunoNaoEncontrado = "O aluno não foi encontrado";
        public const string CursoNaoEncontrado = "O curso não foi encontrado";
        public const string NotaDeAlunoInvalida = "Nota informada é inválida";
        public const string MatriculaNaoLocalizada = "Matricula não localizada";
        public const string MatriculaCancelada = "Ação não permitida por matricula estar cancelada";
        public const string MatriculaConcluida = "Ação não permitida por matricula estar concluida";
    }
}
