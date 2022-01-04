/* Set values + misc */
var fadeTime = 300;
var isChange = false;
/* Assign actions */
//$('.quantity input').change(function () {
//    updateQuantity(this);
//});

$('.btn-remove').click(function () {
    isChange = true;    
    toggleChange();
    removeItem(this);
    console.log("cart item: ", $(".cart-item").length)
    
});

function toggleChange() {
    if (isChange) {
        $("#btn-save").removeClass("hidden");
        $("#btn-reset").removeClass("hidden");
    }
    else {
        $("#btn-save").addClass("hidden");
        $("#btn-reset").addClass("hidden");
    }


}

$('.btn-increase').click(function () {
    isChange = true;
    toggleChange();

    var selectedRow = $(this).parent().parent();
    console.log(selectedRow);
    //update the sub total 

    var quan = parseInt(selectedRow.find(".quantity").val()) + 1;

    selectedRow.find(".quantity").val(quan)
    var subTotal = selectedRow.find(".sub-total");
    console.log("subtotal = ", subTotal);
    subTotal.html("");
    var totalPrice = quan * parseInt(selectedRow.find(".price").html())
    subTotal.html(totalPrice.toString());
    //calculate cart
    recalculateCart()
})
$('.btn-decrease').click(function () {
    isChange = true;
    toggleChange();

    var selectedRow = $(this).parent().parent();
    console.log(selectedRow);
    //update the sub total 
    var quan = parseInt(selectedRow.find(".quantity").val()) - 1;
    if (quan <= 0) return;
    selectedRow.find(".quantity").val(quan)
    var subTotal = selectedRow.find(".sub-total");
    console.log("subtotal = ", subTotal);
    subTotal.html("");
    var totalPrice = quan * parseInt(selectedRow.find(".price").html())
    subTotal.html(totalPrice.toString());
    //recalculate Cart
    recalculateCart()
})
$("input.quantity").change((event) => {
    var quanTarget = $(event.target)
    var quantity = quanTarget.val()
    isChange = true;
    toggleChange();

    if (quantity <= 0) {
        Swal.fire({
            icon: 'error',
            title: 'Oops...',
            text: 'Xin lỗi bạn, giỏ hàng yêu cầu số lượng sản phẩm dương',
            footer: '<p>Bạn có thể xóa sản phẩm này nếu muốn</p>'
        })
        quanTarget.val(1);
        quantity = 1;

    }

    var selectedRow = $(quanTarget).parent().parent()
    var subTotal = selectedRow.find(".sub-total");

    subTotal.html("");
    var totalPrice = parseInt(quantity) * parseInt(selectedRow.find(".price").html())
    console.log("price =", selectedRow.find(".price").html())
    console.log("quan =", quantity)
    console.log(selectedRow)
    subTotal.html(totalPrice.toString());

    recalculateCart();

})
$(document).ready(function () {
    isChange = false;

    toggleChange();
    recalculateCart();
});


/* Recalculate cart */
function recalculateCart() {
    var subtotal = 0;

    /* Sum up row totals */
    $('.cart-item').each(function (element) {
        subtotal += parseFloat($(this).find('.sub-total').html());
    });
    console.log(subtotal)
    /* Calculate totals */
    var total = subtotal;
    $(".total-price").html(total.toLocaleString() + " VND");
}


/* Remove item from cart */
function removeItem(removeButton) {
    /* Remove row from DOM and recalc cart total */
    var productRow = $(removeButton).parent().parent().parent();
    console.log(productRow);
    productRow.slideUp(fadeTime, function () {
        productRow.remove();
        if ($(".cart-item").length <= 0) {
            var table = $("cart-table");
            table.find("tbody").html("<tr><td colspan='4' class='text - center'><p>Bạn chưa thêm sản phẩm nào vào giỏ hàng</p></td ></tr >")
            $("#btn-delete-all").addClass("hidden");
        }
        else {
            $("#btn-delete-all").removeClass("hidden");
        }
        recalculateCart();
    //    updateSumItems();
    });
}

//revert to old status
$("#btn-reset").click(()=>
{
    location.reload();
})


