using CursoOnline.Application.Dtos;
using CursoOnline.Domain;
using CursoOnline.Domain.Base;
using CursoOnline.Domain.Interfaces;

namespace CursoOnline.Application.Services
{
    public class ArmazedorDeCursoService
    {
        private readonly ICursoRepository _cursoRepository;
        private readonly IPublicoAlvoConversor _publicoAlvoConversor;

        public ArmazedorDeCursoService(ICursoRepository cursoRepository, IPublicoAlvoConversor publicoAlvoConversor)
        {
            _cursoRepository = cursoRepository;
            _publicoAlvoConversor = publicoAlvoConversor;
        }

        public void Armazenar(CursoDto cursoDto)
        {
            var cursoJaSalvo = _cursoRepository.ObterPeloNome(cursoDto.Nome);

            ValidadorDeRegra.Novo()
                .Quando(cursoJaSalvo is not null && cursoJaSalvo.Id != cursoDto.Id, MensagensValidacaoDeDominio.CursoJaExiste)
                .DispararExcecaoSeExistir();

            var publicoAlvo = _publicoAlvoConversor.Converter(cursoDto.PublicoAlvo);

            var curso = new Curso(cursoDto.Nome, cursoDto.CargaHoraria, publicoAlvo, cursoDto.Valor, cursoDto.Descricao);

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
