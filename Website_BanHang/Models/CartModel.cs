using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Website_BanHang.Context;

namespace Website_BanHang.Models
{
    public class CartModel
    {
        public Product Product { get; set; }
        public int Quanlity { get; set; }
    }
}