using ExamSheet.Business.Group;
using ExamSheet.Repository;
using ExamSheet.Repository.Student;
using System.Collections.Generic;
using System.Linq;

namespace ExamSheet.Business.Student
{
    public class StudentManager : BaseManager<StudentModel>
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

        public override StudentModel GetById(string id)
        {
            return CreateModel(Repository.GetById(id));
        }

        public override StudentModel CreateModel(IEntity entity)
        {
            var student = entity as Repository.Student.Student;
            var model = new StudentModel();
            model.Id = student.Id;
            model.Group = GroupManager.GetById(student.GroupId);
            model.Name = student.Name;
            model.Surname = student.Surname;
            return model;
        }
    }
}