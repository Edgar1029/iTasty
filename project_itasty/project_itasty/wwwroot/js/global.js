$(document).ready(function () {
    var displayleft = 0;
    var displayright = 0;
    var checklogin = sessionStorage.getItem("userId");

    $(".loginsignbtn").empty();
    if (checklogin != 0) {
        $(".loginsignbtn").append(`<a href="/UserRegister/Logout" class="sign" style="margin-right:20px;">LogOut</a>`)
    } else {
        $(".loginsignbtn").append(`<a href="/UserRegister/Create" class="sign">Sign up</a>`)
    }

    $(".nav-left").on("click", function () {
        $(this).toggleClass("change");
        if (displayleft == 0) {
            $(".left-title").css("display", "block");
            displayleft = 1;
        }
        else {
            $(".left-title").css("display", "none");
            displayleft = 0;
        }
    })

    $(".nav-right").on("click", function () {
        $(".nav-profile-icon").remove();
        if (displayright == 0) {
            $(".right-title").css("display", "block");
            $(".nav-right").prepend(`<i class="fa-solid fa-user fa-2x nav-profile-icon"></i>`);
            displayright = 1;
        }
        else {
            $(".right-title").css("display", "none");
            $(".nav-right").prepend(`<i class="fa-regular fa-user fa-2x nav-profile-icon"></i>`);
            displayright = 0;
        }
    })
})