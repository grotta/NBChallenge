using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NorbitsChallenge.Dal;
using NorbitsChallenge.Helpers;
using NorbitsChallenge.Models;

namespace NorbitsChallenge.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration _config;

        public HomeController(IConfiguration config)
        {
            _config = config;
        }

        public IActionResult Index()
        {
            var model = GetCompanyModel();
            return View(model);
        }
        public IActionResult ChangeCompany(int newUser) {
            UserHelper.setLoggedOnUserCompanyId(newUser);
            var model = GetCompanyModel();
            return RedirectToAction("Index");
        }
        public IActionResult AddCarToTable(string newLicense, int newTireCount, int companyId, string model, string brand, string description) {
            new CarDb(_config).addCarToDB(newLicense,newTireCount,companyId,model,brand,description);
            return RedirectToAction("Index");
        }
        public IActionResult RemoveCarFromTable(string carLicense) {
            new CarDb(_config).removeCarFromDB(carLicense);
            return RedirectToAction("Index");
        }
        public IActionResult UpdateCarInfo(int newTireCount, string carLicense, string model, string brand, string description) {
            new CarDb(_config).updateCar(newTireCount,carLicense,model,brand,description);
            return RedirectToAction("Index");
        }
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private HomeModel GetCompanyModel()
        {
            var companyId = UserHelper.GetLoggedOnUserCompanyId();
            var companyName = new SettingsDb(_config).GetCompanyName(companyId);
            return new HomeModel { CompanyId = companyId, CompanyName = companyName };
        }
    }
}
