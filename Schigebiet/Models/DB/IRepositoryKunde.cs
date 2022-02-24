using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Schigebiet.Models.DB
{
    interface IRepositoryKunde
    {
        void Connect();

        void Disconnect();


        bool Delete(int kundenId);


        bool Insert(Kunde kunde);


        bool ChangeUserData(int userId);




    }
}
