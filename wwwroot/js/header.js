var load = document.getElementById('click_me');
var input_value = document.getElementById('load_input').value;
console.log(input_value);
load.onclick = function () {
    var str = "/product/SearchProductByName?page=1&keyword=" + document.getElementById('load_input').value;
    document.getElementById('click_me').setAttribute('href', str)
};


/*if (@subcate_id >= 1 && @subcate_id <= 5) {
    $(".header-man").addClass("block");
}
if (@subcate_id >= 6 && @subcate_id <= 10) {
    $(".header-woman").addClass("block");
}
if (@subcate_id >= 11 && @subcate_id <= 16) {
    $(".header-unisex").addClass("block");
}
if (@subcate_id >= 17 && @subcate_id <= 18) {
    $(".header-child").addClass("block");
}
if (@subcate_id >= 19 && @subcate_id <= 23) {
    $(".header-items").addClass("block");
}
if (@subcate_id >= 24 && @subcate_id <= 25) {
    $(".header-sale").addClass("block");
} */

