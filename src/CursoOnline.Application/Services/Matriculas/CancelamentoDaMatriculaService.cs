using CursoOnline.Domain.Base;
using CursoOnline.Domain;
using CursoOnline.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CursoOnline.Application.Services.Matriculas
{
    public class CancelamentoDaMatriculaService
    {
        private IMatriculaRepository _matriculaRepository;
        public CancelamentoDaMatriculaService(IMatriculaRepository matriculaRepository)
        {
            _matriculaRepository = matriculaRepository;
        }

        public void Cancelar(int id)
        {
            var matricula = _matriculaRepository.ObterPorId(id);

            ValidadorDeRegra.Novo()
                .Quando(matricula is null, MensagensValidacaoDeDominio.MatriculaNaoLocalizada)
                .DispararExcecaoSeExistir();

            matricula.Cancelar();
        }
    }
}
