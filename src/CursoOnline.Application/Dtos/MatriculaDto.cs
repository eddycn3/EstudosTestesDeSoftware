using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CursoOnline.Application.Dtos
{
    public class MatriculaDto
    {
        public int Id { get; set; }
        public int AlunoId { get; set; }
        public int CursoId { get; set; }
        public decimal ValorMatricula { get; set; }
    }
}
