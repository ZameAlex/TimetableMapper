using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TimeTableLibrary.Client;
using TimeTableLibrary.FpmModels;

namespace TimeTableUI.Controllers
{
    public class GroupsController : Controller
    {
		IFpmClient client;

		public GroupsController(IFpmClient fpmClient)
		{
			client = fpmClient;
		}
        public IActionResult Index()
        {
		
			client.InitRequest().Wait();
			client.Login(new FpmUser("leo", "leoleo")).Wait();
			client.GetSubjectsAndGroups().Wait();
            return View(client.Groups);
        }
    }
}