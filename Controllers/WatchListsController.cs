using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HealthPortal.Models;
using HealthPortal.Controllers;

namespace HealthPortal.Models
{
    public class WatchListsController : ApiController
    {
        IWatchListRepository repository = new WatchListModel();
        IMyDayRepository mydayrep = new MyDayModel();
        // GET api/values
        [HttpGet]
        public IEnumerable<WatchList> Get()
        {
            return repository.GetAll();
        }

        [HttpGet]
        public WatchList GetbyId(int id)
        {
            WatchList watchlist = repository.GetbyId(id);
            return watchlist;
        }

        [HttpPost]
        public Message AddToWatchlist(string userid)
        {
            var result = new Message();
            try
            {
                 MyDay mday=mydayrep.GetRemovePatient(userid);

                //MyDay mday = mydayrep.GetAll().Where(m => m.PatientId == userid).First();
                WatchList wlist = new WatchList
                {
                    Name = mday.Name,
                    Subject = mday.Subject,
                    PatientId = mday.PatientId,
                    UserId = mday.UserId
                };
                var lastwlistid = repository.GetAll().OrderByDescending(lw => lw.WatchListId).First();
                wlist.WatchListId = Convert.ToInt32(lastwlistid.WatchListId) + 1;
                
                WatchList wlst= repository.AddToWatchlist(wlist);
                if (wlst != null)
                {
                    result.success = "Success.";
                }
                
            }
            catch
            {
                result.error = "Error.";
            }
            return result;
        }

        [HttpDelete]
        public void RemoveWatch(string userid)
        {
            repository.DeletePatient(userid);
        } 
    }
}
