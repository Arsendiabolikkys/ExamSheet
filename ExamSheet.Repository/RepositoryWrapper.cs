using ExamSheet.Business;
using ExamSheet.Business.ExamSheet;
using ExamSheet.Repository.ExamSheet;
using NHibernate;

namespace ExamSheet.Repository
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private ISessionFactory sessionFactory;
        private IExamSheetRepository examSheet;

        public IExamSheetRepository ExamSheet
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
