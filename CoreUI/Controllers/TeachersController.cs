using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreUI.Services.Implementation;
using Microsoft.AspNetCore.Mvc;
using TimeTableLibrary.FpmRequests;
using TimeTableLibrary.Helpers;
using TimeTableLibrary.RozkladRequests;
using TimeTableLibrary.Mappers.FpmMappers;
using TimeTableLibrary.Mappers.RozkladMappers;
using CoreUI.Services.Interfaces;
using TimeTableLibrary.FpmModels;
using TimeTableLibrary.Mappers.Interfaces;
using TimeTableLibrary.RozkladModels;

namespace CoreUI.Controllers
{
    public class TeachersController : Controller
    {
		FpmClient fpmClient;
		RozkladClient rozkladClient;
		MappingService service;
		SetService<FpmTeacher> setService;
		IMapper<string, FpmTeacher> teacherMapper;
		IMapper<string, FpmSubject> subjectMapper;

		public TeachersController(FpmClient fpmClient, RozkladClient rozkladClient, MappingService service, SetService<FpmTeacher> setService, IMapper<string, FpmTeacher> teacherMapper, IMapper<string, FpmSubject> subjectMapper)
		{
			this.rozkladClient = rozkladClient;
			this.fpmClient = fpmClient;
			this.service = service;
			this.setService = setService;
			this.teacherMapper = teacherMapper;
			this.subjectMapper = subjectMapper;
		}

		public IActionResult Index()
        {
            return View();
        }

		[HttpGet]
		public IActionResult TeachersMapping()
		{
			ViewBag.Teachers = fpmClient.Teachers;
			return View(rozkladClient.Teachers.Select(s => s.Name).ToList());
		}
		[HttpPost]
		public IActionResult TeachersMapping(Dictionary<string, string> MappedTeachers)
		{
			service.AddMappedTeachers(MappedTeachers);
			return View();
		}

		public IActionResult AddTeachersForSubject()
		{
			foreach (var item in rozkladClient.Teachers)
			{
				setService.SetObjects(service.Teachers[item.Name], subjectMapper.Map(rozkladClient.GetTeachersBySubject()[item.Name].Select(s => s.Title)).ToList());
			}
			return View();
		}
	}
}