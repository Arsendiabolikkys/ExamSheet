using System.Collections.Generic;
using ExamSheet.Repository;

namespace ExamSheet.Business.Student
{
    public class StudentManager : BaseManager<StudentModel>
    {
        public StudentManager(RepositoryWrapper repositoryWrapper)
            : base(repositoryWrapper) { }

        public override IEnumerable<StudentModel> FindAll()
        {
            throw new System.NotImplementedException();
        }
    }
}