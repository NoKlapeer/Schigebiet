using Schigebiet.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Schigebiet.Controllers
{
    public class TicketController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Verkauf() {
            List<Ticket> ti = Tickets();
            return View(ti);
        }

        private List<Ticket> Tickets() {
            return new List<Ticket>()
            {
                new Ticket(){ Ticketnr  = 60,  Preis= 20, KaufZeitpunkt = new DateTime(2021, 10, 26), TicketArt = TicketArt.KindTag },
                new Ticket(){ Ticketnr = 61, Preis = 30, KaufZeitpunkt = new DateTime(2021, 11, 27), TicketArt = TicketArt.ErwTag },
            };
        }
    }
}
