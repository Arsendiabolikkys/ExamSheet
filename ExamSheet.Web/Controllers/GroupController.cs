using System;
using System.Linq;
using ExamSheet.Business.Group;
using ExamSheet.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace ExamSheet.Web.Controllers
{
    public class GroupController : Controller
    {
        protected GroupManager Group { get; set; }

        public GroupController(GroupManager groupManager)
        {
            Group = groupManager;
        }

        public IActionResult Index()
        {
            var groups = Group.FindAll().Select(CreateGroupViewModel).ToList();
            return View(groups);
        }

        protected virtual GroupViewModel CreateGroupViewModel(GroupModel group)
        {
            return new GroupViewModel()
            {
                Id = group.Id,
                Name = group.Name
            };
        }

        [HttpGet]
        public IActionResult Edit(string id)
        {
            var model = Group.GetById(id);
            return View(CreateGroupViewModel(model));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(GroupViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            return SaveOrUpdate(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var model = new GroupViewModel() { Id = Guid.NewGuid().ToString() };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(GroupViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            return SaveOrUpdate(model);
        }

        protected IActionResult SaveOrUpdate(GroupViewModel model)
        {
            var group = new GroupModel() { Id = model.Id, Name = model.Name };
            Group.Save(group);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
                return RedirectToAction(nameof(Index));

            Group.Remove(id);
            return RedirectToAction(nameof(Index));
        }
    }
}