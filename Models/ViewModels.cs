using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HealthPortal.Models
{
    [MetadataType(typeof(UserProfile))]
    public partial class UserInfo
    {
        internal class UserProfile
        {
            [Key]
            public int UserId { get; set; }
            public string UserName { get; set; }
        }
    }
}