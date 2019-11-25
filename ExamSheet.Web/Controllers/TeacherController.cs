using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExamSheet.Business.Account;
using ExamSheet.Business.Teacher;
using ExamSheet.Web.Attributes;
using ExamSheet.Web.Models;

namespace ExamSheet.Web.Controllers
{
    [IsInRole(AccountType.Admin)]
    public class TeacherController : ItemsController<TeacherModel, TeacherViewModel>
    {
        public TeacherController(TeacherManager manager)
            : base(manager) { }

        protected override TeacherModel CreateModel(TeacherViewModel model)
        {
            return new TeacherModel()
            {
                Id = model.Id,
                Name = model.Name,
                Surname = model.Surname
            };
        }

        protected override TeacherViewModel CreateViewModel(TeacherModel model)
        {
            return new TeacherViewModel()
            {
                Id = model.Id,
                Name = model.Name,
                Surname = model.Surname
            };
        }

        protected override TeacherViewModel CreateViewModel()
        {
            return new TeacherViewModel()
            {
                Id = Guid.NewGuid().ToString()
            };
        }
    }
}