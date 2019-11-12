using ExamSheet.Business.Account;
using ExamSheet.Business.Subject;
using ExamSheet.Web.Attributes;
using ExamSheet.Web.Models;
using System;

namespace ExamSheet.Web.Controllers
{
    [IsInRole(AccountType.Admin)]
    public class SubjectController : ItemsController<SubjectModel, SubjectViewModel>
    {
        public SubjectController(SubjectManager manager)
            : base(manager) { }

        protected override SubjectModel CreateModel(SubjectViewModel model)
        {
            return new SubjectModel()
            {
                Id = model.Id,
                Name = model.Name
            };
        }

        protected override SubjectViewModel CreateViewModel(SubjectModel model)
        {
            return new SubjectViewModel()
            {
                Id = model.Id,
                Name = model.Name
            };
        }

        protected override SubjectViewModel CreateViewModel()
        {
            return new SubjectViewModel() { Id = Guid.NewGuid().ToString() };
        }
    }
}