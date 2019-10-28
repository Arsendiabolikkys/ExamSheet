using ExamSheet.Business.Faculty;
using ExamSheet.Repository;
using ExamSheet.Repository.Deanery;
using System.Collections.Generic;
using System.Linq;

namespace ExamSheet.Business.Deanery
{
    public class DeaneryManager : BaseManager<DeaneryModel>
    {
        protected virtual FacultyManager FacultyManager { get; set; }

        public DeaneryManager(RepositoryWrapper repositoryWrapper, FacultyManager facultyManager)
            : base(repositoryWrapper)
        {
            FacultyManager = facultyManager;
        }

        protected DeaneryRepository Repository => repositoryWrapper.Deanery;

        public override IEnumerable<DeaneryModel> FindAll()
        {
            return Repository.FindAll().Select(CreateModel);
        }

        public override DeaneryModel GetById(string id)
        {
            return CreateModel(Repository.GetById(id));
        }

        public override DeaneryModel CreateModel(IEntity entity)
        {
            var deanery = entity as Repository.Deanery.Deanery;
            var model = new DeaneryModel();
            model.Id = deanery.Id;
            model.Name = deanery.Name;
            model.Faculty = FacultyManager.GetById(deanery.FacultyId);
            return model;
        }
    }
}
