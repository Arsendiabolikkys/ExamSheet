using ExamSheet.Repository;
using ExamSheet.Repository.Teacher;
using System.Collections.Generic;
using System.Linq;

namespace ExamSheet.Business.Teacher
{
    public class TeacherManager : BaseManager<TeacherModel>, IItemManager<TeacherModel>
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
            var model = Repository.GetById(id);
            if (model == null)
                return null;
            return CreateModel(model);
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

        public void Save(TeacherModel model)
        {
            if (model == null)
                return;
            if (string.IsNullOrEmpty(model.Id))
                return;
            Repository.Save(CreateModel(model));
        }

        public virtual Repository.Teacher.Teacher CreateModel(TeacherModel teacherModel)
        {
            var teacher = new Repository.Teacher.Teacher();
            teacher.Id = teacherModel.Id;
            teacher.Name = teacherModel.Name;
            teacher.Surname = teacherModel.Surname;
            return teacher;
        }

        public void Remove(string id)
        {
            if (string.IsNullOrEmpty(id))
                return;

            Repository.Remove(id);
        }
    }
}
