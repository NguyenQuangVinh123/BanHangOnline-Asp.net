using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BanHangOnline.Models
{
    public class GioHang
    {
        DBShopBanHangEntities db = new DBShopBanHangEntities();
        public int id_product { get; set; }
        public String name_product { get; set; }
        public String image { get; set; }
        public double unit_price { get; set; }
        public int quantity { get; set; }
        public double TongTien
        {
            get { return quantity * unit_price; }
        }

        //Hàm tạo bảng
        public GioHang(int Id_product)
        {
            id_product = Id_product;
            product product = db.products.Single(n => n.id_product == id_product);
            name_product = product.name_product;
            image = product.image;
            unit_price = double.Parse(product.unit_price.ToString());
            quantity = 1;

        }
    }
}