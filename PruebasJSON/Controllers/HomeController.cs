using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PruebasJSON.Models;
using PruebasJSON.Helpers;
using PruebasJSON.Services;
using Microsoft.AspNetCore.Http;
using PruebasJSON.Interfaces;

namespace PruebasJSON.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILookupTableService _lookupService;
        public HomeController(ILookupTableService lookupService)
        {
            _lookupService = lookupService;
        }
        public IActionResult Index()
        {
            dynamic deserealizedJson = _lookupService.GetDeserializedJson();
            _lookupService.GetLookupData();
            return View(deserealizedJson);
        }
        [HttpPost]
        public IActionResult Index(IFormCollection model)
        {
            return View();
            //foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(json))
            //{
            //    string name = descriptor.Name;
            //    object value = descriptor.GetValue(json);
            //    Console.WriteLine("{0}={1}", name, value);
            //}
        }

        public IActionResult Privacy()
        {
            return View(new User());
        }
        [HttpPost]
        public IActionResult Privacy(User model)
        {

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
