using Microsoft.AspNetCore.Mvc;

public class AuthController : Controller
{
    public IActionResult Login()
    {
        ViewBag.HideNavbar = true;
        return View();
    }

    public IActionResult Logout()
    {
        Response.Cookies.Delete("AuthToken");
        return RedirectToAction("Login");
    }

    public IActionResult Register()
    {
        ViewBag.HideNavbar = true;
        return View();
    }
}
