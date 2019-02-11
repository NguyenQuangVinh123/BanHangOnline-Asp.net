using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace BanHangOnline.Models
{
    [MetadataTypeAttribute(typeof(productmetadata))]
    public partial class product
    {
        internal sealed class productmetadata
        {
            [Display(Name ="Mã Bánh")]
            public int id_product { get; set; }
         
            [Display(Name = "Tên Bánh")]
            public string name_product { get; set; }
           
            [Display(Name = "Mô tả")]
            public string description { get; set; }
           
            [Display(Name = "Giá")]
            public Nullable<int> unit_price { get; set; }
            
            [Display(Name = "Giảm giá")]
            public Nullable<int> promotion_price { get; set; }
            [Display(Name = "Hình ảnh")]
            public string image { get; set; }
           
            [Display(Name = "Định lượng")]
            public string unit { get; set; }
           
            [Display(Name = "Loại Bánh")]
            public Nullable<int> id_type_product { get; set; }
         
            [Display(Name = "Top Product")]
            public Nullable<int> best { get; set; }

        }
    }
}