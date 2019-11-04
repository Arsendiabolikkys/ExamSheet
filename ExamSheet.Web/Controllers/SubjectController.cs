using ExamSheet.Business.Subject;
using ExamSheet.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace ExamSheet.Web.Controllers
{
    //TODO: add Admin access only
    public class SubjectController : Controller
    {
        protected SubjectManager Subject { get; set; }

        public SubjectController(SubjectManager subjectManager)
        {
            Subject = subjectManager;
        }

        public IActionResult Index()
        {
            var subjects = Subject.FindAll().Select(CreateSubjectViewModel).ToList();
            return View(subjects);
        }

        protected virtual SubjectViewModel CreateSubjectViewModel(SubjectModel subject)
        {
            return new SubjectViewModel()
            {
                Id = subject.Id,
                Name = subject.Name
            };
        }

        [HttpGet]
        public IActionResult Edit(string id)
        {
            var model = Subject.GetById(id);
            return View(CreateSubjectViewModel(model));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(SubjectViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            return SaveOrUpdate(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var model = new SubjectViewModel() { Id = Guid.NewGuid().ToString() };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(SubjectViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            return SaveOrUpdate(model);
        }

        protected IActionResult SaveOrUpdate(SubjectViewModel model)
        {
            var subject = new SubjectModel() { Id = model.Id, Name = model.Name };
            Subject.Save(subject);
            return RedirectToAction(nameof(Index));
        }

        //TODO: add remote validation if ExamSheet exist
        public IActionResult Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
                return RedirectToAction(nameof(Index));

            Subject.Remove(id);
            return RedirectToAction(nameof(Index));
        }
    }
}