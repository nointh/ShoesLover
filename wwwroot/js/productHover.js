

$('#tab1,#tab2,#tab3').click(function () {
    $(this).unbind('mouseout mouseover');
    // this will unbind mouseout and mouseover  events when click
    // e.g. onTab1Clicked, mouseout and mouseover events will be unbind on tab1
})

$(".li-product-id").each(function (li_item, index, array) {

    var temp = $(this).val(); //Lấy id product
    var new_img = $('#product-img-' + temp);
    // console.log(temp);
    $(".li-color-" + temp).each(function () {  //hover vào color
        $(this).click(function () {
            $(this).unbind("mouseout"); 
                     
        });
        $(this).mouseover(function () {
            var color_id = $(this).val();
            // alert(color_id);
            $(".variant-img-" + temp).each(function () {  // lấy image từ list variant
                var color_variant_id = $(this).val();
                var c = $(this).attr("src");
             
                if (color_id == color_variant_id) {

                    var b = $(this).attr("src"); // láy đường dẫn 

                  
                    new_img.attr("src", b);
                }


            });




        });

        $(this).mouseout(function () {
            var b = $(".variant-img-" + temp + ":first").attr("src");         
            new_img.attr("src", b);


        });

        










    });
});


/*$(".li-product-id").each(function (li_item, index, array) {

    var temp = $(this).val(); //Lấy id product
    var new_img = $('#product-img-' + temp);
    // console.log(temp);
    $(".li-color-" + temp).each(function (li_item_color, index) {
        $(this).mouseover(function () {
            var color_id = $(this).val();
            $.ajax({
                url: "/product/GetProductVariantImage",
                type: "POST",
                dataType: "JSON",
                data: { product_id: temp, color_id: color_id },
                success: function (data) {
                    data.forEach((element, index, array) => {
                        var str = element.productVariantImage;
                        var a = '/image/';
                        var b = a + str;
                        new_img.removeAttr('src');
                        new_img.attr('src', b);
                    })
                },
                error: () => alert("something wrong")
            });
        });
        $(this).mouseout(function () {
            var color_id = $(this).val();
            $.ajax({
                url: "/product/GetProductVariantImage",
                type: "POST",
                dataType: "JSON",
                data: { product_id: temp, color_id: color_id },
                success: function (data) {
                    data.forEach((element, index, array) => {
                        var str = element.productDefault;
                        var a = '/image/';
                        var b = a + str;
                        new_img.removeAttr('src');
                        new_img.attr('src', b);
                    })
                },
                error: () => alert("something wrong")

            });

        });
        $(this).click(function () {
            $(this).unbind('mouseout');
            var color_id = $(this).val();
            $.ajax({
                url: "/product/GetProductVariantImage",
                type: "POST",
                dataType: "JSON",
                data: { product_id: temp, color_id: color_id },
                success: function (data) {
                    data.forEach((element, index, array) => {
                        var str = element.productVariantImage;                     
                        var a = '/image/';
                        var b = a + str;
                        new_img.removeAttr('src');
                        new_img.attr('src', b);
                        console.log(b);
                        console.log(" da click");
                    })
                },
                error: () => alert("something wrong")
            });
           
        });


    });




});  */


/*$(".li-product-id").each(function (li_item, index, array) {



    var temp = $(this).val();
    var new_img = $('#product-img-' + temp);
    //console.log(temp);
    $(".li-color-" + temp).each(function (li_item_color, index) {
        var temp_color_id = $(this).val();
       // console.log(temp_color_id);

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
                        var a = '/image/';
                        var b = a + str;
                       // console.log(b);
                        new_img.removeAttr('src');
                        new_img.attr('src', b);

                    })

                },
                error: () => alert("something wrong")

            });




        });




    });

}); */

       
