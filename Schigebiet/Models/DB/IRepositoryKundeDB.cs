using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Schigebiet.Models.DB
{
    interface IRepositoryKundeDB
    {
        void Connect();

        void Disconnect();


        bool Delete(int kundenId);


        bool Insert(Kunde kunde);

        Kunde GetKunde(int userId);

        List<Kunde> GetAllKunden();

        bool ChangeUserData(int userId);




    }
}
