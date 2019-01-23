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

		public HomeController(FpmClient fpmClient, RozkladClient rozkladClient)
		{
			this.fpmClient = fpmClient;
			this.rozkladClient = rozkladClient;
			Requests();
		}

		[HttpGet]
		public IActionResult Authorization()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> IndexAsync(FpmUser user)
		{
			fpmClient.User = user;
			await fpmClient.Login();
			await fpmClient.SelectSubjectsAndGroups();
			await fpmClient.SelectTeachers();
			return View("Index", fpmClient.Groups);
		}

		[HttpPost]
		public void GroupSelector(FpmGroup group)
		{

		}

		#region Helper methods
		private async void Requests()
		{
			await fpmClient.InitRequest();
		}
		#endregion Helper methods
	}
}
