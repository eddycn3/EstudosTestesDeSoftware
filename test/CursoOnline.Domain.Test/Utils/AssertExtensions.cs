using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CursoOnline.Domain.Test.Utils
{
    public static class AssertExtensions
    {
        public static void ComMensagem(this ArgumentException exception, string message)
        {
            if (exception.Message == message)
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
