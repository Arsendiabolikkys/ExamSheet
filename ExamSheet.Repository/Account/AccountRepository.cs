using System.Collections.Generic;
using NHibernate;
using NHibernate.Criterion;

namespace ExamSheet.Repository.Account
{
    public class AccountRepository : RepositoryBase<Account>
    {
        public AccountRepository(ISessionFactory sessionFactory)
            : base(sessionFactory) { }

        public override IEnumerable<Account> FindAll(int page, int count)
        {
            using (var session = sessionFactory.OpenSession())
            {
                var query = session.QueryOver<Account>();
                return query
                    .OrderBy(x => x.Email)
                    .Asc
                    .Skip((page - 1) * count)
                    .Take(count)
                    .List();
            }
        }

        public virtual Account GetByEmail(string email)
        {
            using (var session = sessionFactory.OpenSession())
            {
                var criteria = session.CreateCriteria<Account>()
                    .Add(Restrictions.Eq("Email", email));

                var account = criteria.UniqueResult<Account>();
                return account;
            }
        }
    }
}
