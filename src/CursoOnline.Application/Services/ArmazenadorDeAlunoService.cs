using CursoOnline.Application.Dtos;
using CursoOnline.Domain;
using CursoOnline.Domain.Base;
using CursoOnline.Domain.Interfaces;

namespace CursoOnline.Application.Services
{
    public class ArmazenadorDeAlunoService
    {
        private readonly IAlunoRepository _alunoRepository;
        private readonly IPublicoAlvoConversor _publicoAlvoConversor;
        public ArmazenadorDeAlunoService(IAlunoRepository alunoRepository, IPublicoAlvoConversor publicoAlvoConversor)
        {
            _alunoRepository = alunoRepository;
            _publicoAlvoConversor = publicoAlvoConversor;
        }

        public void Armazenar(AlunoDto alunoDto)
        {
            var alunoJaSalvo = _alunoRepository.ObterPorId(alunoDto.Id);

            ValidadorDeRegra.Novo()
                .Quando(alunoJaSalvo is not null && alunoJaSalvo.Cpf == alunoDto.Cpf, MensagensValidacaoDeDominio.AlunoJaExistente)
                .DispararExcecaoSeExistir();

            var publicoAlvoConvertido = _publicoAlvoConversor.Converter(alunoDto.PublicoAlvo);
            var aluno = new Aluno(alunoDto.Nome, alunoDto.Email, alunoDto.Cpf, publicoAlvoConvertido);

            if (alunoDto.Id > 0)
            {
                aluno = _alunoRepository.ObterPorId(alunoDto.Id);
                aluno.AlterarNome(alunoDto.Nome);
                aluno.AlterarCpf(alunoDto.Cpf);
            }

            if (alunoDto.Id == 0)
                _alunoRepository.Adicionar(aluno);
        }

    }
}
