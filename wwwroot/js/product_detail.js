const bigImg = document.querySelector(".product-content-left-big-img img");
const smallImg = document.querySelectorAll(".product-content-left-small-img img");
smallImg.forEach(function(imgItem, x) {
    imgItem.addEventListener("click", function () {
        bigImg.src = imgItem.src;
    })
    
});
// khi click vao detail thi hien detail , guarantee thì hiện guarantee
const detail = document.querySelector(".detail-product");
const guarantee = document.querySelector(".guarantee-product");

if (detail) {
    detail.addEventListener("click", function () {
        document.querySelector(".product-content-right-bottom-content-description-detail").style.display = "block";
        document.querySelector(".product-content-right-bottom-content-description-guarantee").style.display = "none";

    })
}

if (guarantee) {
    guarantee.addEventListener("click", function () {
        document.querySelector(".product-content-right-bottom-content-description-detail").style.display = "none";
        document.querySelector(".product-content-right-bottom-content-description-guarantee").style.display = "block";

    })
}
//  khi click vao arrow thi hien ra detail
const buttonArrow =document.querySelector(".product-content-right-bottom-top");
if(buttonArrow) {
    buttonArrow.addEventListener("click", function () {
        document.querySelector(".product-content-right-bottom-content").classList.toggle("activeB");
    })
}