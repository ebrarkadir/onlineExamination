using Microsoft.AspNetCore.Mvc;

namespace OnlineExamination.Web.Controllers
{
    public class QnAsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
