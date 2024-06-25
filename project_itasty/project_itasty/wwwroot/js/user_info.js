var list_analyze_recipe = [];
var temp_analyze_recipe = [];
var user_id = sessionStorage.userId;
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
        $(".cr-slider-wrap input").after('<span class=""><i class="fa-solid fa-magnifying-glass-plus"></i></span>');
        $(".cr-slider-wrap input").before('<span class=""><i class="fa-solid fa-magnifying-glass-minus"></i></span>');
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
    $("#radio_follow").on('change', function () {
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
			<div class="list_analyze_recipe">
			</div>
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
			    <input class="form-check-input" type="checkbox" id="check_last_year" name="check_year" checked />
			    <label for="check_last_year">最近一年</label>
		    </div>
		    <div class="form-switch d-inline">
			    <input class="form-check-input" type="checkbox" id="check_two_year" name="check_year" />
			    <label for="check_two_year">最近兩年</label>
		    </div>
        `);
    });

    //時間篩選:季
    $("#radio_analyze_season").on('change', function () {
        $(".area_select_time").html(`
			<div class="form-switch d-inline">
				<input class="form-check-input" type="checkbox" id="check_last_season" name="check_season" checked />
				<label for="check_last_season">最近一季</label>
			</div>
			<div class="form-switch d-inline">
				<input class="form-check-input" type="checkbox" id="check_two_season" name="check_season" />
				<label for="check_two_season">最近兩季</label>
			</div>
			<div class="form-switch d-inline">
				<input class="form-check-input" type="checkbox" id="check_three_season" name="check_season" />
				<label for="check_three_season">最近三季</label>
			</div>
        `);
    });

    //時間篩選:月
    $("#radio_analyze_month").on('change', function () {
        $(".area_select_time").html(`
			<div class="form-switch d-inline">
				<input class="form-check-input" type="checkbox" id="check_last_month" name="check_month" checked />
				<label for="check_last_month">最近一月</label>
			</div>
			<div class="form-switch d-inline">
				<input class="form-check-input" type="checkbox" id="check_two_month" name="check_month" />
				<label for="check_two_month">最近兩月</label>
			</div>
        `);
    });

    //時間篩選:週
    $("#radio_analyze_week").on('change', function () {
        $(".area_select_time").html(`
		    <div class="form-switch d-inline">
			    <input class="form-check-input" type="checkbox" id="check_last_week" name="check_week" checked />
			    <label for="check_last_week">最近一週</label>
		    </div>
		    <div class="form-switch d-inline">
			    <input class="form-check-input" type="checkbox" id="check_two_week" name="check_week" />
			    <label for="check_two_week">最近兩週</label>
		    </div>
		    <div class="form-switch d-inline">
			    <input class="form-check-input" type="checkbox" id="check_three_week" name="check_week" />
			    <label for="check_three_week">最近三週</label>
		    </div>
        `);
    });

    //食譜選擇
    $("input[name='chekcbox_select_recipe']").on("change", function () {
        if ($(this).is(":checked")) {
            //選擇上限
            if ($("input[name='chekcbox_select_recipe']:checked").length > 3) {
                this.checked = false;
            }
            //紀錄選取
            else {
                temp_analyze_recipe.push({
                    "recipe_name": $(this).attr("recipe_name"),
                    "recipe_id": $(this).attr("recipe_id")
                });
            }
        }
        //取消選取
        else {
            temp_analyze_recipe = temp_analyze_recipe.filter(elm => elm.recipe_id != $(this).attr("recipe_id"))
        }
    });

    //視窗關閉觸發事件
    $("#modal_select_recipe").on("hidden.bs.modal", function () {
        show_analyze_recipe();
    });

    //視窗開啟觸發事件
    $("#modal_select_recipe").on("show.bs.modal", function () {
        temp_analyze_recipe = list_analyze_recipe.slice();
        $("input[name='chekcbox_select_recipe']").each(function () {
            if (temp_analyze_recipe.findIndex(x => x.recipe_id === $(this).attr("recipe_id")) != -1) {
                $(this).prop("checked", true);
            }
            else {
                $(this).prop("checked", false);
            }
        });
    });

    //分析圖表
    $("#btn_analyze").on("click", async function () {
        let datasets = [];
        let alanyze_x;
        const barColors = ["red", "green", "blue", "orange", "brown"];
        $(".area_analyze_chart").html("");
        if ($("#radio_analyze_year").is(":checked")) {
            console.log("year");
            if ($("#check_last_year").is(":checked") && $("#radio_analyze_fan").is(":checked")) {
                await get_follower_week(user_id, getDaysInYear(new Date().getFullYear() - 1), getDayOfYear())
                    .then(function (data) {
                        datasets.push({
                            label: '最近一年',
                            data: data.alanyze_y,
                            borderColor: "blue",
                            fill: false
                        });
                        alanyze_x = data.alanyze_x;
                    });
                //get_follower_year(12, 1);
            }
            if ($("#check_two_year").is(":checked") && $("#radio_analyze_fan").is(":checked")) {
                await get_follower_week(user_id, getDaysInYear(new Date().getFullYear() - 2), (getDayOfYear() + getDaysInYear(new Date().getFullYear() - 1)))
                    .then(function (data) {
                        datasets.push({
                            label: '最近兩年',
                            data: data.alanyze_y,
                            borderColor: "red",
                            fill: false
                        });
                        alanyze_x = data.alanyze_x;
                    });
                //get_follower_year(24, 2);
            }
            if ($("#check_last_year").is(":checked") && $("#radio_analyze_view").is(":checked")) {
                for (var i of list_analyze_recipe) {
                    await get_recipeview(i.recipe_id, getDaysInYear(new Date().getFullYear() - 1), getDayOfYear())
                        .then(function (data) {
                            datasets.push({
                                label: '最近一年',
                                data: data.alanyze_y,
                                borderColor: "blue",
                                fill: false
                            });
                            alanyze_x = data.alanyze_x;
                        });
                }
            }
            if ($("#check_two_year").is(":checked") && $("#radio_analyze_view").is(":checked")) {
                for (var i of list_analyze_recipe) {
                    await get_recipeview(i.recipe_id, getDaysInYear(new Date().getFullYear() - 2), (getDayOfYear() + getDaysInYear(new Date().getFullYear() - 1)))
                        .then(function (data) {
                            datasets.push({
                                label: '最近兩年',
                                data: data.alanyze_y,
                                borderColor: "blue",
                                fill: false
                            });
                            alanyze_x = data.alanyze_x;
                        });
                }
            }
        }
        else if ($("#radio_analyze_season").is(":checked")) {
            console.log("season");
            if ($("#check_last_season").is(":checked") && $("#radio_analyze_fan").is(":checked")) {
                await get_follower_week(user_id, getQuarterInfo(1).days, Math.ceil((new Date() - new Date(getQuarterInfo(1).lastDay)) / (1000 * 60 * 60 * 24)) - 1)
                    .then(function (data) {
                        datasets.push({
                            label: '最近一季',
                            data: data.alanyze_y,
                            borderColor: "blue",
                            fill: false
                        });
                        alanyze_x = data.alanyze_x;
                    });
            }
            if ($("#check_two_season").is(":checked") && $("#radio_analyze_fan").is(":checked")) {
                await get_follower_week(user_id, getQuarterInfo(2).days, Math.ceil((new Date() - new Date(getQuarterInfo(2).lastDay)) / (1000 * 60 * 60 * 24)) - 1)
                    .then(function (data) {
                        datasets.push({
                            label: '最近兩季',
                            data: data.alanyze_y,
                            borderColor: "blue",
                            fill: false
                        });
                        alanyze_x = data.alanyze_x;
                    });
            }
            if ($("#check_three_season").is(":checked") && $("#radio_analyze_fan").is(":checked")) {
                await get_follower_week(user_id, getQuarterInfo(3).days, Math.ceil((new Date() - new Date(getQuarterInfo(3).lastDay)) / (1000 * 60 * 60 * 24)) - 1)
                    .then(function (data) {
                        datasets.push({
                            label: '最近三季',
                            data: data.alanyze_y,
                            borderColor: "blue",
                            fill: false
                        });
                        alanyze_x = data.alanyze_x;
                    });
            }
            if ($("#check_last_season").is(":checked") && $("#radio_analyze_view").is(":checked")) {
                for (var i of list_analyze_recipe) {
                    await get_recipeview(i.recipe_id, getQuarterInfo(1).days, Math.ceil((new Date() - new Date(getQuarterInfo(1).lastDay)) / (1000 * 60 * 60 * 24)) - 1)
                        .then(function (data) {
                            datasets.push({
                                label: '最近一季',
                                data: data.alanyze_y,
                                borderColor: "blue",
                                fill: false
                            });
                            alanyze_x = data.alanyze_x;
                        });
                }
            }
            if ($("#check_two_season").is(":checked") && $("#radio_analyze_view").is(":checked")) {
                for (var i of list_analyze_recipe) {
                    await get_recipeview(i.recipe_id, getQuarterInfo(2).days, Math.ceil((new Date() - new Date(getQuarterInfo(2).lastDay)) / (1000 * 60 * 60 * 24)) - 1)
                        .then(function (data) {
                            datasets.push({
                                label: '最近兩季',
                                data: data.alanyze_y,
                                borderColor: "blue",
                                fill: false
                            });
                            alanyze_x = data.alanyze_x;
                        });
                }
            }
            if ($("#check_three_season").is(":checked") && $("#radio_analyze_view").is(":checked")) {
                for (var i of list_analyze_recipe) {
                    await get_recipeview(i.recipe_id, getQuarterInfo(3).days, Math.ceil((new Date() - new Date(getQuarterInfo(3).lastDay)) / (1000 * 60 * 60 * 24)) - 1)
                        .then(function (data) {
                            datasets.push({
                                label: '最近三季',
                                data: data.alanyze_y,
                                borderColor: "blue",
                                fill: false
                            });
                            alanyze_x = data.alanyze_x;
                        });
                }
            }
        }
        else if ($("#radio_analyze_month").is(":checked")) {
            console.log("month");
            if ($("#check_last_month").is(":checked") && $("#radio_analyze_fan").is(":checked")) {
                await get_follower_week(user_id, getPreviousMonthDays(1), (new Date).getDate())
                    .then(function (data) {
                        datasets.push({
                            label: '最近一個月',
                            data: data.alanyze_y,
                            borderColor: "blue",
                            fill: false
                        });
                        alanyze_x = data.alanyze_x;
                    });
            }
            if ($("#check_two_month").is(":checked") && $("#radio_analyze_fan").is(":checked")) {
                await get_follower_week(user_id, getPreviousMonthDays(2), (new Date).getDate() + getPreviousMonthDays(1))
                    .then(function (data) {
                        datasets.push({
                            label: '最近兩個月',
                            data: data.alanyze_y,
                            borderColor: "red",
                            fill: false
                        });
                        alanyze_x = data.alanyze_x;
                    });
            }
            if ($("#check_last_month").is(":checked") && $("#radio_analyze_view").is(":checked")) {
                for (var i of list_analyze_recipe) {
                    await get_recipeview(i.recipe_id, getPreviousMonthDays(1), (new Date).getDate())
                        .then(function (data) {
                            datasets.push({
                                label: '最近一個月',
                                data: data.alanyze_y,
                                borderColor: "blue",
                                fill: false
                            });
                            alanyze_x = data.alanyze_x;
                        });
                }
            }
            if ($("#check_two_month").is(":checked") && $("#radio_analyze_view").is(":checked")) {
                for (var i of list_analyze_recipe) {
                    await get_recipeview(i.recipe_id, getPreviousMonthDays(2), (new Date).getDate() + getPreviousMonthDays(1))
                        .then(function (data) {
                            datasets.push({
                                label: '最近兩個月',
                                data: data.alanyze_y,
                                borderColor: "blue",
                                fill: false
                            });
                            alanyze_x = data.alanyze_x;
                        });
                }
            }
        }
        else if ($("#radio_analyze_week").is(":checked")) {
            console.log("week");
            if ($("#check_last_week").is(":checked") && $("#radio_analyze_fan").is(":checked")) {
                await get_follower_week(user_id, 7, (new Date).getDay() + 1)
                    .then(function (data) {
                        datasets.push({
                            label: '最近一週',
                            data: data.alanyze_y,
                            borderColor: "red",
                            fill: false
                        });
                        alanyze_x = data.alanyze_x;
                    });
            }
            if ($("#check_two_week").is(":checked") && $("#radio_analyze_fan").is(":checked")) {
                await get_follower_week(user_id, 7, (new Date).getDay() + 1 + 7)
                    .then(function (data) {
                        datasets.push({
                            label: '最近兩週',
                            data: data.alanyze_y,
                            borderColor: "red",
                            fill: false
                        });
                        alanyze_x = data.alanyze_x;
                    });
            }
            if ($("#check_three_week").is(":checked") && $("#radio_analyze_fan").is(":checked")) {
                await get_follower_week(user_id, 7, (new Date).getDay() + 1 + 14)
                    .then(function (data) {
                        datasets.push({
                            label: '最近三週',
                            data: data.alanyze_y,
                            borderColor: "red",
                            fill: false
                        });
                        alanyze_x = data.alanyze_x;
                    });
            }
            if ($("#check_last_week").is(":checked") && $("#radio_analyze_view").is(":checked")) {
                for (var i of list_analyze_recipe) {
                    await get_recipeview(i.recipe_id, 7, (new Date).getDay() + 1)
                        .then(function (data) {
                            datasets.push({
                                label: '最近一週',
                                data: data.alanyze_y,
                                borderColor: "blue",
                                fill: false
                            });
                            alanyze_x = data.alanyze_x;
                        });
                }
            }
            if ($("#check_two_week").is(":checked") && $("#radio_analyze_view").is(":checked")) {
                for (var i of list_analyze_recipe) {
                    await get_recipeview(i.recipe_id, 7, (new Date).getDay() + 1 + 7)
                        .then(function (data) {
                            datasets.push({
                                label: '最近兩週',
                                data: data.alanyze_y,
                                borderColor: "blue",
                                fill: false
                            });
                            alanyze_x = data.alanyze_x;
                        });
                }
            }
            if ($("#check_three_week").is(":checked") && $("#radio_analyze_view").is(":checked")) {
                for (var i of list_analyze_recipe) {
                    await get_recipeview(i.recipe_id, 7, (new Date).getDay() + 1 + 14)
                        .then(function (data) {
                            datasets.push({
                                label: '最近三週',
                                data: data.alanyze_y,
                                borderColor: "blue",
                                fill: false
                            });
                            alanyze_x = data.alanyze_x;
                        });
                }
            }
        }

        for (i = 0; i < datasets.length; i++) {
            datasets[i].borderColor = barColors[i];
        }
        set_alanyze_canvas(datasets, alanyze_x);
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
            $(".text_user_info > p").html($("#input_user_info").val());
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
    $("#radio_follow").prop('checked') ? $(".area_follow").show() : $(".area_follow").hide();
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

//清空要分析的食譜選擇
function reset_analyze_recipe() {
    list_analyze_recipe = [];
    console.log(list_analyze_recipe);
}

//儲存要分析的食譜選擇
function save_analyze_recipe() {
    list_analyze_recipe = temp_analyze_recipe.slice();
    console.log(list_analyze_recipe);
}

//顯示要分析的食譜選擇
function show_analyze_recipe() {
    $(".list_analyze_recipe").html("");
    for (let i in Array.from(list_analyze_recipe)) {
        console.log(i)
        $(".list_analyze_recipe").html(
            $(".list_analyze_recipe").html() + `
		    <p>
			    ${list_analyze_recipe[i].recipe_name}
			    <button onclick="remove_analyze_recipe(${list_analyze_recipe[i].recipe_id})"><i class="fa-regular fa-square-minus text-danger"></i></button>
		    </p>

        `);
    }
}

//移除要分析的食譜選擇
function remove_analyze_recipe(recipe_id) {
    list_analyze_recipe = list_analyze_recipe.filter(elm => elm.recipe_id != recipe_id)
    show_analyze_recipe();
}

//取得前幾個月有幾天
function getPreviousMonthDays(offset) {
    const date = new Date();
    let year = date.getFullYear();
    let month = date.getMonth();

    // 计算前 offset 个月的年份和月份
    month -= offset;
    while (month < 0) {
        month += 12;
        year -= 1;
    }

    return new Date(year, month + 1, 0).getDate();
}

//取得今天是今年第幾天
function getDayOfYear() {
    const today = new Date();
    const startOfYear = new Date(today.getFullYear(), 0, 1); // 当年的第一天
    const diffInMillis = today - startOfYear; // 当前日期和当年第一天的时间差（以毫秒为单位）
    const dayOfYear = Math.ceil(diffInMillis / (1000 * 60 * 60 * 24)); // 转换为天数并向上取整

    return dayOfYear;
}

//取得特定年份有幾天
function getDaysInYear(year) {
    // 闰年的条件：能被4整除且不能被100整除，或能被400整除
    if ((year % 4 === 0 && year % 100 !== 0) || (year % 400 === 0)) {
        return 366;
    } else {
        return 365;
    }
}

//取得特定季有幾天與最後一天
function getQuarterInfo(offsetQuarters) {
    const today = new Date();
    let year = today.getFullYear();
    let month = today.getMonth(); // 月份从0开始（0-11）

    // 确定当前季
    const currentQuarter = Math.floor(month / 3) + 1;

    // 计算目标季的起始月份和年份
    let targetQuarterYear = year;
    let targetQuarterStartMonth = (currentQuarter - 1 - offsetQuarters) * 3;

    // 如果目标季的起始月份小于0，需要调整年份
    while (targetQuarterStartMonth < 0) {
        targetQuarterStartMonth += 12;
        targetQuarterYear -= 1;
    }

    // 计算目标季的天数
    let targetQuarterDays = 0;
    for (let i = 1; i <= 3; i++) {
        targetQuarterDays += new Date(targetQuarterYear, targetQuarterStartMonth + i, 0).getDate();
    }

    // 确定目标季的最后一天
    const targetQuarterEndMonth = targetQuarterStartMonth + 2;
    const lastDayOfTargetQuarter = new Date(targetQuarterYear, targetQuarterEndMonth + 1, 0);

    return {
        days: targetQuarterDays,
        lastDay: lastDayOfTargetQuarter
    };
}

//取得follower年資料
function get_follower_year(day_length, day_shift) {
    let d;
    let last_date = [];
    let last_date_fan = [];

    d = new Date();
    d.setFullYear(2024 - day_shift);
    d.setMonth(0);
    for (let i = 1; i <= day_length; i++) {
        last_date.push(`${d.getFullYear()}/${d.getMonth() + i}`);
        last_date_fan.push(0);
    }

    $.get("/api/userapi/follower/1", function (data, status) {
        for (let i of data) {

            //判斷追隨年、月份
            let diff_follow_year = (new Date(i.followDate).getFullYear() < d.getFullYear()) ? 0 : (new Date(i.followDate).getFullYear() - d.getFullYear());
            let diff_follow_month = (new Date(i.followDate).getFullYear() < d.getFullYear()) ? 0 : new Date(i.followDate).getMonth() + diff_follow_year * 12;
            //判斷退追年、月份
            let diff_unfollow_year = (i.unfollowDate == null) ? day_length : (new Date(i.unfollowDate).getFullYear() < d.getFullYear()) ? 0 : (new Date(i.unfollowDate).getFullYear() - d.getFullYear());
            let diff_unfollow_month = (i.unfollowDate == null) ? diff_unfollow_year : (new Date(i.unfollowDate).getFullYear() < d.getFullYear()) ? 0 : new Date(i.unfollowDate).getMonth() + diff_unfollow_year * 12;
            //追隨開始~退追之間的月份+1，同一個月則發生不紀錄
            for (let j = diff_follow_month; j < diff_unfollow_month; j++) {
                last_date_fan[j] += 1;
            }
        }
        set_alanyze_canvas(last_date_fan, last_date);
    });
}
//取得follower週資料
function get_follower_week(user_id, day_length, day_shift) {
    let d;
    let array_day = [];
    let array_day_fan = [];

    for (let i = 0; i < day_length; i++) {
        d = new Date();
        d.setDate(d.getDate() - (day_length + day_shift - 1) + i);
        array_day.push(`Day ${i + 1}`);
        array_day_fan.push(0);
    }
    return new Promise((resolve) => {
        $.get(`/api/userapi/follower/${user_id}`, function (data, status) {

            d = new Date();
            d.setDate(d.getDate() - (day_length + day_shift - 1));
            for (let i of data) {

                //let diff_follow_day = (day_length - 1) - Math.floor((d - new Date(i.followDate)) / (1000 * 60 * 60 * 24));
                //let diff_unfollow_day = (i.unfollowDate == null) ? day_length : (day_length - 1) - Math.floor((d - new Date(i.unfollowDate)) / (1000 * 60 * 60 * 24));

                //for (let j = diff_follow_day; j < diff_unfollow_day; j++) {
                //    last_week_fan[j] += 1;
                //}

                let diff_follow_day = Math.ceil(((new Date(i.followDate) < d) ? 0 : (new Date(i.followDate) - d)) / (1000 * 60 * 60 * 24));
                let diff_unfollow_day = (i.unfollowDate == null) ? day_length : Math.ceil(((new Date(i.unfollowDate) < d) ? 0 : (new Date(i.unfollowDate) - d)) / (1000 * 60 * 60 * 24));

                for (let j = diff_follow_day; j < diff_unfollow_day; j++) {
                    array_day_fan[j] += 1;
                }
            }
            if ($("#radio_analyze_grow").is(":checked")) {
                for (let i = array_day_fan.length - 1; i >= 0; i--) {
                    array_day_fan[i] -= array_day_fan[0];
                }
            }
            resolve({
                alanyze_x: array_day,
                alanyze_y: array_day_fan
            });
            //set_alanyze_canvas(array_day_fan, array_day);
        });
    });
}

//取得食譜點閱資料
function get_recipeview(recipe_id, day_length, day_shift) {
    let d;
    let array_day = [];
    let array_day_view = [];
    for (let i = 0; i < day_length; i++) {
        d = new Date();
        d.setDate(d.getDate() - (day_length + day_shift - 1) + i);
        array_day.push(`Day ${i + 1}`);
        array_day_view.push(0);
    }
    return new Promise((resolve) => {
        $.get(`/api/userapi/recipeview/${recipe_id}`, function (data, status) {
            d = new Date();
            d.setDate(d.getDate() - (day_length + day_shift - 1));
            for (let i of data) {
                let diff_view_day = Math.ceil(((new Date(i.viewDate) < d) ? 0 : (new Date(i.viewDate) - d)) / (1000 * 60 * 60 * 24));
                //console.log(diff_view_day);
                for (let j = diff_view_day; j < day_length; j++) {
                    array_day_view[j] += i.viewNum;
                }
            }
            if ($("#radio_analyze_grow").is(":checked")) {
                for (let i = array_day_view.length - 1; i >= 0; i--) {
                    array_day_view[i] -= array_day_view[0];
                }
            }

            resolve({
                alanyze_x: array_day,
                alanyze_y: array_day_view
            });
        });

    });
}


//設定圖表
function set_alanyze_canvas(datasets, alanyze_x) {
    const color = ["text-danger", "text-success", "text-primary"];
    $(".area_analyze_chart").html("");
    for (var i in list_analyze_recipe) {
        console.log(i);
        $(".area_analyze_chart").append(`<span class="${color[i]} me-3">${list_analyze_recipe[i].recipe_name}</span>`);
    }
    $(".area_analyze_chart").append('<canvas id="myChart"></canvas>');

    new Chart("myChart", {
        type: "line",
        data: {
            labels: alanyze_x,
            datasets: datasets
            //    datasets: [{
            //    //    data: [860, 1140, 1060, 1060, 1070, 1110, 1330, 2210, 7830, 2478],
            //    //    borderColor: "red",
            //    //    fill: false
            //    //}, {
            //    //    data: [1600, 1700, 1700, 1900, 2000, 2700, 4000, 5000, 6000, 7000],
            //    //    borderColor: "green",
            //    //    fill: false
            //    //}, {
            //    //    data: [300, 700, 2000, 5000, 6000, 4000, 2000, 1000, 200, 100],
            //    //    borderColor: "blue",
            //    //    fill: false
            //    data: alanyze_y,
            //    borderColor: "blue",
            //    fill: false
            //}]
        },
        options: {
            legend: { display: false }
        }
    });
}

function get_fan(num_page, user_id) {
    $.ajax({
        type: "get",
        url: `/api/userapi/fan/${user_id}`,
        data: {
            "num_page": num_page
        },
        success: function (e) {
            console.log(e);
            $("#list_fan").html("");
            for (var i of e.query) {
                console.log(i);
                $("#list_fan").html(
                    $("#list_fan").html() + `
                <div class="position-relative">
					<img src="data:image/jpg;base64,${(i.userPhoto != undefined) ? i.userPhoto : ""}" class="rounded-circle">

					<div><a href="~/user/${i.userEmail}">${i.userName}</a></div>

					<div>
						<div class="d-inline"> 食譜</div >
                <div class="d-inline"> 粉絲數</div>
					</div >
                <div>${(i.userIntro != null) ? i.userIntro : ""}</div >
				</div >
                `);
            }
        },
        error: function (xmlhttpreq, textstatus) {
            console.log(xmlhttpreq);
            console.log(textstatus);
        }
    });
}

