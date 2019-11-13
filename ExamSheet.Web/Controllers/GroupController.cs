using System;
using System.Linq;
using ExamSheet.Business.Account;
using ExamSheet.Business.Faculty;
using ExamSheet.Business.Group;
using ExamSheet.Web.Attributes;
using ExamSheet.Web.Models;

namespace ExamSheet.Web.Controllers
{
    [IsInRole(AccountType.Admin)]
    public class GroupController : ItemsController<GroupModel, GroupViewModel>
    {
        protected FacultyManager FacultyManager { get; set; }

        public GroupController(GroupManager manager, FacultyManager facultyManager)
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