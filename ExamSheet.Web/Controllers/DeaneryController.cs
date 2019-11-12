using ExamSheet.Business.Account;
using ExamSheet.Business.Deanery;
using ExamSheet.Web.Attributes;
using ExamSheet.Web.Models;
using System;

namespace ExamSheet.Web.Controllers
{
    [IsInRole(AccountType.Admin)]
    public class DeaneryController : ItemsController<DeaneryModel, DeaneryViewModel>
    {
        public DeaneryController(DeaneryManager manager)
            : base(manager) { }

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