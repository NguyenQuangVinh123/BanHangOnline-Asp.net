
$(document).ready(function () {
    function Search() {
                var xhttp;
                var name = $('#searchProductID').val();
//                console.log(`--> +${name}`);
                var name = document.myform.name.value;
                if (name != "") {
                    var url = "SearchServlet?name=" + name;
                    if (window.XMLHttpRequest) {
                        xhttp = new XMLHttpRequest();
                    } else {
                        xhttp = new ActiveXObject("Microsoft.XMLHTTP");
                    }
                    xhttp.onreadystatechange = function () {
                        if (xhttp.readyState = 4) {
                            var data = xhttp.responseText;
                            document.getElementById("searchresult").innerHTML = data;
                        }
                    }
                    xhttp.open("GET", url, true);
                } else {
                    document.getElementById("searchresult").innerHTML = "";
                }

            }
});


