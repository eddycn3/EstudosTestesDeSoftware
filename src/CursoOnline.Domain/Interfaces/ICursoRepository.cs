namespace CursoOnline.Domain.Interfaces
{
    public interface ICursoRepository
    {
        void Adicionar(Curso curso);
        Curso ObterPeloNome(string nome);
        Curso ObterPeloId(int id);
    }
}
