using Microsoft.AspNetCore.Mvc;
using Schigebiet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Schigebiet.Controllers
{
    public class KundeController : Controller
    {
        public IActionResult Index()
        {
            
            return View();
        }
        
        public IActionResult Anmeldung()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Registrierung() {
            return View();
        }

        [HttpPost]
        public IActionResult Registrierung(Kunde userDataFromForm) {
          
            if (userDataFromForm == null)
            {
                
                return RedirectToAction("Registration");
            }

           
            ValidateRegistrationData(userDataFromForm);

            
            if (ModelState.IsValid)
            {
                // TODO: Eingabedaten in einer DB-Tabelle abspeichern

                // zeigen unsere MessageView mit einer entsprechenden Meldung an
                return View("_Message", new Message("Registrierung", "Ihre Daten wurden erfolgreich abgepspeichert"));
            }



            // falls etwas falsch eingegeben wurde wird das Reg-Formular erneut aufgerufen - mit den bereits eingegebenen Daten
            return View(userDataFromForm);
        }

        private void ValidateRegistrationData(Kunde k) {
            // Parameter überprüfen
            if (k == null)
            {
                return;
            }

            // Username
            if ((k.Name == null) || (k.Name.Trim().Length < 4))
            {
                ModelState.AddModelError("Username", "Der Benutzername muss mindestens 4 Zeichen lang sein");
            }

            // Password
            if ((k.Password == null) || (k.Password.Length < 8))
            {
                ModelState.AddModelError("Password", "Das Passwort muss mindestens 8 Zeichen lang sein");
            }
            // + mindestens ein Großbuchstabe + mindestens 1 Kleinbuchstabe + mindestens 1 Sonderzeichen + mindestens 1 Zahl

            // EMail

            // Geburtsdatum 
            if (k.Birthdate >= DateTime.Now)
            {
                ModelState.AddModelError("Birthdate", "Das Geburtsdatum darf sich nicht in der Zukunft befinden!");
            }

            // EMail
            if (!k.EMail.Contains("@"))
            {
                ModelState.AddModelError("EMail", "Die EMail sollte in dem EMail-Format (bsp.: maxmustermann@abc.com)");
            }

            // Gender
            if(k.Geschlecht == null)
            {

            }
        }

    }
}
