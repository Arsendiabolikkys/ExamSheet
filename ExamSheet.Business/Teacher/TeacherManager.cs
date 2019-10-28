using ExamSheet.Repository;
using ExamSheet.Repository.Teacher;
using System.Collections.Generic;
using System.Linq;

namespace ExamSheet.Business.Teacher
{
    public class TeacherManager : BaseManager<TeacherModel>
    {
        public TeacherManager(RepositoryWrapper repositoryWrapper)
            : base(repositoryWrapper) { }

        protected TeacherRepository Repository => repositoryWrapper.Teacher;

        public override IEnumerable<TeacherModel> FindAll()
        {
            return Repository.FindAll().Select(CreateModel);
        }

        public override TeacherModel GetById(string id)
        {
            return CreateModel(Repository.GetById(id));
        }

        public override TeacherModel CreateModel(IEntity entity)
        {
            var teacher = entity as Repository.Teacher.Teacher;
            var model = new TeacherModel();
            model.Id = teacher.Id;
            model.Name = teacher.Name;
            model.Surname = teacher.Surname;
            return model;
        }
    }
}
