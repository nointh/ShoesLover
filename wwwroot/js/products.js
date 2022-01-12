/******************** Products ********************/
/* Category */
/* Display navigatino when click category */
/*const categories = $$(".category-detail > ul > li > a");
categories.forEach((category) => {
    category.onclick = () => {
        $(".category-detail > ul > li > a.active").classList.remove("active");
        category.classList.add("active");
        $(".navigation-list li:nth-child(2) a").innerText = ((category.parentElement).parentElement).previousElementSibling.innerText;

        $(".navigation-list li:nth-child(3) a").innerText = category.innerText;
    };
}); */

/* Filter */
/* Active button filter */
const filterHeader = $(".filter-header .btn");
filterHeader.forEach((btn) => {
    btn.onclick = () => {
        $(".filter-header .btn-active").classList.remove("btn-active");
        btn.classList.add("btn-active");
    };
});

/* Page */
const filterBtnLeft = $(".filter-footer-btn-left");
const filterBtnRight = $(".filter-footer-btn-right");
let pageActive = $(".page .active");
const pageTotal = $(".page .total");
/* Check button disable */
checkDisable = () => {
    if (pageActive.innerText == pageTotal.innerText) {
        filterBtnRight.classList.add("filter-footer-btn--disable");
    } else {
        filterBtnRight.classList.remove("filter-footer-btn--disable");
    }

    if (pageActive.innerText == 1) {
        filterBtnLeft.classList.add("filter-footer-btn--disable");
    } else {
        filterBtnLeft.classList.remove("filter-footer-btn--disable");
    }
};

const element = document.querySelector(".pagination ul");
let totalPages = pageTotal.innerText - 0;
let page = pageActive.innerText - 0;

filterBtnRight.onclick = () => {
    var page = pageActive.innerText - 0;
    if (pageActive.innerText - 0 < pageTotal.innerText - 0) {
        pageActive.innerText = pageActive.innerText - 0 + 1;
    }

    createPagination(totalPages, page + 1) // call function to display page active in Pagination
    checkDisable();
};

filterBtnLeft.onclick = () => {
    var page = pageActive.innerText - 0;
    if (pageActive.innerText - 0 > 1) {
        pageActive.innerText = pageActive.innerText - 1;
    }

    createPagination(totalPages, page - 1); // call function to display page active in Pagination
    checkDisable();
};

/* Pagination */

//calling function with passing parameters and adding inside element which is ul tag
element.innerHTML = createPagination(totalPages, page);
function createPagination(totalPages, page) {
    let liTag = '';
    let active;
    let beforePage = page - 1;
    let afterPage = page + 1;
    if (page > 1) { //show the next button if the page value is greater than 1
        liTag += `<li class="btn prev" onclick="createPagination(totalPages, ${page - 1})"><span><i class="fas fa-angle-left"></i></span></li>`;
    }

    if (page > 2) { //if page value is less than 2 then add 1 after the previous button
        liTag += `<li class="first numb" onclick="createPagination(totalPages, 1)"><span>1</span></li>`;
        if (page > 3) { //if page value is greater than 3 then add this (...) after the first li or page
            liTag += `<li class="dots"><span>...</span></li>`;
        }
    }

    // how many pages or li show before the current li
    if (page == totalPages) {
        beforePage = beforePage - 2;
    } else if (page == totalPages - 1) {
        beforePage = beforePage - 1;
    }
    // how many pages or li show after the current li
    if (page == 1) {
        afterPage = afterPage + 2;
    } else if (page == 2) {
        afterPage = afterPage + 1;
    }

    for (var plength = beforePage; plength <= afterPage; plength++) {
        if (plength > totalPages) { //if plength is greater than totalPage length then continue
            continue;
        }
        if (plength == 0) { //if plength is 0 than add +1 in plength value
            plength = plength + 1;
        }
        if (page == plength) { //if page is equal to plength than assign active string in the active variable
            active = "active";
            pageActive.innerText = plength;
            checkDisable();
        } else { //else leave empty to the active variable
            active = "";
        }
        liTag += `<li class="numb ${active}" onclick="createPagination(totalPages, ${plength})"><span>${plength}</span></li>`;
    }

    if (page < totalPages - 1) { //if page value is less than totalPage value by -1 then show the last li or page
        if (page < totalPages - 2) { //if page value is less than totalPage value by -2 then add this (...) before the last li or page
            liTag += `<li class="dots"><span>...</span></li>`;
        }
        liTag += `<li class="last numb" onclick="createPagination(totalPages, ${totalPages})"><span>${totalPages}</span></li>`;
    }

    if (page < totalPages) { //show the next button if the page value is less than totalPage(20)
        liTag += `<li class="btn next" onclick="createPagination(totalPages, ${page + 1})"><span><i class="fas fa-angle-right"></i></span></li>`;
    }
    element.innerHTML = liTag; //add li tag inside ul tag
    return liTag; //reurn the li tag
}


