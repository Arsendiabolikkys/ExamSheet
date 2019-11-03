using NHibernate;
using System.Collections.Generic;
using System.Linq;

namespace ExamSheet.Repository
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : IEntity
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

        public virtual void Save(T model)
        {
            using (var session = sessionFactory.OpenSession())
            {
                session.SaveOrUpdate(model);
                session.Flush();
            }
        }

        public virtual void Remove(string id)
        {
            using (var session = sessionFactory.OpenSession())
            {
                var item = session.Get<T>(id);
                if (item != null)
                {
                    session.Delete(item);
                    session.Flush();
                }
            }
        }

        public virtual T GetById(string id)
        {
            using (var session = sessionFactory.OpenSession())
            {
                return session.Get<T>(id);
            }
        }
    }
}