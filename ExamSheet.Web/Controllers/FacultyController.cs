using System;
using ExamSheet.Business.Account;
using ExamSheet.Business.Faculty;
using ExamSheet.Web.Attributes;
using ExamSheet.Web.Models;

namespace ExamSheet.Web.Controllers
{
    [IsInRole(AccountType.Admin)]
    public class FacultyController : ItemsController<FacultyModel, FacultyViewModel>
    {
        public FacultyController(FacultyManager manager)
            : base(manager) { }

        protected override FacultyModel CreateModel(FacultyViewModel model)
        {
            return new FacultyModel()
            {
                Id = model.Id,
                Name = model.Name
            };
        }

        protected override FacultyViewModel CreateViewModel(FacultyModel model)
        {
            return new FacultyViewModel()
            {
                Id = model.Id,
                Name = model.Name
            };
        }

        protected override FacultyViewModel CreateViewModel()
        {
            return new FacultyViewModel()
            {
                Id = Guid.NewGuid().ToString()
            };
        }
    }
}