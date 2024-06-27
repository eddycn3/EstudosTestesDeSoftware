using Bogus;
using Bogus.Extensions.Brazil;
using CursoOnline.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CursoOnline.Domain.Test.Alunos
{
    public class AlunoArmazenadorServiceTest
    {
        public AlunoDto _alunoDto;
        public AlunoArmazenadorServiceTest()
        {
            var faker = new Faker();
            _alunoDto = new AlunoDto
            {
                Nome = faker.Person.FullName,
                Cpf = faker.Person.Cpf(),
                Email = faker.Person.Email,
                PublicoAlvo = PublicoAlvoEnum.Estudante
            };
            
        }


        [Fact]
        public void DeveAdicionarAluno()
        {
            var aluno 
        }
    }
}
