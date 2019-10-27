using NHibernate;
using System.Collections.Generic;
using System.Linq;

namespace ExamSheet.Repository
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected ISessionFactory sessionFactory;

        public RepositoryBase(ISessionFactory sessionFactory)
        {
            this.sessionFactory = sessionFactory;
        }

        public virtual IEnumerable<T> FindAll()
        {
            using (var session = sessionFactory.OpenSession())
            {
                var query = session.Query<T>();
                return query.ToList();
            }
        }
    }
}