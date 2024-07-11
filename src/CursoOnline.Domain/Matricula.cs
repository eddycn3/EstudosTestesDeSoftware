using CursoOnline.Domain.Base;

namespace CursoOnline.Domain
{
    public class Matricula : Entidade
    {
        public Aluno Aluno { get; private set; }
        public Curso Curso { get; private set; }
        public decimal ValorMatricula { get; private set; }
        public double NotaAluno { get; private set; }
        public bool CursoConcluido { get; private set; }
        public bool Cancelada { get; private set; }

        public Matricula(Aluno aluno, Curso curso, decimal valorMatricula)
        {
            ValidadorDeRegra.Novo()
                .Quando(aluno is null, MensagensValidacaoDeDominio.MatriculaSemAluno)
                .Quando(curso is null, MensagensValidacaoDeDominio.MatriculaSemCurso)
                .Quando(valorMatricula < 1.0M, MensagensValidacaoDeDominio.ValorMatriculaInvalido)
                .Quando(valorMatricula > curso.Valor, MensagensValidacaoDeDominio.ValorMatriculaMaiorQueValorCurso)
                .Quando(aluno is not null && curso is not null && aluno.PublicoAlvo != curso.PublicoAlvo, MensagensValidacaoDeDominio.PublicoAlvoDiferente)
                .DispararExcecaoSeExistir();

            Aluno = aluno;
            Curso = curso;
            ValorMatricula = valorMatricula;
        }

        public void InformarNota(double notaAluno)
        {
            ValidadorDeRegra.Novo()
                .Quando(notaAluno < 0 || notaAluno > 10, MensagensValidacaoDeDominio.NotaDeAlunoInvalida)
                .Quando(Cancelada, MensagensValidacaoDeDominio.MatriculaCancelada)
                .DispararExcecaoSeExistir();

            NotaAluno = notaAluno;
            CursoConcluido = true;
        }

        public void Cancelar()
        {
            ValidadorDeRegra.Novo()
               .Quando(CursoConcluido, MensagensValidacaoDeDominio.MatriculaConcluida)
               .DispararExcecaoSeExistir();

            Cancelada = true;
        }
    }
}
