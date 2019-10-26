using System.Collections.Generic;
using System.Linq;

namespace ExamSheet.Data
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected RepositoryContext repositoryContext;

        public RepositoryBase(RepositoryContext repositoryContext)
        {
            this.repositoryContext = repositoryContext;
        }

        public IEnumerable<T> FindAll()
        {
            using (var session = repositoryContext.Factory.OpenSession())
            {
                var query = session.Query<T>();
                return query.ToList();
            }
        }
    }
}