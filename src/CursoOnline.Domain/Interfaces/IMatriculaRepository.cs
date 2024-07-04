using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CursoOnline.Domain.Interfaces
{
    public interface IMatriculaRepository
    {
        void Adicionar(Matricula matricula);
        Matricula ObterPorId(int id);
    }
}
