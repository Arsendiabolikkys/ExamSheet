using ExamSheet.Business.Group;
using ExamSheet.Repository;
using ExamSheet.Repository.Student;
using System.Collections.Generic;
using System.Linq;

namespace ExamSheet.Business.Student
{
    public class StudentManager : BaseManager<StudentModel>, IItemManager<StudentModel>
    {
        protected virtual GroupManager GroupManager { get; set; }

        public StudentManager(RepositoryWrapper repositoryWrapper, GroupManager groupManager)
            : base(repositoryWrapper)
        {
            GroupManager = groupManager;
        }

        protected StudentRepository Repository => repositoryWrapper.Student;

        public override IEnumerable<StudentModel> FindAll()
        {
            return Repository.FindAll().Select(CreateModel);
        }

        public IEnumerable<StudentModel> FindGroup(string groupId)
        {
            if (string.IsNullOrEmpty(groupId))
                return new List<StudentModel>();

            return Repository.FindGroup(groupId).Select(CreateModel)?.ToList() ?? new List<StudentModel>();
        }

        public override StudentModel GetById(string id)
        {
            return CreateModel(Repository.GetById(id));
        }

        public override StudentModel CreateModel(IEntity entity)
        {
            var student = entity as Repository.Student.Student;
            var model = new StudentModel();
            model.Id = student.Id;
            model.GroupId = student.GroupId;
            model.Name = student.Name;
            model.Surname = student.Surname;
            return model;
        }

        public void Save(StudentModel model)
        {
            if (model == null)
                return;
            if (string.IsNullOrEmpty(model.Id))
                return;
            Repository.Save(CreateModel(model));
        }

        public virtual Repository.Student.Student CreateModel(StudentModel studentModel)
        {
            var student = new Repository.Student.Student();
            student.Id = studentModel.Id;
            student.Name = studentModel.Name;
            student.Surname = studentModel.Surname;
            student.GroupId = studentModel.GroupId;
            return student;
        }

        public void Remove(string id)
        {
            if (string.IsNullOrEmpty(id))
                return;

            Repository.Remove(id);
        }
    }
}