using System;
using ExamSheet.Business.Account;
using ExamSheet.Business.Group;
using ExamSheet.Web.Attributes;
using ExamSheet.Web.Models;

namespace ExamSheet.Web.Controllers
{
    [IsInRole(AccountType.Admin)]
    public class GroupController : ItemsController<GroupModel, GroupViewModel>
    {
        public GroupController(GroupManager manager)
            : base(manager) { }

        protected override GroupModel CreateModel(GroupViewModel model)
        {
            return new GroupModel()
            {
                Id = model.Id,
                Name = model.Name
            };
        }

        protected override GroupViewModel CreateViewModel(GroupModel model)
        {
            return new GroupViewModel()
            {
                Id = model.Id,
                Name = model.Name 
            };
        }

        protected override GroupViewModel CreateViewModel()
        {
            return new GroupViewModel()
            {
                Id = Guid.NewGuid().ToString()
            };
        }
    }
}