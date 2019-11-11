using ExamSheet.Business.Account;
using ExamSheet.Business.Deanery;
using ExamSheet.Business.Teacher;
using ExamSheet.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Cryptography;

namespace ExamSheet.Web.Controllers
{
    public class AccountController : Controller
    {
        protected AccountManager AccountManager { get; set; }

        protected TeacherManager TeacherManager { get; set; }

        protected DeaneryManager DeaneryManager { get; set; }

        public AccountController(AccountManager accountManager, TeacherManager teacherManager, DeaneryManager deaneryManager)
        {
            AccountManager = accountManager;
            TeacherManager = teacherManager;
            DeaneryManager = deaneryManager;
        }

        public IActionResult Index()
        {
            var accounts = AccountManager.FindAll().Select(x => CreateAccountViewModel(x)).ToList();
            return View(accounts);
        }

        public IActionResult IsUniqueEmailAddress(string email)
        {
            var account = AccountManager.GetByEmail(email);
            return Json(account == null);
        }

        //TODO: is unique reference id

        public IActionResult GetReferences()
        {
            return Json(0);
        }

        [HttpGet]
        public IActionResult Edit(string id)
        {
            var model = AccountManager.GetById(id);
            return View(CreateAccountViewModel(model, true));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(AccountViewModel model)
        {
            if (!ModelState.IsValid)
            {
                InitAccountReferences(model);
                return View(model);
            }

            return SaveOrUpdate(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var model = new AccountViewModel() { Id = Guid.NewGuid().ToString() };
            InitAccountReferences(model);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(AccountViewModel model)
        {
            if (!ModelState.IsValid)
            {
                InitAccountReferences(model);
                return View(model);
            }

            return SaveOrUpdate(model);
        }

        protected IActionResult SaveOrUpdate(AccountViewModel model)
        {
            byte[] saltBytes;
            new RNGCryptoServiceProvider().GetBytes(saltBytes = new byte[16]);
            var cryptedPassword = new Rfc2898DeriveBytes(model.Password, saltBytes);
            var passwordBytes = cryptedPassword.GetBytes(20);

            var password = Convert.ToBase64String(passwordBytes);
            var salt = Convert.ToBase64String(saltBytes);
            var account = new AccountModel()
            {
                Id = model.Id,
                Email = model.Email,
                AccountType = (AccountType)model.AccountType,
                ReferenceId = model.ReferenceId,
                PasswordHash = password,
                Salt = salt
            };
            AccountManager.Save(account);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
                return RedirectToAction(nameof(Index));

            AccountManager.Remove(id);
            return RedirectToAction(nameof(Index));
        }

        protected virtual AccountViewModel CreateAccountViewModel(AccountModel account, bool initReferences = false)
        {
            var accountViewModel = new AccountViewModel()
            {
                Id = account.Id,
                Email = account.Email,
                ReferenceId = account.ReferenceId,
                AccountType = (AccountTypeView)account.AccountType
            };
            if (initReferences)
                InitAccountReferences(accountViewModel);

            return accountViewModel;
        }

        protected virtual void InitAccountReferences(AccountViewModel model)
        {
            model.Teachers = TeacherManager.FindAll().Select(CreateTeacherViewModel).ToList();
            model.Deaneries = DeaneryManager.FindAll().Select(CreateDeaneryViewModel).ToList();
        }

        protected virtual DeaneryViewModel CreateDeaneryViewModel(DeaneryModel deanery)
        {
            return new DeaneryViewModel()
            {
                Id = deanery.Id,
                Name = deanery.Name
            };
        }

        protected virtual TeacherViewModel CreateTeacherViewModel(TeacherModel teacher)
        {
            return new TeacherViewModel()
            {
                Id = teacher.Id,
                Name = teacher.Name,
                Surname = teacher.Surname
            };
        }
    }
}