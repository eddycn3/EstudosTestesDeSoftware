using CursoOnline.Domain.Base;

namespace CursoOnline.Domain
{
    public class Matricula : Entidade
    {
        public Aluno Aluno { get; private set; }
        public Curso Curso { get; private set; }
        public decimal ValorMatricula { get; private set; }

        public Matricula(Aluno aluno, Curso curso, decimal valorMatricula)
        {
            ValidadorDeRegra.Novo()
                .Quando(aluno is null, MensagensValidacaoDeDominio.MatriculaSemAluno)
                .Quando(curso is null, MensagensValidacaoDeDominio.MatriculaSemCurso)
                .Quando(valorMatricula < 1.0M, MensagensValidacaoDeDominio.ValorMatriculaInvalido)
                .Quando(valorMatricula > curso.Valor, MensagensValidacaoDeDominio.ValorMatriculaMaiorQueValorCurso)
                .DispararExcecaoSeExistir();

            Aluno = aluno;
            Curso = curso;
            ValorMatricula = valorMatricula;
        }
        
    }
}
