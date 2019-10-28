using ExamSheet.Repository;
using ExamSheet.Repository.Semester;
using System.Collections.Generic;
using System.Linq;

namespace ExamSheet.Business.Semester
{
    public class SemesterManager : BaseManager<SemesterModel>
    {
        public SemesterManager(RepositoryWrapper repositoryWrapper)
            : base(repositoryWrapper) { }

        protected SemesterRepository Repository => repositoryWrapper.Semester;

        public override IEnumerable<SemesterModel> FindAll()
        {
            return Repository.FindAll().Select(CreateModel);
        }

        public override SemesterModel GetById(string id)
        {
            return new SemesterModel() { Id = "Id", Year = 2019, Number = 1 };
            return CreateModel(Repository.GetById(id));
        }

        public override SemesterModel CreateModel(IEntity entity)
        {
            var semester = entity as Repository.Semester.Semester;
            var model = new SemesterModel();
            model.Id = semester.Id;
            model.Number = semester.Number;
            model.Year = semester.Year;
            return model;
        }
    }
}
