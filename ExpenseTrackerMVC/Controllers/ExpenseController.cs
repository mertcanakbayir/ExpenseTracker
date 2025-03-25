using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTrackerMVC.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ExpenseController : Controller
    {
        public IActionResult AddExpense()
        {
            return View();
        }

        public IActionResult UpdateExpense()
        {
            return View();
        }
    }
}
