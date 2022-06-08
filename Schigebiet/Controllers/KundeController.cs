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
                        return RedirectToAction("Index", "Home");
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


        [HttpGet]
        public IActionResult Delete(int id)
        {
            try
            {
                rep.Connect();
                rep.Delete(id);
                return RedirectToAction("Index");
            }
            catch (DbException)
            {
                return View("_Message", new Message("Datenbankfehler!", "Der Benutzer konnte nicht gelöscht werden! Versuchen sie es später erneut."));
            }
            finally
            {
                rep.Disconnect();
            }
        }


        [HttpGet]
        public IActionResult Update(int id)
        {
            try
            {
                rep.Connect();
                // Liste mit allen Usern an die View übergeben
                return View(rep.GetKunde(id));
            }
            catch (DbException)
            {
                return View("_Message", new Message("Datenbankfehler", "Der Kunde konnten nicht geladen werdne",
                    "Versuchen sie es später erneut"));
            }
            finally
            {
                rep.Disconnect();
            }
            return View();
        }

        [HttpPost]
        public IActionResult Update(Kunde k, int kundenid)
        {
            
            if (k == null)
            {
                return RedirectToAction("update");
            }
            if (ModelState.IsValid)
            {
                try
                {
                    rep.Connect();
                    if (rep.ChangeKundenData(kundenid, k))
                    {
                        return View("_Message",
                            new Message("Update", "Ihre Daten wurden erfolgreich geupdatet"));
                    }
                    else
                    {
                        return View("_Message",
                            new Message("Update", "Ihre Daten konnten NICHT geupdatet werden!",
                                        "Bitte versuchen Sie es später erneut!"));
                    }
                }
                catch (DbException)
                {
                    return View("_Message",
                        new Message("Update", "Datenbankfehler!",
                                    "Bitte veruschen sie es später erneut!"));

                }
                finally
                {
                    rep.Disconnect();
                }
                //falls etwas falsch eingeg. wurde, wird das Reg-formular
                // erneut aufgerufen - mit dne bereits eingegebnenen Daten.
            }
            return View(k);
        }

        public IActionResult Abmelden()
        {
            if (RepositoryKundeDB.logged)
            {
                RepositoryKundeDB.logged = false;
                RepositoryKundeDB.isAdmin = false;
                return RedirectToAction("Anmeldung");
            }
            else
            {
                return View("_Message", new Message("Abmeldung", "Sie sind nicht angemeldet!"));
            }
        }

        



        public IActionResult checkEMail(string email)
        {
            try
            {
                rep.Connect();
                if (rep.AskEmail(email))
                {
                    return new JsonResult(true);
                }
                else
                {
                    return new JsonResult(false);
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



        [HttpGet]
        public IActionResult Registrierung()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Registrierung(Kunde kundenDataFromForm)
        {

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

        private void ValidateRegistrationData(Kunde k)
        {

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


            if (!(k.Password.Contains("0") || k.Password.Contains("1") || k.Password.Contains("2") || k.Password.Contains("3") || k.Password.Contains("4") || k.Password.Contains("5") || k.Password.Contains("6") || k.Password.Contains("7") || k.Password.Contains("8") || k.Password.Contains("9")))
            {
                ModelState.AddModelError("Password", "Das Passwort muss mindestens eine Zahl enthalten");
            }



            if (k.Birthdate >= DateTime.Now)
            {
                ModelState.AddModelError("Birthdate", "Das Geburtsdatum darf sich nicht in der Zukunft befinden!");
            }


            if ((!k.EMail.Contains("@")) || (k.EMail == null))
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
