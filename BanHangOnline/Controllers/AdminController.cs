using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BanHangOnline.Models;
using PagedList;
using System.IO;
using PagedList.Mvc;
using System.Data.Entity;

namespace BanHangOnline.Controllers
{
    public class AdminController : Controller
    {
        DBShopBanHangEntities db = new DBShopBanHangEntities();
        // GET: Admin
        public ActionResult Index()
        {

            return View();
        }
        public ActionResult DanhMuc(int? page)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(db.type_product.ToList().ToPagedList(pageNumber, pageSize));
        }
        [HttpGet]
        public ActionResult SanPham(int? page, int? id_type_product)
        {
            //ViewBag.ListTypeProduct = new SelectList(db.type_product, "id_type_product", "name_type_product");
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            //List<type_product> elm = null;
            IEnumerable<product> lstSP = null;
            if (id_type_product == null)
            {
                lstSP = db.products.ToList().OrderBy(n => n.id_type_product);
                //ViewBag.ListTypeProduct = db.type_product.ToList();
            }
            else
            {
                lstSP = db.products.Where(n => n.id_type_product == id_type_product).ToList();
                //elm = db.type_product.ToList();
                //elm.RemoveAt(id_type_product.Value);
                //type_product obj = elm[id_type_product.Value -1];
                //elm.Insert(0, obj);
                //ViewBag.ListTypeProduct = elm;
            }
            ViewBag.ListTypeProduct = db.type_product.ToList();
            ViewBag.IdTypeProduct = id_type_product;
            return View(lstSP.ToPagedList(pageNumber,pageSize));
        }

        //[HttpPost]
        //public ActionResult SanPham(int departID)
        //{
        //    IEnumerable<product> lstSP = null;
        //    ViewBag.ListTypeProduct = new SelectList(db.type_product, "id_type_product", "name_type_product");
        //     lstSP = db.products.Where(n => n.id_type_product == departID).ToList();
        //    return View(lstSP);
        //}



