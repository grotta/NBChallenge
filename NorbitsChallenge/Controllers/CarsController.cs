using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NorbitsChallenge.Dal;
using NorbitsChallenge.Models;

namespace NorbitsChallenge.Controllers
{
    public class CarsController : Controller
    {
        private readonly IConfiguration _config;

        public CarsController(IConfiguration config) {
            _config = config;
        }
        public IActionResult CarViewList(int companyId)
        {
            var model = new CarsViewModel();
            var cars = new CarDb(_config).getCars(companyId);
            var companyNameFromDB = new SettingsDb(_config).GetCompanyName(companyId);
            model.Cars = cars;
            model.companyName = companyNameFromDB;
            
            return View(model);
        }
        public IActionResult CarView(string licensePlate)
        {
            bool carExistsInDb = new CarDb(_config).carExists(licensePlate);
            if (carExistsInDb){
                var model = new CarViewModel();
                var newCar = new CarDb(_config).getCar(licensePlate);
                var companyNameFromDB = new SettingsDb(_config).GetCompanyName(newCar.CompanyId);
                model.car = newCar;
                model.companyName = companyNameFromDB;

                return View(model);
            }
            else {
                return RedirectToAction("Index", "Home");
            }
        }
    }
        
    }