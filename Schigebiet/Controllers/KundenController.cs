using Microsoft.AspNetCore.Mvc;
using Projekt_Schigebiet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projekt_Schigebiet.Controllers
{
    public class KundenController : Controller
    {
        public IActionResult Index()
        {
            Kunde kunde = new Kunde(0, "Hermann", "Dolm");
            
            return View(kunde);
        }
        public IActionResult Registrierung()
        {
            return View();
        }
        public IActionResult Anmeldung()
        {
            return View();
        }
    }
}
