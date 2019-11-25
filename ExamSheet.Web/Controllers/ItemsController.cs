using System.Linq;
using ExamSheet.Business;
using ExamSheet.Business.Account;
using ExamSheet.Web.Attributes;
using ExamSheet.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace ExamSheet.Web.Controllers
{
    [IsInRole(AccountType.Admin)]
    public abstract class ItemsController<T, TView> : Controller
        where T : IItem
        where TView : IItemViewModel
    {
        protected virtual IItemManager<T> ItemManager { get; set; }

        protected virtual int PageSize { get; set; } = 10;

        public ItemsController(IItemManager<T> itemManager)
        {
            ItemManager = itemManager;
        }

        public virtual IActionResult Index(int page = 1)
        {
            var totalCount = ItemManager.GetTotal();
            var model = CreateItemsViewModel(totalCount, page, PageSize);
            return View(model);
        }

        protected virtual ItemsViewModel CreateItemsViewModel(int totalCount, int page, int pageSize)
        {
            var model = new ItemsViewModel();
            var pageModel = new PageViewModel(totalCount, page, pageSize);
            model.Page = pageModel;
            model.Items = ItemManager.FindAll(page, pageSize).Select(CreateViewModel).OfType<IItemViewModel>().ToList();
            return model;
        }

        protected abstract TView CreateViewModel(T model);

        [HttpGet]
        public virtual IActionResult Edit(string id)
        {
            OnEdit();
            var model = ItemManager.GetById(id);
            return View(CreateViewModel(model));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual IActionResult Edit(TView model)
        {
            if (!ModelState.IsValid)
            {
                OnEdit();
                return View(model);
            }

            return SaveOrUpdate(model);
        }

        protected virtual void OnEdit() { }

        [HttpGet]
        public virtual IActionResult Create()
        {
            OnCreate();
            var model = CreateViewModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual IActionResult Create(TView model)
        {
            if (!ModelState.IsValid)
            {
                OnCreate();
                return View(model);
            }

            return SaveOrUpdate(model);
        }

        protected virtual void OnCreate() { }

        protected abstract TView CreateViewModel();

        protected abstract T CreateModel(TView model);

        protected virtual IActionResult SaveOrUpdate(TView model)
        {
            var businessModel = CreateModel(model);
            ItemManager.Save(businessModel);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public virtual IActionResult Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
                return RedirectToAction(nameof(Index));

            ItemManager.Remove(id);
            return Json(1);
        }
    }
}