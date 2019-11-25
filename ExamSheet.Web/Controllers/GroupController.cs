using System;
using System.Collections.Generic;
using System.Linq;
using ExamSheet.Business.Account;
using ExamSheet.Business.Faculty;
using ExamSheet.Business.Group;
using ExamSheet.Web.Attributes;
using ExamSheet.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ExamSheet.Web.Controllers
{
    [IsInRole(AccountType.Admin)]
    public class GroupController : ItemsController<GroupModel, GroupViewModel>
    {
        protected GroupManager GroupManager { get; set; }

        protected FacultyManager FacultyManager { get; set; }

        public GroupController(GroupManager manager, FacultyManager facultyManager)
            : base(manager)
        {
            FacultyManager = facultyManager;
            GroupManager = manager;
        }

        [NonAction]
        public override IActionResult Index(int page = 1)
        {
            return base.Index(page);
        }

        public virtual IActionResult Index(int page = 1, string facultyId = "")
        {
            var totalCount = GroupManager.GetTotal(facultyId);
            var model = CreateItemsViewModel(facultyId, totalCount, page, PageSize);
            return View(model);
        }

        protected virtual GroupsViewModel CreateItemsViewModel(string facultyid, int totalCount, int page, int pageSize)
        {
            var model = new GroupsViewModel();
            model.Page = new PageViewModel(totalCount, page, pageSize);
            model.Items = GroupManager.FindAll(facultyid, page, pageSize).Select(CreateViewModel).OfType<IItemViewModel>().ToList();
            var faculties = FacultyManager.FindAll().ToList();
            model.Faculties = new List<SelectListItem>() { new SelectListItem() { Value = "", Text = "Не обрано", Selected = string.IsNullOrEmpty(facultyid) } };
            model.Faculties.AddRange(faculties.Select(x => new SelectListItem() { Value = x.Id, Text = x.Name, Selected = x.Id == facultyid }));
            model.FacultyId = facultyid;
            return model;
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
                Name = model.Name,
                FacultyId = model.FacultyId
            };
        }

        protected override GroupViewModel CreateViewModel(GroupModel model)
        {
            return new GroupViewModel()
            {
                Id = model.Id,
                Name = model.Name,
                FacultyId = model.FacultyId
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