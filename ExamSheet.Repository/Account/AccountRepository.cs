using System.Collections.Generic;
using NHibernate;
using NHibernate.Criterion;

namespace ExamSheet.Repository.Account
{
    public class AccountRepository : RepositoryBase<Account>
    {
        public AccountRepository(ISessionFactory sessionFactory)
            : base(sessionFactory) { }

        public override IEnumerable<Account> FindAll()
        {
            using (var session = sessionFactory.OpenSession())
            {
                var criteria = session.CreateCriteria<Account>()
                    .Add(Restrictions.Not(Restrictions.Eq("AccountType", (short)2)))
                    .AddOrder(Order.Asc("Email"));

                return criteria.List<Account>();
            }
        }

        public override IEnumerable<Account> FindAll(int page, int count)
        {
            using (var session = sessionFactory.OpenSession())
            {
                var criteria = session.CreateCriteria<Account>()
                    .Add(Restrictions.Not(Restrictions.Eq("AccountType", (short)2)))
                    .AddOrder(Order.Asc("Email"))
                    .SetFirstResult(page - 1)
                    .SetMaxResults(count);

                return criteria.List<Account>();
            }
        }

        public override int GetTotal()
        {
            using (var session = sessionFactory.OpenSession())
            {
                var criteria = session.CreateCriteria<Account>()
                    .SetProjection(Projections.RowCount())
                    .Add(Restrictions.Not(Restrictions.Eq("AccountType", (short)2)));

                return criteria.UniqueResult<int>();
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
