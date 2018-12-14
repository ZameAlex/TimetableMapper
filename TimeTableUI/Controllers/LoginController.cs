using Microsoft.AspNetCore.Mvc;
using TimeTableLibrary.Client;
using TimeTableLibrary.FpmModels;

namespace TimeTableUI.Controllers
{
	public class LoginController : Controller
    {
		readonly IFpmClient client;
		public LoginController(IFpmClient fpmClient)
		{
			client = fpmClient;
		}
		[HttpGet]
        public IActionResult Index()
        {
            return View();
        }

		[HttpPost]
		public IActionResult Create(FpmUser user)
		{
			client.InitRequest();
			client.User = user;
			client.Login();
			return RedirectToAction("Index", "Groups");
		}
    }
}