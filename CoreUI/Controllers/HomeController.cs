using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CoreUI.Models;
using TimeTableLibrary.FpmModels;
using TimeTableLibrary.FpmRequests;
using TimeTableLibrary.RozkladRequests;

namespace CoreUI.Controllers
{
	public class HomeController : Controller
	{

		FpmClient fpmClient;
		RozkladClient rozkladClient;

		[HttpGet]
		public IActionResult Authorization()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Index(FpmUser user)
		{
			return View();
		}
	}
}
