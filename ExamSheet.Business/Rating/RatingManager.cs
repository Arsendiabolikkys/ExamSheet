using ExamSheet.Business.Student;
using ExamSheet.Business.Subject;
using ExamSheet.Repository;
using ExamSheet.Repository.Rating;
using System.Collections.Generic;
using System.Linq;

namespace ExamSheet.Business.Rating
{
    public class RatingManager : BaseManager<RatingModel>
    {
        protected virtual StudentManager StudentManager { get; set; }

        public RatingManager(RepositoryWrapper repositoryWrapper, SubjectManager subjectManager, StudentManager studentManager)
            : base(repositoryWrapper)
        {
            StudentManager = studentManager;
        }

        protected RatingRepository Repository => repositoryWrapper.Rating;

        public override IEnumerable<RatingModel> FindAll()
        {
            return Repository.FindAll().Select(CreateModel);
        }

        public override RatingModel GetById(string id)
        {
            return CreateModel(Repository.GetById(id));
        }

        public virtual IList<RatingModel> FindAll(string examSheetId)
        {
            if (string.IsNullOrEmpty(examSheetId))
                return new List<RatingModel>();
            return Repository.FindAll(examSheetId).Select(CreateModel).ToList();
        }

        public virtual IList<RatingModel> FindAll(IEnumerable<string> examSheetIds)
        {
            if (!examSheetIds?.Any() ?? true)
                return new List<RatingModel>();
            return Repository.FindAll(examSheetIds.ToArray()).Select(CreateModel).ToList();
        }

        public virtual void SaveRatings(IEnumerable<RatingModel> ratings)
        {
            if (!ratings?.Any() ?? true)
                return;

            Repository.Save(ratings.Select(CreateEntity));
        }

        public virtual Repository.Rating.Rating CreateEntity(RatingModel model)
        {
            return new Repository.Rating.Rating()
            {
                ExamSheetId = model.ExamSheetId,
                Mark = model.Mark,
                StudentId = model.StudentId
            };
        }

        public override RatingModel CreateModel(IEntity entity)
        {
            var rating = entity as Repository.Rating.Rating;
            var model = new RatingModel();
            model.Mark = rating.Mark;
            model.StudentId = rating.StudentId;
            model.ExamSheetId = rating.ExamSheetId;
            return model;
        }
    }
}
