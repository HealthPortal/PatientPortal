using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HealthPortal.Models;
namespace HealthPortal.Controllers
{
   public class GetAccessUsers
    {
       WallEntities db = new WallEntities();
       public List<AuthenticationUser> GetAccUsers(string userId)
       {
           List<AuthenticationUser> autuser = db.AuthenticationUsers.Where(a => a.UserAdmin.Contains(userId)).ToList();
           if (autuser.Any())
           {
               return autuser;
           }
           else
           {
               return null;
           }
       }
       public List<AuthenticationUser> GetAccessAccept(string useraccessid) 
       {
           List<AuthenticationUser> autuser = db.AuthenticationUsers.Where(a => a.UserAccess.Contains(useraccessid) && a.Verification == "Verify").ToList();
           if (autuser.Any())
           {
               return autuser;
           }
           else
           {
               return null;
           }
       }


    }
}
