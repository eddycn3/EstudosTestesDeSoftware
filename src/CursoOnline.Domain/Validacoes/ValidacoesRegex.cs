using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CursoOnline.Domain.Validacoes
{
    public static class ValidacaoRegex
    {
        public static bool CepfEhValido(this string cpf)
        {
            if (string.IsNullOrEmpty(cpf))
                return false;

            var regex = new Regex(@"^\d{3}\.\d{3}\.\d{3}-\d{2}$");

            return regex.IsMatch(cpf);
        }

        public static bool EmailEhValido(this string email)
        {
            if (string.IsNullOrEmpty(email))
                return false;

            var regex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");

            return regex.IsMatch(email);
        }

    }
}
