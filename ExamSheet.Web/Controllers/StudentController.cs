using ExamSheet.Business.Account;
using ExamSheet.Business.Faculty;
using ExamSheet.Business.Group;
using ExamSheet.Business.Student;
using ExamSheet.Web.Attributes;
using ExamSheet.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ExamSheet.Web.Controllers
{
    [IsInRole(AccountType.Admin)]
    public class StudentController : ItemsController<StudentModel, StudentViewModel>
    {
        protected GroupManager GroupManager { get; set; }

        protected FacultyManager FacultyManager { get; set; }

        protected StudentManager StudentManager { get; set; }
        
        public StudentController(StudentManager studentManager, GroupManager groupManager, FacultyManager facultyManager)
            : base(studentManager)
        {
            GroupManager = groupManager;
            FacultyManager = facultyManager;
            StudentManager = studentManager;
        }

        [NonAction]
        public override IActionResult Index(int page = 1)
        {
            return base.Index(page);
        }

        public virtual IActionResult Index(int page = 1, string facultyId = "", string groupId = "")
        {
            var totalCount = StudentManager.GetTotal(facultyId, groupId);
            var model = CreateItemsViewModel(facultyId, groupId, totalCount, page, PageSize);
            return View(model);
        }

        protected virtual StudentsViewModel CreateItemsViewModel(string facultyId, string groupId, int total, int page, int pageSize)
        {
            var model = new StudentsViewModel();
            model.Page = new PageViewModel(total, page, pageSize);
            model.Items = StudentManager.FindAll(facultyId, groupId, page, pageSize).Select(CreateViewModel).OfType<IItemViewModel>().ToList();
            var faculties = FacultyManager.FindAll().ToList();
            model.Faculties = new List<SelectListItem>() { new SelectListItem() { Value = "", Text = "Не обрано", Selected = string.IsNullOrEmpty(facultyId) } };
            model.Faculties.AddRange(faculties.Select(x => new SelectListItem() { Value = x.Id, Text = x.Name, Selected = x.Id == facultyId }));
            model.FacultyId = facultyId;
            if (!string.IsNullOrEmpty(facultyId))
            {
                var groups = GroupManager.FindAllForFaculty(facultyId);
                model.Groups = new List<SelectListItem>() { new SelectListItem() { Value = "", Text = "Не обрано", Selected = string.IsNullOrEmpty(groupId) } };
                model.Groups.AddRange(groups.Select(x => new SelectListItem() { Value = x.Id, Text = x.Name, Selected = x.Id == groupId }));
                model.GroupId = groupId;
            }
            return model;
        }

        protected virtual void InitGroups()
        {
            var groups = GroupManager.FindAll().Select(CreateGroupModel).ToList();
            ViewData["groups"] = groups;
        }

        protected override void OnEdit()
        {
            InitGroups();
            base.OnEdit();
        }

        protected override void OnCreate()
        {
            InitGroups();
            base.OnCreate();
        }

        protected virtual GroupViewModel CreateGroupModel(GroupModel group)
        {
            return new GroupViewModel()
            {
                Id = group.Id,
                Name = group.Name
            };
        }

        protected override StudentModel CreateModel(StudentViewModel model)
        {
            return new StudentModel()
            {
                Id = model.Id,
                Name = model.Name,
                Surname = model.Surname,
                GroupId = model.GroupId
            };
        }

        protected override StudentViewModel CreateViewModel(StudentModel model)
        {
            return new StudentViewModel()
            {
                Id = model.Id,
                Name = model.Name,
                Surname = model.Surname,
                GroupId = model.GroupId
            };
        }

        protected override StudentViewModel CreateViewModel()
        {
            return new StudentViewModel()
            {
                Id = Guid.NewGuid().ToString()
            };
        }
    }
}