


// function change(id_bill)
// {
    // $.ajax({
        // url: "/ShopBanHang/BillServlet",
        // type: "post",
        // data: {
               
            // command: "update",
            // status : "Xong",
           // id_bill: id_bill 
        // },
        // success: function(res) {
            // window.location = "manager_bill.jsp";
// //               document.getElementById("status").html = data.status.valueOf(id_bill);
        // },
        // error: function(res) {
            // console.error(res);
        // }
    // });
// }
// function change(id_product){
// $.ajax({
    // url: "localhost:55902/Home/GioHangPartial",
    // type: "post",
	// data: {id_product = id_product},
    // success: function (data) {
        // $('#cart').innerHTML(data);
    // }
// });
// }

  $(document).ready(function(){
    $("#dropdown").click(function(){
		
          $("#dropdownlist").load("../GioHang/GioHang", function(responsetxt, statustxt, xhr){
			  
               if(statustxt == "success")
					
               if(statustxt == "error")
                 alert("error: " + xhr.status + ": " + xhr.statustext);
           });
       });
  });
  
    
 
 // $.ajax({
    // url: /Home/GioHangPartial,  //Pass URL here 
    // type: "GET", //Also use GET method
    // success: function(data) {
        // var time = $(data).find('#cart').html();
        
    // }
// });