using ExamSheet.Business.Account;
using ExamSheet.Business.Group;
using ExamSheet.Business.Student;
using ExamSheet.Web.Attributes;
using ExamSheet.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace ExamSheet.Web.Controllers
{
    [IsInRole(AccountType.Admin)]
    public class StudentController : ItemsController<StudentModel, StudentViewModel>
    {
        protected GroupManager GroupManager { get; set; }

        //TODO: add filters per group on list page
        public StudentController(StudentManager studentManager, GroupManager groupManager)
            : base(studentManager)
        {
            GroupManager = groupManager;
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