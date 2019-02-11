using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BanHangOnline.Models
{
    [MetadataTypeAttribute(typeof(khachhangmetadata))]
    public partial class KhachHang
    {
        internal sealed class khachhangmetadata
        {
            public int MaKH { get; set; }
            [Display(Name = "Họ và tên")]
            [Required(ErrorMessage = "{0} không được để trống")]
            public string HoTen { get; set; }
            [Display(Name = "Ngày sinh")]
            [Required(ErrorMessage = "{0} không được để trống")]
            public Nullable<System.DateTime> NgaySinh { get; set; }
            [Display(Name = "Giới tính")]
            public string GioiTinh { get; set; }
            [Display(Name = "Số điện thoại")]
            [Required(ErrorMessage = "{0} không được để trống")]
            public Nullable<decimal> DienThoai { get; set; }
            [Display(Name = "Tên tài khoản")]
            [Required(ErrorMessage = "{0} không được để trống")]
            public string TaiKhoan { get; set; }
            [Display(Name = "Mật khẩu")]
            [Required(ErrorMessage = "{0} không được để trống")]
            public string MatKhau { get; set; }
            [Display(Name = "Email ")]
            [Required(ErrorMessage = "{0} không được để trống")]
            public string Email { get; set; }
            [Display(Name = "Địa chỉ")]
            [Required(ErrorMessage = "{0} không được để trống")]
            public string DiaChi { get; set; }
        }
    }
}