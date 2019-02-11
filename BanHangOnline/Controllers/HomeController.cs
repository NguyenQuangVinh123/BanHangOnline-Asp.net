using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BanHangOnline.Models;
using PagedList;
using PagedList.Mvc;
namespace BanHangOnline.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        DBShopBanHangEntities db = new DBShopBanHangEntities();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult About()
        {
            return View();
        }
        public ActionResult Contact()
        {
            return View();
        }
        public PartialViewResult NewProductPartial()
        {
            var listNewProduct = db.products.Where(n=>n.best==1).Take(8).ToList();
            return PartialView(listNewProduct);
        }
        public PartialViewResult TopProductPartial(int? page)
        {
            int pageSize = 8;
            int pageNumber = (page ?? 1);
            var listTopProduct = db.products.ToList().ToPagedList(pageNumber,pageSize);
            return PartialView(listTopProduct);
        }
        public PartialViewResult MenuPartial()
        {
            var listMenu = db.type_product.ToList();
            return PartialView(listMenu);
        }
        public PartialViewResult LeftSideSPTheoLoaiSP()
        {
            var listMenu = db.type_product.ToList();
            return PartialView(listMenu);
        }
        // Xem chi tiết SP

        public ViewResult XemChiTiet(int id_product)
        {
            product product = db.products.SingleOrDefault(n => n.id_product == id_product);
            if(product == null)
            {
                //Trả về trang báo lỗi
                Response.StatusCode = 404;
                return null;
            }
            return View(product);
        }

        // SP theo loạiSP

        public ViewResult SPTheoLoaiSP (int id_type_product = 0) {

           
            type_product tp = db.type_product.SingleOrDefault(n => n.id_type_product == id_type_product);
            if(tp==null)
            {
                Response.StatusCode = 404;
                return null;
            }


           
            List<product> listProduct = db.products.Where(n => n.id_type_product == id_type_product).ToList();
            if(listProduct.Count == 0)
            {
                ViewBag.product = "Không có bánh nào ";
            }
            return View(listProduct);
        }

        // Đăng ký 
        [HttpGet]
        public ActionResult DangKy()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DangKy(KhachHang kh)
        {
            if (ModelState.IsValid)
            {
                //Insert data into KhachHang database
                db.KhachHangs.Add(kh);
                db.SaveChanges();
                Session["Email"] = kh.TaiKhoan.ToString();
                return View("~/Views/Home/Index.cshtml");
            }
            return View();
        }
        // Dang nhap
        [HttpGet]
        public ActionResult DangNhap()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DangNhap(FormCollection f)
        {
            string sEmail = f["email"].ToString();
            string sMatKhau = f["password"].ToString();
            KhachHang kh = db.KhachHangs.SingleOrDefault(n => n.Email == sEmail && n.MatKhau == sMatKhau);
            if (kh != null)
            {
                Session["TaiKhoan"] = kh;
                Session["Email"] = kh.TaiKhoan.ToString();
                return View("~/Views/Home/Index.cshtml");
            }
            ViewBag.ThongBao = "Tên email hoặc mật khẩu không đúng";
            return View();
        }
        public ActionResult DangXuat()
        {
            Session["Email"] = null;
            return View("~/Views/Home/DangNhap.cshtml");

        }





        


        // Tìm kiếm

        [HttpPost]
        public ActionResult KetQuaTimKiem(FormCollection f, int? page)
        {
            string sTuKhoa = f["txtTimKiem"].ToString();
            ViewBag.TuKhoa = sTuKhoa;
            List<product> lstKQTK = db.products.Where(n => n.name_product.Contains(sTuKhoa)).ToList();
            //Phan trang tìm kiếm
            int pageNumber = (page ?? 1);
            int pageSize = 4;
            if (lstKQTK.Count == 0)
            {

                ViewBag.ThongBao = "Không tìm thấy sản phẩm nào";
                return View(db.products.OrderBy(n => n.name_product).ToPagedList(pageNumber, pageSize).ToList());
            }
            ViewBag.ThongBao = "Đã tìm thấy " + lstKQTK.Count() + " " + " kết quả";
            return View(lstKQTK.OrderBy(n => n.name_product).ToPagedList(pageNumber, pageSize));
        }
        [HttpGet]
        public ActionResult KetQuaTimKiem(String sTuKhoa, int? page)
        {
            ViewBag.TuKhoa = sTuKhoa;
            List<product> lstKQTK = db.products.Where(n => n.name_product.Contains(sTuKhoa)).ToList();
            //Phan trang tìm kiếm
            int pageNumber = (page ?? 1);
            int pageSize =4;
            if (lstKQTK.Count == 0)
            {

                ViewBag.ThongBao = "Không tìm thấy sản phẩm nào";
                return View(db.products.OrderBy(n => n.name_product).ToPagedList(pageNumber, pageSize).ToList());
            }
            ViewBag.ThongBao = "Đã tìm thấy " + lstKQTK.Count() + " " + " kết quả";
            return View(lstKQTK.OrderBy(n => n.name_product).ToPagedList(pageNumber, pageSize));
        }




    }

}
