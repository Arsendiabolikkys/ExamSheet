using ExamSheet.Business.Account;
using ExamSheet.Business.Deanery;
using ExamSheet.Business.Faculty;
using ExamSheet.Web.Attributes;
using ExamSheet.Web.Models;
using System;
using System.Linq;

namespace ExamSheet.Web.Controllers
{
    [IsInRole(AccountType.Admin)]
    public class DeaneryController : ItemsController<DeaneryModel, DeaneryViewModel>
    {
        protected FacultyManager FacultyManager { get; set; }

        public DeaneryController(DeaneryManager manager, FacultyManager facultyManager)
            : base(manager)
        {
            FacultyManager = facultyManager;
        }

        protected virtual void InitFaculties()
        {
            var faculties = FacultyManager.FindAll().Select(CreateFacultyViewModel).ToList();
            ViewData["faculties"] = faculties;
        }
        
        protected virtual FacultyViewModel CreateFacultyViewModel(FacultyModel facultyModel)
        {
            return new FacultyViewModel()
            {
                Id = facultyModel.Id,
                Name = facultyModel.Name
            };
        }

        protected override void OnEdit()
        {
            InitFaculties();
            base.OnEdit();
        }

        protected override void OnCreate()
        {
            InitFaculties();
            base.OnCreate();
        }

        protected override DeaneryModel CreateModel(DeaneryViewModel model)
        {
            return new DeaneryModel()
            {
                Id = model.Id,
                Name = model.Name,
                FacultyId = model.FacultyId
            };
        }

        protected override DeaneryViewModel CreateViewModel(DeaneryModel model)
        {
            return new DeaneryViewModel()
            {
                Id = model.Id,
                Name = model.Name,
                FacultyId = model.FacultyId
            };
        }

        protected override DeaneryViewModel CreateViewModel()
        {
            return new DeaneryViewModel()
            {
                Id = Guid.NewGuid().ToString()
            };
        }
    }
}