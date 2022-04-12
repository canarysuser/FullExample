using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ProjectsMVCApp.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using ProjectEntities;
using Microsoft.AspNetCore.Http;

namespace ProjectsMVCApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IConfiguration configuration;
        public HomeController(ILogger<HomeController> logger, IConfiguration config)
        {
            _logger = logger;
            configuration = config;
        }
      
        public async Task<IActionResult> Index()
        {

            //var model = await this.GetResponseFromApi<IEnumerable<Employee>>(
            //    baseUri: configuration.GetConnectionString("ProjectsAPIUrl"),
            //    requestUrl: "api/employees");
            var model = await this.SendDataToApi<LoginViewModel, AuthenticatedUser<int>>(
                baseUri: configuration.GetConnectionString("ProjectsAPIUrl"),
                requestUrl: "api/login",
                model: new LoginViewModel { Username = "admin", Password = "admin" }
                );

            HttpContext.Session.SetString("Token", model.Token);
            HttpContext.Session.SetString("RoleName", model.RoleName);
            HttpContext.Session.SetString("Username", model.Name);
            
            return View(model);
        }

        [RoleManager("Admin")]
        public IActionResult Privacy()
        {
            //check whether the session object exists, if yes
            var role = HttpContext.Session.GetString("RoleName");
            if (role != "Admin") return Unauthorized();

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
