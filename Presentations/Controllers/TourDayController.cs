using Microsoft.AspNetCore.Mvc;

namespace Safary.Properties
{
    public class TourDayController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
