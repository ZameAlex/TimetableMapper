﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace TimeTableUI.Controllers
{
    public class TeachersSubjectsLinkingController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}