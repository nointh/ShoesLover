
$('#tab1,#tab2,#tab3').click(function () {
    $(this).unbind('mouseout mouseover');
    // this will unbind mouseout and mouseover  events when click
    // e.g. onTab1Clicked, mouseout and mouseover events will be unbind on tab1
})

 $(".li-product-id").each(function (li_item, index, array) {
   
    var temp = $(this).val();
    var new_img = $('#product-img-'+temp);
    console.log(temp);
    $(".li-color-" + temp).each(function (li_item_color, index) {
        var temp_color_id = $(this).val();
        console.log(temp_color_id);
       
      
            
        $(this).mouseover(function () {
            var product_id = temp;  // lấy được id của productid
            console.log(product_id);
            var color_id = temp_color_id;
            console.log(color_id);
            var check = $(".watcher-product ul li:first-child").val();
            if (check == temp_color_id) {
                return false;
            }
            else {

                $.ajax({
                    url: "/product/GetProductVariantImage",
                    type: "POST",
                    dataType: "JSON",
                    data: { product_id: temp, color_id: temp_color_id },
                    success: function (data) {
                        data.forEach((element, index, array) => {
                            var str = element.productVariantImage;
                            console.log(str);
                            var a = '/image/product_default_img/';
                            var b = a + str;
                            console.log(b);

                            new_img.removeAttr('src');
                            new_img.attr('src', b);

                        })
                    },
                    error: () => alert("something wrong")
                });
            }



        });
        $(this).mouseout(function () {
            var product_id = temp;
            console.log(product_id);
            var color_id = temp_color_id;
            var check = $(".watcher-product ul li:first-child").val();
            if (check == temp_color_id) {
                return false;
            }


            $.ajax({
                url: "/product/GetProductVariantImage",
                type: "POST",
                dataType: "JSON",
                data: { product_id: temp, color_id: temp_color_id },
                success: function (data) {
                    data.forEach((element, index, array) => {
                        var str = element.productDefault;
                        console.log(str);
                        var a = '/image/product_default_img/';
                        var b = a + str;
                        console.log(b);
                        new_img.removeAttr('src');
                        new_img.attr('src', b);

                    })

                },
                error: () => alert("something wrong")

            });

        });
           

        $(this).on("click", function () {
            $(this).unbind("mouseout mouseover");
            // this will unbind mouseout and mouseover  events when click
            // e.g. onTab1Clicked, mouseout and mouseover events will be unbind on tab1
        })


        })




    })  


$(".li-product-id").each(function (li_item, index, array) {



    var temp = $(this).val();
    var new_img = $('#product-img-' + temp);
    console.log(temp);
    $(".li-color-" + temp).each(function (li_item_color, index) {
        var temp_color_id = $(this).val();
        console.log(temp_color_id);

        $(this).on("click", function () {
            
             
           
            $.ajax({
                url: "/product/GetProductVariantImage",
                type: "POST",
                dataType: "JSON",
                data: { product_id: temp, color_id: temp_color_id },
                success: function (data) {
                    data.forEach((element, index, array) => {
                        var str = element.productVariantImage;
                        console.log(str);
                        var a = '/image/product_default_img/';
                        var b = a + str;
                        console.log(b);
                        new_img.removeAttr('src');
                        new_img.attr('src', b);

                    })

                },
                error: () => alert("something wrong")

            });




        });




    });

});

       
