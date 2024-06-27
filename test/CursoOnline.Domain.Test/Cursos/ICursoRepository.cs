namespace CursoOnline.Domain.Test.Cursos
{
    public interface ICursoRepository
    {
        void Adicionar(Curso curso);
        Curso ObterPeloNome(string nome);
        Curso ObterPeloId(int id);
    }
}
