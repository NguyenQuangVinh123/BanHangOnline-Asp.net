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
    public class TimKiemController : Controller
    {
        DBShopBanHangEntities db = new DBShopBanHangEntities();
        // GET: TimKiem
        [HttpPost]
        public ActionResult KetQuaTimKiem(FormCollection f,int? page)
        {
            string sTuKhoa = f["txtTimKiem"].ToString();
            List<product> lstKQTK = db.products.Where(n => n.name_product.Contains(sTuKhoa)).ToList();
            //Phan trang tìm kiếm
            int pageNumber = (page ?? 1);
            int pageSize = 8;
            if (lstKQTK.Count == 0)
            {

                ViewBag.ThongBao = "Không tìm thấy sản phẩm nào";
                return View(db.products.OrderBy(n => n.name_product).ToPagedList(pageNumber,pageSize).ToList());
            }
            ViewBag.ThongBao = "Đã tìm thấy " + lstKQTK.Count() + " " + " kết quả";
            return View(lstKQTK.OrderBy(n=>n.name_product).ToPagedList(pageNumber,pageSize));
        }
    }
}