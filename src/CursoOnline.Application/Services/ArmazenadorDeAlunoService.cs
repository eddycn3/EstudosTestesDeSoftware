using CursoOnline.Application.Dtos;
using CursoOnline.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CursoOnline.Application.Services
{
    internal class ArmazenadorDeAlunoService
    {
        private readonly IAlunoRepository _alunoRepository;
        public ArmazenadorDeAlunoService(IAlunoRepository alunoRepository)
        {
            _alunoRepository = alunoRepository;
        }

        public void Armazenar(AlunoDto alunoDto)
        {

        }

    }
}
