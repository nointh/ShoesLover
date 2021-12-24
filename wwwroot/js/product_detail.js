
<script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.6.0/jquery.min.js" integrity="sha512-894YE6QWD5I59HgZOGReFYm4dnWc1Qt5NtvYSaNcOP+u1T9qYdvdihz0PPSiiqn/+/3e7Jo4EaG7TubfWGUrMQ==" crossorigin="anonymous" referrerpolicy="no-referrer"></script>



// khi click vao detail thi hien detail , guarantee thì hiện guarantee

const detail = document.querySelector(".detail-product");
const guarantee = document.querySelector(".guarantee-product");

if (detail) {
    detail.addEventListener("click", function () {
        document.querySelector(".product-content-right-bottom-content-description-detail").style.display = "block";
        document.querySelector("detail-product").classList.add("border-detail");
        document.querySelector("guarantee-product").classList.remove("border-detail");
        document.querySelector(".product-content-right-bottom-content-description-guarantee").style.display = "none";

    })
}

if (guarantee) {
    guarantee.addEventListener("click", function () {
        document.querySelector(".product-content-right-bottom-content-description-detail").style.display = "none";
        document.querySelector("detail-product").classList.remove("border-detail");
        document.querySelector("guarantee-product").classList.add("border-detail");
        document.querySelector(".product-content-right-bottom-content-description-guarantee").style.display = "block";

    })
}
//  khi click vao arrow thi hien ra detail
const buttonArrow = document.querySelector(".product-content-right-bottom-top");
if (buttonArrow) {
    buttonArrow.addEventListener("click", function () {
        document.querySelector(".product-content-right-bottom-content").classList.toggle("activeB");
    })
}









//Số lượng



var size = document.querySelectorAll(".li-size-id");
size.forEach(function (item, index) {
    console.log(3);
});
