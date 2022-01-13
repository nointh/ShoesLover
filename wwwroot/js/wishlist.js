
//Tạo sản phẩm yêu thích

function add_wishlist(click_id) {

    var id = click_id;
    var name = $(".wishlist-product_name-" + id).val();
    var image = $(".wishlist-default_image-" + id).val();
    var regular = $(".wishlist-regular_price-" + id).val();
    var sale = $(".wishlist-sale_price-" + id).val();
    var url = $(".wishlist_product_url-" + id).attr("href");
    var new_item = {
        'id': id,
        'name': name,
        'image': '/image/' + image,
        'regular': regular,
        'sale': sale,
        'url': 'https://localhost:5001' + url
    }
    if (localStorage.getItem('data') == null) { //nếu key data bằng rỗng
        localStorage.setItem('data', '[]'); //set lại cho nó rỗng
    }
    var old_data = JSON.parse(localStorage.getItem('data')); //lấy dữ liệu
    // old_data.push(new_item); //Nếu mà trong cái sản phẩm yêu thích có dữ liệu rồi thì nó sẽ đấy cái product mới vào
    //   localStorage.setItem('data', JSON.stringify(old_data));  //set lại key data của item  /*

    var matches = $.grep(old_data, function (obj) {
        return obj.id == id;
    });
    if (matches.length) {

        //  alert("Sản phẩm bạn đã thêm vào rồi không thể thêm nữa");
    }
    else {
        old_data.push(new_item);
        $("#row_wishlist").append('<div  id="' + new_item.id + '" style="margin:10px 0; display: flex; justify-content:space-between;"><div class="col-md-4"><img src="' + new_item.image + '" width="80%"></div>' +
            '<div style="font-size:11px" ><p class="product-name-wishlist" style="color: black;">' + new_item.name + '</p><p style="color: black; text-decoration: line-through;">' + new_item.regular + '</p><p style="color: red"> ' + new_item.sale + '</p > <a style="color: blue;text-decoration: underline;" href="' + new_item.url + '">Xem chi tiết</a></div ></div > ');
    }

    localStorage.setItem('data', JSON.stringify(old_data));
}


function View() {
    if (localStorage.getItem('data') != null) {
        var data = JSON.parse(localStorage.getItem('data'));
        data.reverse(); //đảo ngược sản phẩm mới thêm lên đầu
        document.getElementById('row_wishlist').style.overflow = 'scroll';
        document.getElementById('row_wishlist').style.height = '300px';
        for (i = 0; i < data.length; i++) {

            var name = data[i].name;
            var id = data[i].id;
            var sale = data[i].sale;
            var regular = data[i].regular;
            var image = data[i].image;
            var url = data[i].url;
            $("#row_wishlist").append('<div id="' + id + '" style="margin:10px 0; display: flex; justify-content:space-between;"><div class="col-md-4"><img src="' + image + '" width="80%"></div>' +
                '<div style="font-size:11px"><p class="product-name-wishlist" style="color: black;">' + name + '</p><p style="color: black;text-decoration: line-through;" >' + regular + '</p><p style="color:red">' + sale + '</p><a style="color: blue;text-decoration: underline;" href="' + url + '">Xem chi tiết</a></div></div>');
        }
    }
}
View();



//viewed




function product_viewed(click_id) {

    var id = click_id;
    var name = $(".wishlist-product_name-" + id).val();
    var image = $(".wishlist-default_image-" + id).val();
    var regular = $(".wishlist-regular_price-" + id).val();
    var sale = $(".wishlist-sale_price-" + id).val();
    var url = $(".wishlist_product_url-" + id).attr("href");
    var new_item = {
        'id': id,
        'name': name,
        'image': '/image/' + image,
        'regular': regular,
        'sale': sale,
        'url': 'https://localhost:5001' + url
    }
    if (localStorage.getItem('viewed') == null) { //nếu key data bằng rỗng
        localStorage.setItem('viewed', '[]'); //set lại cho nó rỗng
    }
    var old_data = JSON.parse(localStorage.getItem('viewed')); //lấy dữ liệu
    // old_data.push(new_item); //Nếu mà trong cái sản phẩm yêu thích có dữ liệu rồi thì nó sẽ đấy cái product mới vào
    //   localStorage.setItem('data', JSON.stringify(old_data));  //set lại key data của item  /*

    var matches = $.grep(old_data, function (obj) {
        return obj.id == id;
    });
    if (matches.length) {

        // alert("Sản phẩm bạn đã thêm vào rồi không thể thêm nữa");
    }
    else {
        old_data.push(new_item);
        $("#row_wishlist").append('<div id="' + new_item.id + '" style="margin:10px 0; display: flex; justify-content:space-between;"><div class="col-md-4"><img style ="width:70px; margin-top:8px ;" src="' + new_item.image + '"></div>' +
            '<div style="margin-left: 15px; font-size:11px" ><p class="product-name-wishlist" style="color: black;">' + new_item.name + '</p><p style="color: black; text-decoration: line-through;">' + new_item.regular + '</p><p style="color: red"> ' + new_item.sale + '</p > <a style="color: blue;text-decoration: underline;" href="' + new_item.url + '">Xem chi tiết</a></div ></div > ');
    }

    localStorage.setItem('viewed', JSON.stringify(old_data));
}


function Viewed() {
    if (localStorage.getItem('viewed') != null) {
        var data = JSON.parse(localStorage.getItem('viewed'));
        data.reverse(); //đảo ngược sản phẩm mới thêm lên đầu
        document.getElementById('row_viewed').style.overflow = 'scroll';
        document.getElementById('row_viewed').style.height = '300px';
        for (i = 0; i < data.length; i++) {

            var name = data[i].name;
            var id = data[i].id;
            var sale = data[i].sale;
            var regular = data[i].regular;
            var image = data[i].image;
            var url = data[i].url;
            $("#row_viewed").append('<div id="' + id + '" style="margin:10px 0; display: flex; justify-content:space-between;"><div class="col-md-4"><img style ="width:70px; margin-top:8px ;" src="' + image + '"></div>' +
                '<div style="margin-left: 15px; font-size:11px"><p class="product-name-wishlist" style="color: black;">' + name + '</p><p style="color: black;text-decoration: line-through;" >' + regular + '</p><p style="color:red">' + sale + '</p><a style="color: blue;text-decoration: underline;" href="' + url + '">Xem chi tiết</a></div></div>');
        }
    }
}
Viewed();
