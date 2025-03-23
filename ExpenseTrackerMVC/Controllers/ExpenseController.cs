using Microsoft.AspNetCore.Mvc;

namespace ExpenseTrackerMVC.Controllers
{
    public class ExpenseController : Controller
    {
        public IActionResult AddExpense()
        {
            return View();
        }
    }
}
