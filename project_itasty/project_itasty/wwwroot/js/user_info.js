$(document).ready(function () {
    var myCrop;
    var list_analyze_recipe = [];

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

    //分析圖表切換分析項目
    //粉絲數
    $("#radio_analyze_fan").on('change', function () {
        $(".area_analyze_recipe").html(``);
    });

    //點閱數
    $("#radio_analyze_view").on('change', function () {
        $(".area_analyze_recipe").html(`
			<label>選擇食譜：</label>
			<p>
				<i class="fa-solid fa-brush"></i>
				食譜名稱
				<button><i class="fa-regular fa-square-minus"></i></button>
			</p>
			<p>
				<button>
					<i class="fa-regular fa-square-plus" data-bs-toggle="modal" data-bs-target="#modal_select_recipe">加入食譜</i>
				</button>
			</p>
        `);
    });

    //時間篩選:年
    $("#radio_analyze_year").on('change', function () {
        $(".area_select_time").html(`
		    <div class="form-switch d-inline">
			    <input class="form-check-input" type="checkbox" id="check_last_year" />
			    <label for="check_last_year">最近一年</label>
		    </div>
		    <div class="form-switch d-inline">
			    <input class="form-check-input" type="checkbox" id="check_two_year" />
			    <label for="check_two_year">最近兩年</label>
		    </div>
        `);
    });

    //時間篩選:季
    $("#radio_analyze_season").on('change', function () {
        $(".area_select_time").html(`
			<div class="form-switch d-inline">
				<input class="form-check-input" type="checkbox" id="check_last_sea" />
				<label for="check_last_sea">最近一季</label>
			</div>
			<div class="form-switch d-inline">
				<input class="form-check-input" type="checkbox" id="check_two_sea" />
				<label for="check_two_sea">最近兩季</label>
			</div>
			<div class="form-switch d-inline">
				<input class="form-check-input" type="checkbox" id="check_three_sea" />
				<label for="check_three_sea">最近三季</label>
			</div>
        `);
    });

    //時間篩選:月
    $("#radio_analyze_month").on('change', function () {
        $(".area_select_time").html(`
			<div class="form-switch d-inline">
				<input class="form-check-input" type="checkbox" id="check_last_mon" />
				<label for="check_last_mon">最近一月</label>
			</div>
			<div class="form-switch d-inline">
				<input class="form-check-input" type="checkbox" id="check_two_mon" />
				<label for="check_two_mon">最近兩月</label>
			</div>
        `);
    });

    //時間篩選:週
    $("#radio_analyze_week").on('change', function () {
        $(".area_select_time").html(`
		    <div class="form-switch d-inline">
			    <input class="form-check-input" type="checkbox" id="check_last_wee" />
			    <label for="check_last_wee">最近一週</label>
		    </div>
		    <div class="form-switch d-inline">
			    <input class="form-check-input" type="checkbox" id="check_two_wee" />
			    <label for="check_two_wee">最近兩週</label>
		    </div>
		    <div class="form-switch d-inline">
			    <input class="form-check-input" type="checkbox" id="check_three_wee" />
			    <label for="check_three_wee">最近三週</label>
		    </div>
        `);
    });

    //食譜選擇
    $("input[name='chekcbox_select_recipe']").on("change", function () {
        //選擇上限
        if ($("input[name='chekcbox_select_recipe']:checked").length > 3) {
            this.checked = false;
        }
        else {
            list_analyze_recipe.push({
                "recipe_name": 1,
                "recipe_id": 1
            });
        }
        console.log($(this).attr("recipe_name"))
        console.log(list_analyze_recipe);
    });


    //分析圖表
    if ($('#myChart').length > 0) {
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
    }
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

//追蹤or取消追蹤
function user_follow(user_id, follower_id, btn_user_follow) {

    $.ajax({
        type: "post",
        url: `/api/userapi`,
        data: {
            "user_id": user_id.toString(),
            "follower_id": follower_id.toString(),
        },
        success: function (e) {
            if (e)
                btn_user_follow.innerText = "追蹤";
            else
                btn_user_follow.innerText = "取消追蹤";

        },
        error: function (xmlhttpreq, textstatus) {
            console.log(xmlhttpreq);
            console.log(textstatus);
        }
    });
}
