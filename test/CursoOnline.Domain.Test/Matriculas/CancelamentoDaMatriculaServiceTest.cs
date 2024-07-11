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
    public class CancelamentoDaMatriculaServiceTest
    {
        private readonly Mock<IMatriculaRepository> _matriculaRepositoryMock;
        private readonly CancelamentoDaMatriculaService _conclusaoMatriculaService;

        public CancelamentoDaMatriculaServiceTest()
        {
            _matriculaRepositoryMock = new Mock<IMatriculaRepository>();
            _conclusaoMatriculaService = new CancelamentoDaMatriculaService(_matriculaRepositoryMock.Object);
        }

        [Fact]
        public void DeveCancelarMatricula()
        {
            var matricula = MatriculaBuilder.Novo().Build();
            _matriculaRepositoryMock.Setup(s => s.ObterPorId(matricula.Id)).Returns(matricula);

            _conclusaoMatriculaService.Cancelar(matricula.Id);

            Assert.True(matricula.Cancelada);

        }

        [Fact]
        public void DeveNotificarQuandoMatriulaNaoExistir()
        {
            Matricula matriculaInvalida = null;
            int matriculaInvalidaId = 1;

            _matriculaRepositoryMock.Setup(s => s.ObterPorId(It.IsAny<int>())).Returns(matriculaInvalida);

            Assert.Throws<ExecaoDeDominio>(() => _conclusaoMatriculaService.Cancelar(matriculaInvalidaId))
                .ComMensagem(MensagensValidacaoDeDominio.MatriculaNaoLocalizada);
        }
    }
}
