using EM.Domain.Conexao;
using EM.Domain.Interfaces;
using FirebirdSql.Data.FirebirdClient;
using System;

namespace EM.Repository
{
    public abstract class RepositorioAbstrato<T>
        where T : IEntidade
    {

        public abstract void Add(T aluno);
        public abstract void Remove(T aluno);
        public abstract void Update(T aluno);
        public abstract IEnumerable<T> GetAll();
        public abstract IEnumerable<T> Get(Func<T, bool> predicate);

    }
}
