using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Website_BanHang.Models;

namespace Website_BanHang.Context
{
    [MetadataType(typeof(ProductMasterdata))]
    public partial class Product
    {
          [NotMapped]
          public System.Web.HttpPostedFileBase ImageUpload { get; set; }
        
    }

    [MetadataType(typeof(CategoryMasterdata))]
    public partial class Category
    {
        [NotMapped]
        public System.Web.HttpPostedFileBase ImageUpload { get; set; }

    }
}