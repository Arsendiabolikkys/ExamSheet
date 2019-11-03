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

        public virtual void Save(FacultyModel facultyModel)
        {
            if (facultyModel == null)
                return;
            if (string.IsNullOrEmpty(facultyModel.Id))
                return;
            Repository.Save(CreateModel(facultyModel));
        }

        public virtual void Remove(string id)
        {
            if (string.IsNullOrEmpty(id))
                return;

            Repository.Remove(id);
        }
        
        public virtual Repository.Faculty.Faculty CreateModel(FacultyModel facultyModel)
        {
            var faculty = new Repository.Faculty.Faculty();
            faculty.Id = facultyModel.Id;
            faculty.Name = facultyModel.Name;
            return faculty;
        }

        public override FacultyModel CreateModel(IEntity entity)
        {
            var faculty = entity as Repository.Faculty.Faculty;
            var model = new FacultyModel();
            if (faculty == null)
                return model;
            model.Id = faculty.Id;
            model.Name = faculty.Name;
            return model;
        }
    }
}