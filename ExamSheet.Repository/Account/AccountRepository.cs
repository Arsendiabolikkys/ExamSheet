using NHibernate;

namespace ExamSheet.Repository.Account
{
    public class AccountRepository : RepositoryBase<Account>
    {
        public AccountRepository(ISessionFactory sessionFactory)
            : base(sessionFactory) { }
    }
}
