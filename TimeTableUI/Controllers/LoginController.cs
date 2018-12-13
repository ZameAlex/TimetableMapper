using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TimeTableLibrary.Client;
using TimeTableLibrary.FpmModels;
using TimeTableUI.Models.TimeTableModels.FPM;

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
		public IActionResult Create(User user)
		{
			client.InitRequest();
			client.Login(new FpmUser(user.Login, user.Password));
			return RedirectToAction("Index", "Dashboard");
		}
    }
}