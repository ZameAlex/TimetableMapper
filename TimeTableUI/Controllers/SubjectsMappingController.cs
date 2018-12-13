using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TimeTableLibrary.Client;

namespace TimeTableUI.Controllers
{
    public class SubjectsMappingController : Controller
    {
		IFpmClient client;
		public SubjectsMappingController(IFpmClient fpmClient)
		{
			client = fpmClient;
		}

	public IActionResult Index()
        {
			client.
            return View();
        }
    }
}