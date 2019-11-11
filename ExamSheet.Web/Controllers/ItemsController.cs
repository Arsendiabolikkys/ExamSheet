using System.Linq;
using ExamSheet.Business;
using ExamSheet.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace ExamSheet.Web.Controllers
{
    public abstract class ItemsController<T, TView> : Controller
        where T : IItem
        where TView : IItemViewModel
    {
        protected virtual IItemManager<T> ItemManager { get; set; }

        public ItemsController(IItemManager<T> itemManager)
        {
            ItemManager = itemManager;
        }

        public virtual IActionResult Index()
        {
            var items = ItemManager.FindAll().Select(CreateViewModel).ToList();
            return View(items);
        }

        protected abstract TView CreateViewModel(T model);

        [HttpGet]
        public virtual IActionResult Edit(string id)
        {
            var model = ItemManager.GetById(id);
            return View(CreateViewModel(model));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual IActionResult Edit(TView model)
        {
            if (!ModelState.IsValid)
                return View(model);

            return SaveOrUpdate(model);
        }

        [HttpGet]
        public virtual IActionResult Create()
        {
            var model = CreateViewModel();
            return View(model);
        }

        protected abstract TView CreateViewModel();

        protected abstract T CreateModel(TView model);

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual IActionResult Create(TView model)
        {
            if (!ModelState.IsValid)
                return View(model);

            return SaveOrUpdate(model);
        }

        protected virtual IActionResult SaveOrUpdate(TView model)
        {
            var businessModel = CreateModel(model);
            ItemManager.Save(businessModel);
            return RedirectToAction(nameof(Index));
        }

        public virtual IActionResult Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
                return RedirectToAction(nameof(Index));

            ItemManager.Remove(id);
            return RedirectToAction(nameof(Index));
        }
    }
}