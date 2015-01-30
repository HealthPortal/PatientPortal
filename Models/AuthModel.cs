using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HealthPortal.Models
{
    public class AuthModel
    {
        public Int32 Authuserid { get; set; }
        public string UserAdmin { get; set; }
        public string UserAccess { get; set; }
        public string EmailId { get; set; }
        public string Verification { get; set; }
        public string AccessType { get; set; }
    }
}