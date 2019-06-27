using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TimeTableLibrary.FpmRequests;
using TimeTableLibrary.Helpers;
using TimeTableLibrary.Mappers;
using TimeTableLibrary.RozkladRequests;

namespace CoreUI.Controllers
{
    public class TimetableController : Controller
    {
		FpmClient fpmClient;
		RozkladClient rozkladClient;
		ResultLessonsMapper mapper;
		MappingService mappingService;

		public TimetableController(FpmClient fpmClient, RozkladClient rozkladClient, ResultLessonsMapper resultLessonsMapper, MappingService mappingService)
		{
			this.rozkladClient = rozkladClient;
			this.fpmClient = fpmClient;
			mapper = resultLessonsMapper;
			this.mappingService = mappingService;
		}
		public async Task<IActionResult> Index()
        {
			await fpmClient.TimetableClearRequest();
			await fpmClient.SetTimetableRequest(mapper.Map(rozkladClient.rozkladTimeTable[0], rozkladClient.rozkladTimeTable[1]), mappingService);
			return Json(Ok());
        }
    }
}