// Hover phần màu


var new_img = document.getElementById('product-img-default');

var obj = document.getElementById('color_small_2');

obj.onmouseover = function () {
    new_img.removeAttribute('src');
    new_img.setAttribute('src', './assets/img/b1_default_img_1.jpg')

};
obj.onmouseleave = function () {
    new_img.removeAttribute('src');
    new_img.setAttribute('src', './assets/img/b8_default_img.jpg')
}


var obj_2 = document.getElementById('color_small_3');

obj_2.onmouseover = function () {
    new_img.removeAttribute('src');
    new_img.setAttribute('src', './assets/img/b9_default_img_4.jpg')

};
obj_2.onmouseleave = function () {
    new_img.removeAttribute('src');
    new_img.setAttribute('src', './assets/img/b8_default_img.jpg')
}


var obj_3 = document.getElementById('color_small_4');

obj_3.onmouseover = function () {
    new_img.removeAttribute('src');
    new_img.setAttribute('src', './assets/img/b6_default_img_3.jpg')

};
obj_3.onmouseleave = function () {
    new_img.removeAttribute('src');
    new_img.setAttribute('src', './assets/img/b8_default_img.jpg')
}


var obj_4 = document.getElementById('color_small_4');

obj_3.onmouseover = function () {
    new_img.removeAttribute('src');
    new_img.setAttribute('src', './assets/img/b6_default_img_3.jpg')

};
obj_3.onmouseleave = function () {
    new_img.removeAttribute('src');
    new_img.setAttribute('src', './assets/img/b8_default_img.jpg')
}

var obj_3 = document.getElementById('color_small_4');

obj_3.onmouseover = function () {
    new_img.removeAttribute('src');
    new_img.setAttribute('src', './assets/img/b6_default_img_3.jpg')

};
obj_3.onmouseleave = function () {
    new_img.removeAttribute('src');
    new_img.setAttribute('src', './assets/img/b8_default_img.jpg')
}

var obj_3 = document.getElementById('color_small_4');

obj_3.onmouseover = function () {
    new_img.removeAttribute('src');
    new_img.setAttribute('src', './assets/img/b6_default_img_3.jpg')

};
obj_3.onmouseleave = function () {
    new_img.removeAttribute('src');
    new_img.setAttribute('src', './assets/img/b8_default_img.jpg')
}

var obj_3 = document.getElementById('color_small_4');

obj_3.onmouseover = function () {
    new_img.removeAttribute('src');
    new_img.setAttribute('src', './assets/img/b6_default_img_3.jpg')

};
obj_3.onmouseleave = function () {
    new_img.removeAttribute('src');
    new_img.setAttribute('src', './assets/img/b8_default_img.jpg')
}

var obj_3 = document.getElementById('color_small_4');

obj_3.onmouseover = function () {
    new_img.removeAttribute('src');
    new_img.setAttribute('src', './assets/img/b6_default_img_3.jpg')

};
obj_3.onmouseleave = function () {
    new_img.removeAttribute('src');
    new_img.setAttribute('src', './assets/img/b8_default_img.jpg')
}
var obj_3 = document.getElementById('color_small_4');

obj_3.onmouseover = function () {
    new_img.removeAttribute('src');
    new_img.setAttribute('src', './assets/img/b6_default_img_3.jpg')

};
obj_3.onmouseleave = function () {
    new_img.removeAttribute('src');
    new_img.setAttribute('src', './assets/img/b8_default_img.jpg')
}

var obj_3 = document.getElementById('color_small_4');

obj_3.onmouseover = function () {
    new_img.removeAttribute('src');
    new_img.setAttribute('src', './assets/img/b6_default_img_3.jpg')

};
obj_3.onmouseleave = function () {
    new_img.removeAttribute('src');
    new_img.setAttribute('src', './assets/img/b8_default_img.jpg')
}






