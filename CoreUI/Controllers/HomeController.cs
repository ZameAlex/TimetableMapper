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
using TimeTableLibrary.Helpers;
using Microsoft.Extensions.Options;

namespace CoreUI.Controllers
{
	public class HomeController : Controller
	{

		FpmClient fpmClient;
		RozkladClient rozkladClient;
		MappingService service;

		public HomeController(FpmClient fpmClient, RozkladClient rozkladClient, MappingService service)
		{
			this.fpmClient = fpmClient;
			this.rozkladClient = rozkladClient;
			this.service = service;
		}

		[HttpGet]
		public IActionResult Authorization()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Index(FpmUser user)
		{
			fpmClient.User = user;
			await fpmClient.InitRequest();
			await fpmClient.Login();
			await fpmClient.SelectSubjectsAndGroups();
			await fpmClient.SelectTeachers();
			return View("Index", fpmClient.Groups);
		}

		[HttpPost]
		public async Task<IActionResult> GroupSelector(string ID)
		{
			try
			{
				rozkladClient.Group = fpmClient.Groups.FirstOrDefault(group => group.Id == ID).Name;
				await rozkladClient.GetTimetable();
			}
			catch(Exception e)
			{
				return new JsonResult("Error while loading data. Group isn`t exists, or timetable for this group isn`t avalaible.");
			}
			fpmClient.CurrentGroup = fpmClient.Groups.FirstOrDefault(group => group.Id == ID);
			return PartialView("_Menu");
		}
	}
}
