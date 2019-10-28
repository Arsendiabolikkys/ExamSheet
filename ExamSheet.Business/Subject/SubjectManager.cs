using ExamSheet.Repository;
using ExamSheet.Repository.Subject;
using System.Collections.Generic;
using System.Linq;

namespace ExamSheet.Business.Subject
{
    public class SubjectManager : BaseManager<SubjectModel>
    {
        public SubjectManager(RepositoryWrapper repositoryWrapper)
            : base(repositoryWrapper) { }

        protected SubjectRepository Repository => repositoryWrapper.Subject;

        public override IEnumerable<SubjectModel> FindAll()
        {
            return Repository.FindAll().Select(CreateModel);
        }

        public override SubjectModel GetById(string id)
        {
            return CreateModel(Repository.GetById(id));
        }

        public override SubjectModel CreateModel(IEntity entity)
        {
            var subject = entity as Repository.Subject.Subject;
            var model = new SubjectModel();
            model.Id = subject.Id;
            model.Name = subject.Name;
            return model;
        }
    }
}
