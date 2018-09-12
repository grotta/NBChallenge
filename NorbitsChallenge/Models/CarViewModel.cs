using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NorbitsChallenge.Bll;
using NorbitsChallenge.Dal;

namespace NorbitsChallenge.Models
{
    public class CarViewModel
    {
        public Car car { get; set; }
        public string companyName { get; set; }
    }
}
