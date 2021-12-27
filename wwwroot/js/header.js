const $ = document.querySelector.bind(document);
const $$ = document.querySelectorAll.bind(document);

/******************** Header ********************/
const menuList = $('.menu-list');
    /* Display menu on mobile */
    const loginMobile = $('.login__mobile');
    const menuMobile = $('.menu__mobile-click');
    const heightHeader = header.clientHeight;
    menuMobile.onclick = () => {
        menuList.classList.toggle('display-on-mobile');
        loginMobile.classList.toggle('display-on-mobile');
    }

    /* Display submenu on mobile */
    const subMenus = $$('.subMenu-list');
    const products = $$('.menu-list__products > a');
    for (let i = 0; i < products.length; i++) {
        products[i].addEventListener('click', function(e) {
            subMenus[i].classList.toggle('display-on-mobile');
            (subMenus[i].previousElementSibling).lastElementChild.classList.toggle('rotate');
        })
    }

    /* Disable menu products link */
    if (document.documentElement.clientWidth <= 739) {
        products.forEach(function (product) {
            product.href = '#';
        })
    }

    /* Edit backgroundColor of header when scroll */
    window.addEventListener("scroll", function() {
        let y = window.pageYOffset;
        if (y > 0) {
            $("#main-header").style.backgroundColor = "rgba(255,255,255,1)";
        }
        else {
            $("#main-header").style.backgroundColor = "rgba(255,255,255,0.5)";
        }
    })

