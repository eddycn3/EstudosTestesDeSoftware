namespace CursoOnline.Domain.Base
{
    public class ExecaoDeDominio: ArgumentException 
    { 
        public List<string> MensagensDeErro {  get; set; }

        public ExecaoDeDominio(List<string> mensagensDeErro)
        {
            MensagensDeErro = mensagensDeErro;
        }

    }

}
