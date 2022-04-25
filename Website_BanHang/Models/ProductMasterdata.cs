using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Website_BanHang.Models
{
    public partial class ProductMasterdata
    {
        [Display(Name = "Mã")]
        public int id { get; set; }
        [Required]

        [Display(Name = "Tên sản phẩm")]
        public string name { get; set; }

        [Display(Name = "Hình đại diện")]
        public string avatar { get; set; }

        [Display(Name = "Mã danh mục")]
        public Nullable<int> categoryID { get; set; }

        [Display(Name = "Loại sản phẩm")]
        public Nullable<int> typeID { get; set; }

        [Display(Name = "Mô tả chi tiết")]
        public string fulldescription { get; set; }
        


    }
}