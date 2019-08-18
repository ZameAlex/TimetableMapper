using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TimeTableLibrary.Extensions;
using TimeTableLibrary.FpmRequests;
using TimeTableLibrary.RozkladRequests;
using TimeTableLibrary.Helpers;
using Microsoft.CodeAnalysis.Options;
using Microsoft.Extensions.Options;
using CoreUI.Services.Implementation;
using CoreUI.Services.Interfaces;
using TimeTableLibrary.FpmModels;
using TimeTableLibrary.Mappers.Interfaces;

namespace CoreUI.Controllers
{
    public class SubjectsController : Controller
    {
		FpmClient fpmClient;
		RozkladClient rozkladClient;
		MappingService service;
		SetService<FpmGroup> setService;
		IMapper<string, FpmSubject> subjectMapper;

		public SubjectsController(FpmClient fpmClient, RozkladClient rozkladClient, MappingService service, SetService<FpmGroup> setService, IMapper<string, FpmSubject> subjectMapper)
		{
			this.rozkladClient = rozkladClient;
			this.fpmClient = fpmClient;
			this.service = service;
			this.setService = setService;
			this.subjectMapper = subjectMapper;
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
			return View();
		}

		public IActionResult AddSubjectsForGroup()
		{
			setService.SetObjects(fpmClient.CurrentGroup, subjectMapper.Map(rozkladClient.Subjects.Select(s => s.Title)).ToList());
			return View("Index");
		}
	}
}