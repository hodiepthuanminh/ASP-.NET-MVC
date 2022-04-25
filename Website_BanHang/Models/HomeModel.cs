using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Website_BanHang.Context;

namespace Website_BanHang.Models
{
    public class HomeModel
    {
        public List<Category> lstCategories { get; set; }
        public List<Product> lstProducts { get; set; }
    }
}