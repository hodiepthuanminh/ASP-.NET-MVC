using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Website_BanHang.Models
{
    public class CategoryMasterdata
    {
        [Display(Name ="Mã")]
        public int id { get; set; }
        [Required]

        [Display(Name = "Tên danh mục")]
        public string name { get; set; }

        [Display(Name = "Hình đại diện")]
        public string avatar { get; set; }

        [Display(Name = "Loại phổ biến")]
        public Nullable<int> popularID { get; set; }
    }
}