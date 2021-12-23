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

        
        [HttpGet]
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
                
                return RedirectToAction("Registrierung");
            }

           
            ValidateRegistrationData(userDataFromForm);

            
            if (ModelState.IsValid)
            {
                return View("_Message", new Message("Registrierung", "Ihre Daten wurden erfolgreich abgepspeichert"));
            }



            
            return View(userDataFromForm);
        }

        private void ValidateRegistrationData(Kunde k) {

            Boolean Kleinbuchstabe = false;
            Boolean Großbuchstabe = false;

            if (k == null)
            {
                return;
            }

            
            if ((k.Name == null) || (k.Name.Trim().Length < 4))
            {
                ModelState.AddModelError("Name", "Der Benutzername muss mindestens 4 Zeichen lang sein");
            }

            
            if ((k.Password == null) || (k.Password.Length < 8))
            {
                ModelState.AddModelError("Password", "Das Passwort muss mindestens 8 Zeichen lang sein");
            }




            if ((k.Password == null) || (k.Password.Length < 8))
            {
                ModelState.AddModelError("Password", "Das Passwort muss mindestens 8 Zeichen lang sein");
            }


            if (k.Birthdate >= DateTime.Now)
            {
                ModelState.AddModelError("Birthdate", "Das Geburtsdatum darf sich nicht in der Zukunft befinden!");
            }


            if ((!k.EMail.Contains("@"))||(k.EMail == null))
            {
                ModelState.AddModelError("EMail", "Die EMail sollte in dem EMail-Format (bsp.: maxmustermann@abc.com)");
            }
        }

        [HttpPost]
        public IActionResult Anmeldung(Kunde kundenDataFromForm)
        {
            if (kundenDataFromForm == null)
            {
                return RedirectToAction("Anmeldung");
            }
            if (ModelState.IsValid)
            {
                return View("_Message", new Message("Anmeldung", "Sie wurden erfolgreich angemeldet!"));
            }
            if (!ModelState.IsValid)
            {
                return View("_Message", new Message("Fehler", "Benutzername oder Password Falsch"));
            }


            return View();
        }

        //private void ValidateLoginData(Kunde k) {
        //    if (((k.Name == null) || (k.Name.Trim().Length < 4)) || ((k.Password == null) || (k.Password.Length < 8)))
        //    {
        //        ModelState.AddModelError("Name", "Fehler");
        //    }
        //}

    }
}
