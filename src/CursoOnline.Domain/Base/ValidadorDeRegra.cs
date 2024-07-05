namespace CursoOnline.Domain.Base
{
    public class ValidadorDeRegra
    {
        private List<string> _mensagensErros;

        private ValidadorDeRegra()
        {
            _mensagensErros = new List<string>();
        }

        public static ValidadorDeRegra Novo()
        {
            return new ValidadorDeRegra();
        }

        public ValidadorDeRegra Quando(bool temErro, string mensagemErro)
        {
            if(temErro)
                _mensagensErros.Add(mensagemErro);


            return this;
        }

        public void DispararExcecaoSeExistir()
        {
            if (_mensagensErros.Any())
                throw new ExecaoDeDominio(_mensagensErros);
        }
    }

}
