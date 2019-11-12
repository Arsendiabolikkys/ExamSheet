using ExamSheet.Business.Account;
using ExamSheet.Web.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace ExamSheet.Web.Controllers
{
    [IsInRole(AccountType.Admin)]
    public class StudentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}