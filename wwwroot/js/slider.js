const imgPosition = document.querySelectorAll(".aspect-ratio-169 img");
const imgContainer = document.querySelector(".aspect-ratio-169");
const dotItem = document.querySelectorAll(".dot");
let imgNumber = imgPosition.length; // lay tong so phan tu anh
let index = 0;
imgPosition.forEach(function (image, index) {
    image.style.left = index * 100 + "%"; //Tạo phần hiển thị ảnh theo tỉ lệ %
    dotItem[index].addEventListener("click", function () {  // khi click vao dau cham
        slider(index);
    })

})

function ImgSlide() {

    index++;
    console.log(index);
    if (index >= imgNumber) {
        index = 0;
    }
    slider(index);

}
setInterval(ImgSlide, 5000)

function slider(index) {
    imgContainer.style.left = "-" + index * 100 + "%"; // cho anh hien ra man hinh (100%)
    const dotActive = document.querySelector(".active");
    dotActive.classList.remove("active");
    dotItem[index].classList.add("active");
}

// khi scroll chuot xuong thi header se ve lai background white

const header = document.querySelector("header");
window.addEventListener("scroll", function () {
    x = window.pageYOffset;
    if (x > 0) {
        header.classList.add("sticky");
        
    }
    else {
        header.classList.remove("sticky");
    }
   
}) 



// click vao icon bar 
     $(document).ready(function(){
        $('.menu-icon-bar').click(function(){
            $('.menu-mobile').slideToggle();
            $('.sub-menu-mobile').hide();
            // $('.others-mobile').hide(); //hide cai icon
        }) 
     }
    )
    $(document).ready(function(){
        $('.menu-mobile > ul > li > a').click(function(){
            $('.sub-menu-mobile').slideToggle();
            // $('.others-mobile').hide(); //hide cai icon
        }) 
     }
    )