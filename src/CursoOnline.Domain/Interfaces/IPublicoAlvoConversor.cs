using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CursoOnline.Domain.Interfaces
{
    public interface IPublicoAlvoConversor
    {
        PublicoAlvoEnum Converter(string publicoAlvo);
    }
}
