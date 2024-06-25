using CursoOnline.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CursoOnline.Domain.Test.Utils
{
    public static class AssertExtensions
    {
        public static void ComMensagem(this ExecaoDeDominio exception, string message)
        {
            if (exception.MensagensDeErro.Contains(message))
            {
                Assert.True(true);
            }
            else
            {
                Assert.False(true,$"Esperava a mensagem '{message}'");
            }
        }
    }
}
