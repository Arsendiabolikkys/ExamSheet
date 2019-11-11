using NHibernate;
using NHibernate.Criterion;

namespace ExamSheet.Repository.Account
{
    public class AccountRepository : RepositoryBase<Account>
    {
        public AccountRepository(ISessionFactory sessionFactory)
            : base(sessionFactory) { }

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
