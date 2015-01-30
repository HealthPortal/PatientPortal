using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HealthPortal.Models
{
    interface IWatchListRepository
    {
        IEnumerable<WatchList> GetAll();
        WatchList GetbyId(int id);
        WatchList AddToWatchlist(WatchList wlist);
        void DeletePatient(string userid);
    }
}
