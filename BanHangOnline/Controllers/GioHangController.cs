using BanHangOnline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace BanHangOnline.Controllers
{
    public class GioHangController : Controller
    {
        // GET: GioHang
        DBShopBanHangEntities db = new DBShopBanHangEntities();
        
        #region Giỏ Hàng
        // Xây dựng chức năng đặt hàng
        public List<GioHang> LayGioHang()
        {
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;

            if (lstGioHang == null)
            {
                // Nếu giỏ hàng chưa tồn tại thì tạo giỏ hàng
                lstGioHang = new List<GioHang>();
                Session["GioHang"] = lstGioHang;
            }
            return lstGioHang;
        }
        // Add cart
        public ActionResult ThemGioHang(int id_product, string strUrl)
        {
            product product = db.products.SingleOrDefault(n => n.id_product == id_product);
            if (product == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            // Lấy session giỏ hàng
            List<GioHang> lstGioHang = LayGioHang();
            GioHang sp = lstGioHang.Find(n => n.id_product == id_product);
            if (sp == null)
            {
                sp = new GioHang(id_product);

                lstGioHang.Add(sp);
                ViewBag.CartItemView = lstGioHang;
                //ViewBag.TongTien = TongTien();
                return Redirect(strUrl);
            }
            else
            {
                sp.quantity++;
                return Redirect(strUrl);
            }
        }
        // Update cart
        public ActionResult CapNhapGioHang(int id_product, FormCollection f)
        {
            product product = db.products.SingleOrDefault(n => n.id_product == id_product);
            if (product == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            List<GioHang> lstGioHang = LayGioHang();
            GioHang sp = lstGioHang.SingleOrDefault(n => n.id_product == id_product);
            if (sp != null)
            {
                sp.quantity = int.Parse(f["txtSoLuong"].ToString());
            }
            return RedirectToAction("../Home/Index");
        }
        // Delete cart
        public ActionResult XoaGioHang(int id_product)
        {
            product product = db.products.SingleOrDefault(n => n.id_product == id_product);
            if (product == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            List<GioHang> lstGioHang = LayGioHang();
            GioHang sp = lstGioHang.SingleOrDefault(n => n.id_product == id_product);
            if (sp != null)
            {
                lstGioHang.RemoveAll(n => n.id_product == id_product);

            }
            if (lstGioHang.Count == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("../Home/Index");
        }
        //Xay dung trang giỏ hàng
        public ActionResult GioHang()
        {

            if (Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "Home");

            }
            List<GioHang> lstGioHang = LayGioHang();
            return View(lstGioHang);

        }
        //Tính tổng số lượng
        private int TongSoLuong()
        {
            int quantity = 0;
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if (lstGioHang != null)
            {
                quantity = lstGioHang.Sum(n => n.quantity);
            }
            return quantity;
        }
        // Tinh tổng tiền
        private double TongTien()
        {
            double TongTien = 0;
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if (lstGioHang != null)
            {
                TongTien = lstGioHang.Sum(n => n.TongTien);
            }
            return TongTien;
        }
        // Xây dung giohangpartial
        public ActionResult GioHangPartial()
        {
           
            if (TongSoLuong() == 0)
            {
                return PartialView();
            }
            ViewBag.TongSoLuong = TongSoLuong();
            ViewBag.TongTien = TongTien();
            return PartialView();
           
        }
        //Xây dưng trang edit giỏ hàng
        public ActionResult SuaGioHang()
        {
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            List<GioHang> lstGioHang = LayGioHang();
            return View(lstGioHang);
        }
        #endregion

        #region Đặt hàng
        [HttpGet]
        public ActionResult DatHang(bill bill)
        {
            return View("DatHang");
        }
        [HttpPost]
        public ActionResult DatHang()
        {
           
            //Thêm đơn hàng
            bill bill = new bill();
            KhachHang kh = (KhachHang) Session["TaiKhoan"];
            //if (Session["TaiKhoan"] == null)
            //{
            //    return RedirectToAction("DangNhap", "Home");
            //}
            //if (Session["GioHang"] == null)
            //{
            //    RedirectToAction("Index", "Home");
            //}
            List<GioHang> gh = LayGioHang();
            
                bill.MaKH = kh.MaKH;
            
            bill.MaKH = kh.MaKH;
            bill.date_order = DateTime.Now;
            db.bills.Add(bill);
            db.SaveChanges();
            // Them chi tiết đơn hàng
            foreach(var item in gh)
            {
                bill_detail bill_Detail = new bill_detail();
                bill_Detail.id_bill = bill.id_bill;
                bill_Detail.id_product = item.id_product;
                bill_Detail.quantity = item.quantity;
                bill_Detail.unit_price = (int)item.unit_price;
                db.bill_detail.Add(bill_Detail);
            }
            db.SaveChanges();
           
            return RedirectToAction("Index", "Home");

        }
        #endregion


    }
}