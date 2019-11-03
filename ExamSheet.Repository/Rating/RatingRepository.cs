using NHibernate;
using NHibernate.Criterion;
using System.Collections.Generic;

namespace ExamSheet.Repository.Rating
{
    public class RatingRepository : RepositoryBase<Rating>
    {
        public RatingRepository(ISessionFactory sessionFactory)
            : base(sessionFactory) { }

        public virtual IList<Rating> FindAll(string examSheetId)
        {
            using (var session = sessionFactory.OpenSession())
            {
                var criteria = session.CreateCriteria<Rating>()
                    .Add(Expression.Eq("ExamSheetId", examSheetId));

                return criteria.List<Rating>();
            }
        }
    }
}
