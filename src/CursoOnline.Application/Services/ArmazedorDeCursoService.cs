using CursoOnline.Application.Dtos;
using CursoOnline.Domain;
using CursoOnline.Domain.Base;
using CursoOnline.Domain.Interfaces;

namespace CursoOnline.Application.Services
{
    public class ArmazedorDeCursoService
    {
        private readonly ICursoRepository _cursoRepository;

        public ArmazedorDeCursoService(ICursoRepository cursoRepository)
        {
            _cursoRepository = cursoRepository;
        }

        public void Armazenar(CursoDto cursoDto)
        {
            var cursoJaSalvo = _cursoRepository.ObterPeloNome(cursoDto.Nome);

            ValidadorDeRegra.Novo()
                .Quando(cursoJaSalvo is not null && cursoJaSalvo.Id != cursoDto.Id, MensagensValidacaoDeDominio.CursoJaExiste)
                .Quando(!Enum.TryParse(cursoDto.PublicoAlvo, out PublicoAlvoEnum publicoAlto), MensagensValidacaoDeDominio.PublicoAlvoInvalido)
                .DispararExcecaoSeExistir();

            var curso = new Curso(cursoDto.Nome, cursoDto.CargaHoraria, publicoAlto, cursoDto.Valor, cursoDto.Descricao);

            if (cursoDto.Id > 0)
            {
                curso = _cursoRepository.ObterPeloId(cursoDto.Id);
                curso.AlterarNome(cursoDto.Nome);
                curso.AlterarValor(cursoDto.Valor);
                curso.AlterarCargaHoraria(cursoDto.CargaHoraria);
            }

            if (cursoDto.Id == 0)
                _cursoRepository.Adicionar(curso);
        }

    }
}
