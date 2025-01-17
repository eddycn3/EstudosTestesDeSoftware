﻿
using CursoOnline.Domain;

namespace CursoOnline.Domain.Test.Builders
{
    public class MatriculaBuilder
    {
        private int _id;
        private Aluno _aluno = AlunoBuilder.Novo().Build();
        private Curso _curso = CursoBuilder.Novo().Build();
        private decimal _valorMatricula = 950.0M;
        private bool Concluido;
        private bool Cancelada;
        public static MatriculaBuilder Novo()
        {
            return new MatriculaBuilder();
        }

        public Matricula Build()
        {
            var matricula = new Matricula(_aluno, _curso, _valorMatricula);

            if (_id > 0)
            {
                var propertyInfo = matricula.GetType().GetProperty("Id");
                propertyInfo?.SetValue(matricula, Convert.ChangeType(_id, propertyInfo.PropertyType), null);
            }

            if(Cancelada)
                matricula.Cancelar();

            if(Concluido)
            {
                matricula.InformarNota(7);
            }

            return matricula;
        }

        public MatriculaBuilder ComAluno(Aluno aluno)
        {
            _aluno = aluno;
            return this;
        }

        public MatriculaBuilder ComCurso(Curso curso)
        {
            _curso = curso;
            return this;
        }

        public MatriculaBuilder ComValor(decimal valor)
        {
            _valorMatricula = valor;
            return this;
        }

        public MatriculaBuilder ComConcluido(bool concluido)
        {
            Concluido = concluido;
            return this;
        }
    }

}

