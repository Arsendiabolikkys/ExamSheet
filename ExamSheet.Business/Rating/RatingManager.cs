using ExamSheet.Business.Semester;
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

        public RatingManager(RepositoryWrapper repositoryWrapper, SemesterManager semesterManager, SubjectManager subjectManager,
            StudentManager studentManager)
            : base(repositoryWrapper)
        {
            //SemesterManager = semesterManager;
            //SubjectManager = subjectManager;
            StudentManager = studentManager;
        }

        protected RatingRepository Repository => repositoryWrapper.Rating;

        public override IEnumerable<RatingModel> FindAll()
        {
            return Repository.FindAll().Select(CreateModel);
        }

        public override RatingModel GetById(string id)
        {
            return new RatingModel();
            return CreateModel(Repository.GetById(id));
        }

        public virtual IList<RatingModel> FindAll(string examSheetId)
        {
            if (string.IsNullOrEmpty(examSheetId))
                return new List<RatingModel>();
            return Repository.FindAll(examSheetId).Select(CreateModel).ToList();
        }

        public override RatingModel CreateModel(IEntity entity)
        {
            var rating = entity as Repository.Rating.Rating;
            var model = new RatingModel();
            model.Mark = rating.Mark;
            //model.Semester = SemesterManager.GetById(rating.SemesterId);
            //model.Subject = SubjectManager.GetById(rating.SubjectId);
            model.Student = StudentManager.GetById(rating.StudentId);
            return model;
        }
    }
}
