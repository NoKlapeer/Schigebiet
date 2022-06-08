using Schigebiet.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Schigebiet.Models
{
    public class Kunde
    {
        private int kundenId;


        public int KundenId
        {
            get { return this.kundenId; }
            set
            {
                if(value >= 0)
                {
                    this.kundenId = value;
                }
            }
        }
        
        public string Name { get; set; }

        public string Password { get; set; }

        public DateTime Birthdate { get; set; }
        
        public string EMail { get; set; }
        public Geschlecht Geschlecht { get; set; }

       

        //public Kunde(int kId, string na, string pw)
        //{
        //    this.KundenId = kId;
        //    this.Name = na;
        //    this.Password = pw;
        //}

    }
}
