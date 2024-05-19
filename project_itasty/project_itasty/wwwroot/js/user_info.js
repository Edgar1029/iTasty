$(document).ready(function () {
    var myCrop;

    $(".btn_save_banner").on("click", function () {
        myCrop.croppie("result", {
            type: "canvas",
            format: "jpeg", //type_img
            quality: 0.85   //compress_ratio
        }).then(function (src) {
            displayCropImg(src, $("#img_user_banner"));
        });
    });

    $("#btn_user_banner").on("click", function () {
        $("#inputFile_user_banner").val("");
        $("#inputFile_user_banner").trigger('click');
    });

    $("#inputFile_user_banner").on("change", function () {
        readFile(this, 450, 150);
        $("#area_user_data").addClass("d-none");
        $("#area_user_img").removeClass("d-none");
        $(".btn_save_icon").addClass("d-none");
        $(".btn_save_banner").removeClass("d-none");
    });

    $(".btn_save_icon").on("click", function () {
        myCrop.croppie("result", {
            type: "canvas",
            format: "jpeg", //type_img
            quality: 0.85   //compress_ratio
        }).then(function (src) {
            displayCropImg(src, $("#img_user_icon"));
        });
    });

    $("#btn_user_icon").on("click", function () {
        $("#inputFile_user_icon").val("");
        $("#inputFile_user_icon").trigger('click');
    });

    $("#inputFile_user_icon").on("change", function () {
        readFile(this, 250, 250);
        $("#area_user_data").addClass("d-none");
        $("#area_user_img").removeClass("d-none");
        $(".btn_save_banner").addClass("d-none");
        $(".btn_save_icon").removeClass("d-none");
    });

    function readFile(input, num_width, num_height) {

        let width_crop = num_width, // 圖片裁切寬度 px 值
            height_crop = num_height, // 圖片裁切高度 px 值
            type_crop = "square", // 裁切形狀: square 為方形, circle 為圓形
            width_preview = 450, // 預覽區塊寬度 px 值
            height_preview = 300, // 預覽區塊高度 px 值
            compress_ratio = 0.85, // 圖片壓縮比例 0~1
            type_img = "jpeg", // 圖檔格式 jpeg png webp
            oldImg = new Image(),
            file, oldImgDataUrl;

        $('#oldImg').croppie('destroy')

        myCrop = $("#oldImg").croppie({
            viewport: { // 裁切區塊
                width: width_crop,
                height: height_crop,
                type: type_crop
            },
            boundary: { // 預覽區塊
                width: width_preview,
                height: height_preview
            }
        });

        if (input.files && input.files[0]) {
            file = input.files[0];
        } else {
            // alert("瀏覽器不支援此功能！建議使用最新版本 Chrome");
            return;
        }

        if (file.type.indexOf("image") == 0) {
            var reader = new FileReader();

            reader.onload = function (e) {
                oldImg.src = e.target.result; // 載入 oldImg 取得圖片資訊
                myCrop.croppie("bind", {
                    url: e.target.result
                });
            };

            reader.readAsDataURL(file);
        } else {
            alert("您上傳的不是圖檔！");
        }
        $(".cr-slider-wrap input").after('<span class=""><i class="fa-solid fa-magnifying-glass-plus h6"></i></span>');
        $(".cr-slider-wrap input").before('<span class=""><i class="fa-solid fa-magnifying-glass-minus h6"></i></span>');
        $(".cr-slider-wrap input").addClass('px-2');
    }

    function displayCropImg(src, img_obj) {
        img_obj.prop('src', src);

        $("#area_user_data").removeClass("d-none");
        $("#area_user_img").addClass("d-none");
    }

});
function user_edit(id) {

    fetch($("#img_user_icon").prop('src'))
        .then(res => res.blob())
        .then(blob => {

            var formData = new FormData();
            formData.append('id', id.toString());
            formData.append('name', $("#input_user_name").prop('value'));
            formData.append('photo', blob);


            $.ajax({
                type: "post",
                url: `/api/userapi`,
                contentType: false,
                processData: false,
                data: formData,
                success: function (e) {
                    $("#modal_user_info").modal("hide");
                    $(".text_user_name").html($("#input_user_name").val());
                    $(".img_user_icon").prop('src', $("#img_user_icon").prop('src'))
                },
                error: function (xmlhttpreq, textstatus) {
                    console.log(xmlhttpreq);
                    console.log(textstatus);
                }
            });

        });


}