using ExamSheet.Business.Faculty;
using ExamSheet.Repository;
using ExamSheet.Repository.Deanery;
using System.Collections.Generic;
using System.Linq;

namespace ExamSheet.Business.Deanery
{
    public class DeaneryManager : BaseManager<DeaneryModel>, IItemManager<DeaneryModel>
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
            var model = Repository.GetById(id);
            if (model == null)
                return null;
            return CreateModel(model);
        }

        public override DeaneryModel CreateModel(IEntity entity)
        {
            var deanery = entity as Repository.Deanery.Deanery;
            var model = new DeaneryModel();
            model.Id = deanery.Id;
            model.Name = deanery.Name;
            //model.Faculty = FacultyManager.GetById(deanery.FacultyId);
            model.FacultyId = deanery.FacultyId;
            return model;
        }

        public void Save(DeaneryModel model)
        {
            if (model == null)
                return;
            if (string.IsNullOrEmpty(model.Id))
                return;
            Repository.Save(CreateModel(model));
        }

        public virtual Repository.Deanery.Deanery CreateModel(DeaneryModel deaneryModel)
        {
            var deanery = new Repository.Deanery.Deanery();
            deanery.Id = deaneryModel.Id;
            deanery.Name = deaneryModel.Name;
            deanery.FacultyId = deaneryModel.FacultyId;
            return deanery;
        }

        public void Remove(string id)
        {
            if (string.IsNullOrEmpty(id))
                return;

            Repository.Remove(id);
        }
    }
}
