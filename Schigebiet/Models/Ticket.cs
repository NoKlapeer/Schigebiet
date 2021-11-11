using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projekt_Schigebiet.Models
{
    public class Ticket
    {
        private int ticketnr;
        private decimal preis;
        

        public int Ticketnr
        {
            get { return this.ticketnr; }
            set
            {
                if(value > 0)
                {
                    this.ticketnr = value;
                }
            }
        }

        public decimal Preis {
            get { return this.preis; }
            set {
                if(value > 0)
                {
                    this.preis = value;
                }
            }
        }

        public TicketArt ticketArt { get; set; }


    }
}
