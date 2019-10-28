using System.Collections.Generic;
using System.Linq;
using ExamSheet.Repository;
using ExamSheet.Repository.Faculty;

namespace ExamSheet.Business.Faculty
{
    public class FacultyManager : BaseManager<FacultyModel>
    {
        public FacultyManager(RepositoryWrapper repositoryWrapper)
            : base(repositoryWrapper) { }

        protected FacultyRepository Repository => repositoryWrapper.Faculty;

        public override IEnumerable<FacultyModel> FindAll()
        {
            return Repository.FindAll().Select(CreateModel);
        }

        public override FacultyModel GetById(string id)
        {
            return CreateModel(Repository.GetById(id));
        }

        public override FacultyModel CreateModel(IEntity entity)
        {
            var faculty = entity as Repository.Faculty.Faculty;
            var model = new FacultyModel();
            model.Id = faculty.Id;
            model.Name = faculty.Name;
            return model;
        }
    }
}