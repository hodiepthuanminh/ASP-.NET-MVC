using System;
using System.Collections.Generic;

using System.Linq;
using System.Web;

namespace Website_BanHang.Models
{
    public class UserMasterdata
    {
        public int id { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public Nullable<bool> isAdmin { get; set; }
    }
}