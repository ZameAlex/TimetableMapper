﻿using System;
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
		////IFpmClient client;

		//public GroupsController(//IFpmClient fpmClient)
		//{
		//	client = fpmClient;
		//}
		//[HttpGet]
  //      public IActionResult Index()
  //      {
		//	client.GetSubjectsAndGroups().Wait();
  //          return View(client.Groups);
  //      }

		//[HttpPost]
		//public IActionResult GroupMenu(string id)
		//{
		//	var group = client.Groups.Where(g => g.Id == id).First();
		//	client.CurrentGroup = group;
		//	ViewBag.Name = client.CurrentGroup.Name;
		//	return View();
		//}
    }
}