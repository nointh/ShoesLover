$(".button_wishlist").each(function () {
    $(this).on("click", function () {
        var id = $(this).attr("id");
        $(".rating-product-interface-" + id).toggleClass("block-color");
    });
});