﻿using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;

namespace AspDotNetCoreMVC.MovieApp.Controllers
{
    public class HelloWorldController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        /*public string Index()
        {
            return "This is my default action.";
        }*/

        public IActionResult Welcome(string name, int numTimes = 5)
        {
            //return "This is Welcome action method..";
            //return HtmlEncoder.Default.Encode($"Hello {name}, ID is {ID}");
            ViewData["Message"] = "hello " + name;
            ViewData["NumTimes"] = numTimes;
            return View();
        }
    }
}
