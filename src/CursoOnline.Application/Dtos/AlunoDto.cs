using CursoOnline.Domain;

namespace CursoOnline.Application.Dtos
{
    public class AlunoDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Email { get; set; }
        public PublicoAlvoEnum PublicoAlvo { get; set; }
    }
}
