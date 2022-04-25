using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using Website_BanHang.Context;

using static Website_BanHang.Common;

namespace Website_BanHang.Areas.Admin.Controllers
{
    public class ProductADController : Controller
    {
        Website_BanHangEntities objWebASP = new Website_BanHangEntities();
        // GET: Admin/ProductAD
        public ActionResult Index(string SearchString, string currentFilter, int? page)
            {
            var lstProduct = new List<Product>();
            if (SearchString != null)
            {
                page = 1;
            }
            else
            {
                SearchString = currentFilter;
            }
            if (!string.IsNullOrEmpty(SearchString))
            {
                lstProduct = objWebASP.Products.Where(n => n.name.Contains(SearchString)).ToList();
            }
            else
            {
                lstProduct = objWebASP.Products.ToList();
            }
            ViewBag.CurrentFilter = SearchString;
            int pageSize = 4;
            int pageNumber = (page ?? 1);
            //Sắp xếp theo id sản phảm, sản phẩm mới đưa lên đầu
            lstProduct = lstProduct.OrderByDescending(n => n.id).ToList();
            return View(lstProduct.ToPagedList(pageNumber, pageSize));
        }
        [HttpGet]
        public ActionResult Create()
        {
            this.LoadData();
            return View();
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Create(Product objProduct)
        {
            this.LoadData();
            if (ModelState.IsValid)
            {
                try
                {
                    if (objProduct.ImageUpload != null)
                    {
                        string fileName = Path.GetFileNameWithoutExtension(objProduct.ImageUpload.FileName);
                        string extension = Path.GetExtension(objProduct.ImageUpload.FileName);
                        fileName = fileName + "_" + long.Parse(DateTime.Now.ToString("yyyyyMMddhhmmss")) + extension;
                        objProduct.avatar = fileName;
                        objProduct.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/images/product/"), fileName));
                        objWebASP.Products.Add(objProduct);
                        objWebASP.SaveChanges();
                        return RedirectToAction("Index");
                    }

                }
                catch (Exception)
                {

                    return RedirectToAction("Index");
                }
            }
            return View(objProduct);
            
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var objProduct = objWebASP.Products.Where(n => n.id == id).FirstOrDefault();
            return View(objProduct);
        }
        [HttpPost]
        public ActionResult Delete(Context.Product objPro)
        {
            var objProduct = objWebASP.Products.Where(n => n.id == objPro.id).FirstOrDefault();
            objWebASP.Products.Remove(objProduct);
            objWebASP.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            this.LoadData();
            var objProduct = objWebASP.Products.Where(n => n.id == id).FirstOrDefault();
            return View(objProduct);
        }

        [HttpPost]
        public ActionResult Edit(Product objProduct)
        {
            this.LoadData();
            if (objProduct.ImageUpload != null)
            {
                string fileName = Path.GetFileNameWithoutExtension(objProduct.ImageUpload.FileName);
                string extension = Path.GetExtension(objProduct.ImageUpload.FileName);
                fileName = fileName + "_" + long.Parse(DateTime.Now.ToString("yyyyyMMddhhmmss")) + extension;
                objProduct.avatar = fileName;
                objProduct.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/images/product/"), fileName));
                
            }
            objWebASP.Entry(objProduct).State = EntityState.Modified;
            objWebASP.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Detail(int id)
        {
            var objProduct = objWebASP.Products.Where(n => n.id == id).FirstOrDefault();
            return View(objProduct);
        }
        void LoadData()
        {
            Common objCommon = new Common();
            var lstCat = objWebASP.Categories.ToList();
            ListtoDataTableConverter converter = new ListtoDataTableConverter();
            DataTable dtCategory = converter.toDataTable(lstCat);
            ViewBag.ListCategory = objCommon.ToSelectList(dtCategory, "id", "name");

            //Loại sản phẩm
            List<ProductType> lstProductType = new List<ProductType>();
            ProductType objProductType = new ProductType();
            objProductType.Id = 1;
            objProductType.Name = "Giảm giá sốc";
            lstProductType.Add(objProductType);

            objProductType = new ProductType();
            objProductType.Id = 2;
            objProductType.Name = "Đề xuất";
            lstProductType.Add(objProductType);
            DataTable dtProductType = converter.toDataTable(lstProductType);
            ViewBag.ProductType = objCommon.ToSelectList(dtProductType, "Id", "Name");
        }
    }
}