/* Set values + misc */
var promoCode;
var promoPrice;
var fadeTime = 300;

/* Assign actions */
$('.quantity input').change(function () {
    updateQuantity(this);
});

$('.btn-remove').click(function () {
    removeItem(this);
});

$('.btn-increase').click(function () {
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

/* Update quantity */
function updateQuantity(quantityInput) {
    /* Calculate line price */
    var productRow = $(quantityInput).parent().parent();
    var price = parseFloat(productRow.children('.price').text());
    var quantity = $(quantityInput).val();
    var linePrice = price * quantity;

    /* Update line price display and recalc cart totals */
    productRow.children('.subtotal').each(function () {
        $(this).fadeOut(fadeTime, function () {
            $(this).text(linePrice.toFixed(2));
            recalculateCart();
            $(this).fadeIn(fadeTime);
        });
    });

    productRow.find('.item-quantity').text(quantity);
    updateSumItems();
}

function updateSumItems() {
    var sumItems = 0;
    $('.quantity input').each(function () {
        sumItems += parseInt($(this).val());
    });
    $('.total-items').text(sumItems);
}

/* Remove item from cart */
function removeItem(removeButton) {
    /* Remove row from DOM and recalc cart total */
    var productRow = $(removeButton).parent().parent().parent();
    console.log(productRow);
    productRow.slideUp(fadeTime, function () {
        productRow.remove();
        recalculateCart();
    //    updateSumItems();
    });
}

