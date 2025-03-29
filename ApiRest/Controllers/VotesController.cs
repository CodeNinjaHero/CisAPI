using Microsoft.AspNetCore.Mvc;

namespace ApiRest.Controllers
{
    public class VotesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
