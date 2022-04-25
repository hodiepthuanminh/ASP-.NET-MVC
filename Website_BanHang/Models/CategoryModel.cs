using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Website_BanHang.Context;

namespace Website_BanHang.Models
{
    public class CategoryModel
    {
        public List<Category> lstCatgories { get; set; }
        public List<Product> lstProduct { get; set; }
    }
}