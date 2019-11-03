using System;
using System.Linq;
using ExamSheet.Business.Faculty;
using ExamSheet.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace ExamSheet.Web.Controllers
{
    public class FacultyController : Controller
    {
        protected FacultyManager Faculty { get; set; }

        public FacultyController(FacultyManager facultyManager)
        {
            Faculty = facultyManager;
        }

        public IActionResult Index()
        {
            var faculties = Faculty.FindAll().Select(CreateFacultyViewModel).ToList();
            return View(faculties);
        }

        protected virtual FacultyViewModel CreateFacultyViewModel(FacultyModel faculty)
        {
            return new FacultyViewModel()
            {
                Id = faculty.Id,
                Name = faculty.Name
            };
        }

        [HttpGet]
        public IActionResult Edit(string id)
        {
            var model = Faculty.GetById(id);
            return View(CreateFacultyViewModel(model));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(FacultyViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            return SaveOrUpdate(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var model = new FacultyViewModel() { Id = Guid.NewGuid().ToString() };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(FacultyViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            return SaveOrUpdate(model);
        }

        protected IActionResult SaveOrUpdate(FacultyViewModel model)
        {
            var faculty = new FacultyModel() { Id = model.Id, Name = model.Name };
            Faculty.Save(faculty);
            return RedirectToAction(nameof(Index));
        }
        
        public IActionResult Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
                return RedirectToAction(nameof(Index));

            Faculty.Remove(id);
            return RedirectToAction(nameof(Index));
        }
    }
}