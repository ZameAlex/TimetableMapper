using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TimeTableLibrary.Extensions;
using TimeTableLibrary.FpmRequests;
using TimeTableLibrary.RozkladRequests;
using TimeTableLibrary.Helpers;
using TimeTableLibrary.Helpers.Models;
using Microsoft.CodeAnalysis.Options;
using Microsoft.Extensions.Options;
using CoreUI.Services.Implementation;
using CoreUI.Services.Interfaces;
using TimeTableLibrary.FpmModels;

namespace CoreUI.Controllers
{
    public class SubjectsController : Controller
    {
		FpmClient fpmClient;
		RozkladClient rozkladClient;
		MappingService service;
		SetService<FpmGroup> setService;

		public SubjectsController(FpmClient fpmClient, RozkladClient rozkladClient, MappingService service, SetService<FpmGroup> setService)
		{
			this.rozkladClient = rozkladClient;
			this.fpmClient = fpmClient;
			this.service = service;
			this.setService = setService;
		}

		public IActionResult Index()
		{
			return View();
		}

		[HttpGet]
		public IActionResult SubjectsMapping()
		{
			ViewBag.Subjects = fpmClient.Subjects;
			return View(rozkladClient.Subjects.Select(s=>s.Title).ToList());
		}
		[HttpPost]
		public IActionResult SubjectsMapping(Dictionary<string,string> MappedSubjects)
		{
			service.AddMappedSubjects(MappedSubjects);
			return View();
		}

		public IActionResult AddSubjectsForGroup()
		{
			setService.SetObjects(fpmClient.CurrentGroup, fpmClient.Subjects);
			return View();
		}
	}
}