        public ActionResult HoaDon(int? page)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(db.bill_detail.ToList().ToPagedList(pageNumber, pageSize));
        }

        
        [HttpGet]
        public ActionResult Create(bill bill)
        {
            // Dua du lieu vao dropdownlist
            ViewBag.id_type_product = new SelectList (db.type_product.ToList(),"id_type_product","name_type_product");
        
            
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(product product,HttpPostedFileBase fileUpload)
        {
        
            ViewBag.id_type_product = new SelectList(db.type_product.ToList(), "id_type_product", "name_type_product");
            if(fileUpload == null)
            {
                ViewBag.ThongBao = "Vui lòng chọn hình ảnh";
                return View();
            }

            if (ModelState.IsValid)
            {
                //Lưu tên file
                var fileName = Path.GetFileName(fileUpload.FileName);
                // Luu đường dẫn của file
                var path = Path.Combine(Server.MapPath("~/image/product"), fileName);
                if (System.IO.File.Exists(path))
                {
                    ViewBag.ThongBao = "Hình ảnh đã tồn tại";
                }
                else
                {
                    fileUpload.SaveAs(path);
                }
                product.image = fileUpload.FileName;
                db.products.Add(product);
                db.SaveChanges();
            }
            return RedirectToAction("SanPham");
        }

        // Chỉnh sửa sản phẩm
        [HttpGet]
        public ActionResult Edit(int id_product)
        {
            product product = db.products.SingleOrDefault(n => n.id_product == id_product);
            if(product == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            ViewBag.id_type_product = new SelectList(db.type_product.ToList(), "id_type_product", "name_type_product",product.id_type_product);

            return View(product);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(product product, HttpPostedFileBase fileUpload)
        {
            if (ModelState.IsValid)
            {
                ////Lưu tên file
                var fileName = Path.GetFileName(fileUpload.FileName);

                //// Luu đường dẫn của file
                var path = Path.Combine(Server.MapPath("~/image/product"), fileName);
                //if (System.IO.File.Exists(path))
                //{
                    
                //}
                //else
                //{
                    fileUpload.SaveAs(path);
                //}

                product.image = fileUpload.FileName;
                db.products.Attach(product);
                db.Entry(product).State = EntityState.Modified;

                db.SaveChanges();
            }
            ViewBag.id_type_product = new SelectList(db.type_product.ToList(), "id_type_product", "name_type_product", product.id_type_product);
            return RedirectToAction("SanPham");
        }
        public ActionResult Details(int id_product)
        {
            product product = db.products.SingleOrDefault(n => n.id_product==id_product);
            if(product == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(product);
        }
        [HttpGet]
        public ActionResult Delete(int id_product)
        {
            product product = db.products.SingleOrDefault(n => n.id_product == id_product);
            if(product == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(product);
        }
        [HttpPost,ActionName("Delete")]
        public ActionResult XacNhanXoa(int id_product)
        {
            product product = db.products.SingleOrDefault(n => n.id_product == id_product);
            if(product == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            db.products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("SanPham");
        }

        // Danh mục
        [HttpGet]
        public ActionResult CreateProductType()
        {
            return View();
        }
        [HttpPost]
     
        public ActionResult CreateProductType(type_product type_Product)
        {


            if (ModelState.IsValid)
            {
               
                db.type_product.Add(type_Product);
                db.SaveChanges();
            }
            return RedirectToAction("DanhMuc");
        }

        // Chỉnh sửa sản phẩm
        [HttpGet]
        public ActionResult EditTypeProduct(int id_type_product)
        {
            type_product type_Product = db.type_product.SingleOrDefault(n => n.id_type_product == id_type_product);
            if (type_Product == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            return View(type_Product);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditTypeProduct(type_product type_Product)
        {
            if (ModelState.IsValid)
            {
               
                db.Entry(type_Product).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            return RedirectToAction("SanPham");
        }
        public ActionResult DetailsTypeProduct(int id_type_product)
        {
            type_product type_Product = db.type_product.SingleOrDefault(n => n.id_type_product == id_type_product);
            if (type_Product == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(type_Product);
        }
        [HttpGet]
        public ActionResult DeleteTypeProduct(int id_type_product)
        {
            type_product type_Product = db.type_product.SingleOrDefault(n => n.id_type_product == id_type_product);
            if (type_Product == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(type_Product);
        }
        [HttpPost, ActionName("DeleteTypeProduct")]
        public ActionResult XacNhanXoaLoaiSP(int id_type_product)
        {
            type_product type_Product = db.type_product.SingleOrDefault(n => n.id_type_product == id_type_product);
            if (type_Product == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            db.type_product.Remove(type_Product);
            db.SaveChanges();
            return RedirectToAction("DanhMuc");
        }


        // Đăng nhập
        [HttpGet]
        public ActionResult LoginAdmin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LoginAdmin(FormCollection f)
        {
            string sEmail = f["email"].ToString();
            string sMatKhau = f["password"].ToString();
            var s = Encrytor.MD5Hash(sMatKhau);
            user user = db.users.SingleOrDefault(n => n.email == sEmail && n.password == s);
            if (user != null)
            {
                Session["TaiKhoan"] = user;
                Session["Email"] = user.full_name.ToString();
                if (Session["Email"] == null)
                {
                    return View("~/Views/Admin/LoginAdmin");
                }
                else
                {
                    return View("~/Views/Admin/Index.cshtml");
                }
                
            }
            
            
            ViewBag.ThongBao = "Tên email hoặc mật khẩu không đúng";
            return View("LoginAdmin");
        }
        public ActionResult DangXuat()
        {
            Session["Email"] = null;
            return View("~/Views/Admin/LoginAdmin.cshtml");

        }

        //filter data
        [HttpGet]
        public ActionResult FilterData()
        {
            // Dua du lieu vao dropdownlist
            ViewBag.id_type_product = new SelectList(db.type_product.ToList(), "id_type_product", "name_type_product");


            return View();
        }


    }
}