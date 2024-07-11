using CursoOnline.Application.Services.Matriculas;
using CursoOnline.Domain.Base;
using CursoOnline.Domain.Interfaces;
using CursoOnline.Domain.Test.Builders;
using CursoOnline.Domain.Test.Utils;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CursoOnline.Domain.Test.Matriculas
{
    public class ConclusaoMatriculaServiceTest
    {
        private readonly Mock<IMatriculaRepository> _matriculaRepositoryMock;
        private readonly ConclusaoMatriculaService _conclusaoMatriculaService;

        public ConclusaoMatriculaServiceTest()
        {
            _matriculaRepositoryMock = new Mock<IMatriculaRepository>();
            _conclusaoMatriculaService = new ConclusaoMatriculaService(_matriculaRepositoryMock.Object);
        }

        [Fact]
        public void DeveInformarNotaAluno()
        {
            double notaAluno = 9.5;
            var matricula = MatriculaBuilder.Novo().Build();
            _matriculaRepositoryMock.Setup(s => s.ObterPorId(matricula.Id)).Returns(matricula);
            _conclusaoMatriculaService.Concluir(matricula.Id, notaAluno);

            Assert.Equal(matricula.NotaAluno, notaAluno);
        }

        [Fact]
        public void DeveNotificarQuandoMatriulaNaoExistir()
        {
            Matricula matriculaInvalida = null;
            double notaAluno = 0;
            int matriculaInvalidaId = 1;

            _matriculaRepositoryMock.Setup(s => s.ObterPorId(It.IsAny<int>())).Returns(matriculaInvalida);

            Assert.Throws<ExecaoDeDominio>(() => _conclusaoMatriculaService.Concluir(matriculaInvalidaId, notaAluno))
                .ComMensagem(MensagensValidacaoDeDominio.MatriculaNaoLocalizada);
        }

       
    }
}
