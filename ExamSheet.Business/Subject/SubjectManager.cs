using ExamSheet.Repository;
using ExamSheet.Repository.Subject;
using System.Collections.Generic;
using System.Linq;

namespace ExamSheet.Business.Subject
{
    public class SubjectManager : BaseManager<SubjectModel>, IItemManager<SubjectModel>
    {
        public SubjectManager(RepositoryWrapper repositoryWrapper)
            : base(repositoryWrapper) { }

        protected SubjectRepository Repository => repositoryWrapper.Subject;

        public override IEnumerable<SubjectModel> FindAll()
        {
            return Repository.FindAll().Select(CreateModel);
        }

        public virtual IEnumerable<SubjectModel> FindAllForTeacher(string teacherId)
        {
            if (string.IsNullOrEmpty(teacherId))
                return new List<SubjectModel>();

            return Repository.FindAllForTeacher(teacherId).Select(CreateModel);
        }

        public virtual IEnumerable<SubjectModel> FindAllForFaculty(string facultyId)
        {
            if (string.IsNullOrEmpty(facultyId))
                return new List<SubjectModel>();

            return Repository.FindAllForFaculty(facultyId).Select(CreateModel);
        }

        public virtual IEnumerable<SubjectModel> FindAll(int page, int count)
        {
            return Repository.FindAll(page, count).Select(CreateModel);
        }

        public virtual int GetTotal()
        {
            return Repository.GetTotal();
        }

        public override SubjectModel GetById(string id)
        {
            var subject = Repository.GetById(id);
            if (subject == null)
                return null;
            return CreateModel(subject);
        }

        public virtual List<SubjectModel> GetByIdList(IEnumerable<string> ids)
        {
            if (!ids?.Any() ?? true)
                return new List<SubjectModel>();

            var subjects = Repository.GetByIdList(ids.ToArray());
            if (!subjects?.Any() ?? true)
                return new List<SubjectModel>();

            return subjects.Select(CreateModel).ToList();
        }

        public virtual void Save(SubjectModel subjectModel)
        {
            if (subjectModel == null)
                return;
            if (string.IsNullOrEmpty(subjectModel.Id))
                return;
            Repository.Save(CreateModel(subjectModel));
        }

        public virtual void Remove(string id)
        {
            if (string.IsNullOrEmpty(id))
                return;

            Repository.Remove(id);
        }

        public virtual Repository.Subject.Subject CreateModel(SubjectModel subjectModel)
        {
            var subject = new Repository.Subject.Subject();
            subject.Id = subjectModel.Id;
            subject.Name = subjectModel.Name;
            return subject;
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
