using System;
using NHibernate;

namespace ExamSheet.Repository.ExamSheet
{
    public class ExamSheetRepository : RepositoryBase<ExamSheet>
    {
        public ExamSheetRepository(ISessionFactory sessionFactory) 
            : base(sessionFactory) { }

        //public override IEnumerable<ExamSheetModel> FindAll()
        //{
        //    using (var session = sessionFactory.OpenSession())
        //    {
        //        return new List<ExamSheetModel>()
        //        {
        //            new ExamSheetModel() { Id = Guid.NewGuid().ToString(), Ratings = "Test", State = ExamSheetState.New },
        //            new ExamSheetModel() { Id = Guid.NewGuid().ToString(), Ratings = "Test rating", State = ExamSheetState.New },
        //            new ExamSheetModel() { Id = Guid.NewGuid().ToString(), Ratings = "Test", State = ExamSheetState.Open },
        //            new ExamSheetModel() { Id = Guid.NewGuid().ToString(), Ratings = "Test", State = ExamSheetState.Open },
        //            new ExamSheetModel() { Id = Guid.NewGuid().ToString(), Ratings = "Test", State = ExamSheetState.Closed },
        //            new ExamSheetModel() { Id = Guid.NewGuid().ToString(), Ratings = "Test", State = ExamSheetState.Archived },
        //        };
        //    }
            
        //}
    }
}