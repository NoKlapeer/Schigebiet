using Microsoft.AspNetCore.Mvc;
using Schigebiet.Models;
using Schigebiet.Models.DB;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace Schigebiet.Controllers
{
    public class KundeController : Controller
    {

        private IRepositoryKundeDB rep = new RepositoryKundeDB();

        private bool angemeldet = false;

        public async Task<IActionResult> Index()
        {
            try
            {
                rep.Connect();
                return View(await rep.GetAllKundenAsync());
            }
            catch (DbException)
            {
                return View("_Message", new Message("Datenbankfehler",
                                "Kunden wurden nicht geladen!",
                                "Versuchen Sie es später erneut!"));
            }
            finally
            {
                rep.Disconnect();
            }
        }

        
        [HttpGet]
        public IActionResult Anmeldung()
        {
            return View();
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
                try
                {
                    rep.Connect();
                    if (rep.Login(kundenDataFromForm.Name, kundenDataFromForm.Password))
                    {
                        angemeldet = true;
                        return RedirectToAction("Index","Home");
                    }
                    else
                    {
                        return View("_Message", new Message("Anmeldung", "Anmeldedaten falsch!"));
                    }
                }
                catch (DbException e)
                {
                    return View("_Message",
                                new Message("Anmeldung", "Datenbankfehler!",
                                            "Bitte versuchen Sie es später erneut!"));
                }
                finally
                {
                    rep.Disconnect();
                }
            }

            
            return View(kundenDataFromForm);
        }

        public IActionResult Delete(int id)
        {
            // TODO: User mit der ID id löschen

            return View();
        }

        public IActionResult Update(int id)
        {
            // TODO: User mit der ID id ändern

            return View();
        }


        public IActionResult Abmelden()
        {
            if (angemeldet) {
                angemeldet = false;
                return RedirectToAction("Anmeldung");
            }
            return View("home");
        }

        [HttpGet]
        public IActionResult Registrierung() {
            return View();
        }

        [HttpPost]
        public IActionResult Registrierung(Kunde kundenDataFromForm) {
          
            if (kundenDataFromForm == null)
            {
                return RedirectToAction("Registrierung");
            }

           
            ValidateRegistrationData(kundenDataFromForm);

            
            if (ModelState.IsValid)
            {
                try
                {
                    rep.Connect();
                    if (rep.Insert(kundenDataFromForm))
                    {
                        return View("_Message",
                                new Message("Registrierung", "Daten gespeichert!"));
                    }
                    else
                    {
                        return View("_Message",
                                new Message("Registrierung", "Daten NICHT gespeichert!",
                                            "Bitte versuchen Sie es später erneut!"));
                    }
                }
                catch (DbException e)
                {
                    return View("_Message",
                                new Message("Registrierung", "Datenbankfehler!",
                                            "Bitte versuchen Sie es später erneut!"));
                }
                finally
                {
                    rep.Disconnect();
                }
            }

            return View(kundenDataFromForm);
        }

        private void ValidateRegistrationData(Kunde k) {

            Boolean keinKleinbuchstabe = false;
            Boolean keinGroßbuchstabe = false;

            keinKleinbuchstabe = k.Password.ToUpper().Equals(k.Password);
            keinGroßbuchstabe = k.Password.ToLower().Equals(k.Password);

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


            if ((keinKleinbuchstabe) || (keinGroßbuchstabe))
            {
                ModelState.AddModelError("Password", "Das Passwort muss Groß- und Kleinbuchstaben enthalten");
            }


           if (!(k.Password.Contains("0") || k.Password.Contains("1") || k.Password.Contains("2")|| k.Password.Contains("3")|| k.Password.Contains("4")|| k.Password.Contains("5")|| k.Password.Contains("6")|| k.Password.Contains("7")|| k.Password.Contains("8")|| k.Password.Contains("9")))
           {
                ModelState.AddModelError("Password", "Das Passwort muss mindestens eine Zahl enthalten");
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

        

        //private void ValidateLoginData(Kunde k) {
        //    if (((k.Name == null) || (k.Name.Trim().Length < 4)) || ((k.Password == null) || (k.Password.Length < 8)))
        //    {
        //        ModelState.AddModelError("Name", "Fehler");
        //    }
        //}

    }
}
