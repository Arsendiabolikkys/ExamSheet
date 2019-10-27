using ExamSheet.Repository.ExamSheet;
using NHibernate;

namespace ExamSheet.Repository
{
    public class RepositoryWrapper
    {
        private ISessionFactory sessionFactory;
        private ExamSheetRepository examSheet;

        public ExamSheetRepository ExamSheet
        {
            get
            {
                if (examSheet == null)
                {
                    examSheet = new ExamSheetRepository(sessionFactory);
                }
                return examSheet;
            }
        }

        public RepositoryWrapper(ISessionFactory sessionFactory)
        {
            this.sessionFactory = sessionFactory;
        }
    }
}
