$(document).ready(function () {
    var myCrop;

    //橫幅儲存
    $(".btn_save_banner").on("click", function () {
        myCrop.croppie("result", {
            type: "canvas",
            format: "jpeg", //type_img
            quality: 0.85   //compress_ratio
        }).then(function (src) {
            displayCropImg(src, $("#img_user_banner"));
        });
    });

    //橫幅編輯
    $("#btn_user_banner").on("click", function () {
        $("#inputFile_user_banner").val("");
        $("#inputFile_user_banner").trigger('click');
    });

    //橫幅輸入
    $("#inputFile_user_banner").on("change", function () {
        readFile(this, 450, 150);
        $("#area_user_data").addClass("d-none");
        $("#area_user_img").removeClass("d-none");
        $(".btn_save_icon").addClass("d-none");
        $(".btn_save_banner").removeClass("d-none");
    });

    //頭像儲存
    $(".btn_save_icon").on("click", function () {
        myCrop.croppie("result", {
            type: "canvas",
            format: "jpeg", //type_img
            quality: 0.85   //compress_ratio
        }).then(function (src) {
            displayCropImg(src, $("#img_user_icon"));
        });
    });

    //頭像編輯
    $("#btn_user_icon").on("click", function () {
        $("#inputFile_user_icon").val("");
        $("#inputFile_user_icon").trigger('click');
    });

    //頭像輸入
    $("#inputFile_user_icon").on("change", function () {
        readFile(this, 250, 250);
        $("#area_user_data").addClass("d-none");
        $("#area_user_img").removeClass("d-none");
        $(".btn_save_banner").addClass("d-none");
        $(".btn_save_icon").removeClass("d-none");
    });

    //圖片裁切
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

    //裁切完的圖片輸出
    function displayCropImg(src, img_obj) {
        img_obj.prop('src', src);

        $("#area_user_data").removeClass("d-none");
        $("#area_user_img").addClass("d-none");
    }

    //預設勾選食譜頁籤
    $("#radio_article").prop('checked', true);
    radio_change();

    //切換頁籤顯示的內容
    $("#radio_article").on('change', function () {
        radio_change();
    });
    $("#radio_fan").on('change', function () {
        radio_change();
    });
    $("#radio_analysis").on('change', function () {
        radio_change();
    });

    const xValues = [100, 200, 300, 400, 500, 600, 700, 800, 900, 1000];
    new Chart("myChart", {
        type: "line",
        data: {
            labels: xValues,
            datasets: [{
                data: [860, 1140, 1060, 1060, 1070, 1110, 1330, 2210, 7830, 2478],
                borderColor: "red",
                fill: false
            }, {
                data: [1600, 1700, 1700, 1900, 2000, 2700, 4000, 5000, 6000, 7000],
                borderColor: "green",
                fill: false
            }, {
                data: [300, 700, 2000, 5000, 6000, 4000, 2000, 1000, 200, 100],
                borderColor: "blue",
                fill: false
            }]
        },
        options: {
            legend: { display: false }
        }
    });
});

//發送使用者資訊更新
async function user_edit(id) {

    //fetch($("#img_user_icon").prop('src'))
    //    .then(res => res.blob())
    //    .then(blob => {
    //        console.log(blob);
    //    });

    const user_icon = await get_img_blob($("#img_user_icon").prop('src'))
    const user_banner = await get_img_blob($("#img_user_banner").prop('src'))


    var formData = new FormData();
    formData.append('id', id.toString());
    formData.append('name', $("#input_user_name").prop('value'));
    formData.append('intro', $("#input_user_info").prop('value'));
    formData.append('photo', user_icon);
    formData.append('banner', user_banner);

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
            $(".img_user_banner").prop('src', $("#img_user_banner").prop('src'))
        },
        error: function (xmlhttpreq, textstatus) {
            console.log(xmlhttpreq);
            console.log(textstatus);
        }
    });
}

// 取得圖片blob
async function get_img_blob(src) {
    const response = await fetch(src);
    return response.blob()
}

//切換頁籤顯示的內容
function radio_change() {
    $("#radio_article").prop('checked') ? $(".area_article").show() : $(".area_article").hide();
    $("#radio_fan").prop('checked') ? $(".area_fan").show() : $(".area_fan").hide();
    $("#radio_analysis").prop('checked') ? $(".area_analyze").show() : $(".area_analyze").hide();

}

//圖表
