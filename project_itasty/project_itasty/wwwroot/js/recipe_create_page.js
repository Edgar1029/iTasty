//食材列最後一列的ID號碼start
var ingredients_content_num = [];
var ingredients_content_last_num = "";
$(".ingredients_content_num").each(function () {
    let id = $(this).attr("id").replace(/[^0-9]/ig, "");
    ingredients_content_num.push(id);
})
ingredients_content_last_num = Math.max.apply(null, ingredients_content_num) + 1;
//食材列最後一列的ID號碼end
//食材標題列最後一列的ID號碼start
var ingredients_title_num = [];
var ingredients_title_last_num = "";
$(".ingredients_title").each(function () {
    let id = $(this).attr("id").replace(/[^0-9]/ig, "");
    ingredients_title_num.push(id);
})
ingredients_title_last_num = Math.max.apply(null, ingredients_title_num) + 1;
//食材標題列最後一列的ID號碼end
//步驟列最後一列的ID號碼start
var step_content_num = [];
var step_content_last_num = "";
$(".step_row_anchor").each(function () {
    let id = $(this).attr("id").replace(/[^0-9]/ig, "");
    step_content_num.push(id);
})
step_content_last_num = Math.max.apply(null, step_content_num) + 1;
//步驟列最後一列的ID號碼end

add_stap_num();//步驟編號初始化
//$("#step_row_1 h4").append(step_content_last_num - 1); 
add_ingredients_num();//食材欄編號初始化
// #ingredients事件代理start
$("#ingredients").on("click", ".ingredients_content_num", function () {
    //用戶輸入儲存變數
    let ingredients = [];
    let ingredients_number = "";
    let ingredients_unit = "";
    let ingredients_kcalg = "";
    let customize_ingredients = "";
    let customize_kcalg = "";
    let customize_ingredients_kcalg = "";
    let customize_ingredients_number = "";
    let customize_ingredients_unit = "";

    //將當前所選容器的ID號碼提取出來
    let content_id = $(this).attr("id");
    let row_num = content_id.replace(/[^0-9]/ig, "");

    //食材輸入start 
    // 將選擇的食材存入ingredients[]
    $("#ingredients").on("click", `#ingredients_content_${row_num} .ingredients_options_btn`, function () {
        // 重製陣列
        ingredients = [];
        $(".ingredients_options_btn").removeClass("ingredients_options_btn_click");
        $(this).addClass("ingredients_options_btn_click");
        var this_p = $(this).find("p");
        this_p.each(function () {
            ingredients.push($(this).text());
        })
    })
    // 將輸入的食材數量存入ingredients_number            
    $(`#ingredients_content_${row_num} .ingredients_number`).on("input", function () {
        ingredients_number = $(this).val();
    })
    // 將選擇的食材單位存入ingredients_unit            
    $(`#ingredients_content_${row_num} .ingredients_unit`).on("change", function () {
        ingredients_unit = $(this).val();
    })
    // 將用戶輸入的食材資訊顯示到畫面上            
    $(`#ingredients_content_${row_num}`).on("click", ".submit_ingredients", function () {
        let add_p1 = "";
        let add_p2 = "";
        if (ingredients_unit == "g" || ingredients_unit == "ml") {
            ingredients_kcalg = (ingredients[2] * ingredients_number).toFixed(1);
        } else if (ingredients_unit == "kg" || ingredients_unit == "L") {
            ingredients_kcalg = (ingredients[2] * ingredients_number * 1000).toFixed(1);
        } else {
            ingredients_kcalg = 0;
        }

        if (ingredients.length > 1) {
            add_p1 = `<p class="align-middle ingredients_name">${ingredients[0]}</p>
                         <p class="align-middle ingredients_name">${ingredients[1]}</p>
                         <p class=" kcalg d_none">${ingredients_kcalg}</p>
                         <input type="hidden" class="IngredientsName" name="IngredientsName" value="${ingredients[0]}">
                         <input type="hidden" class="IngredientsId" name="IngredientsId" value="${ingredients[3]}">
                         <input type="hidden" class="Kcalg" name="IngredientKcalg" value="${ingredients[2]}">
                         `;
        }

        if (ingredients_unit == "適量") {
            add_p2 = `<p class="align-middle">${ingredients_unit}</p>
                      <input type="hidden" class="IngredientsUnit" name="IngredientsUnit" value="${ingredients_unit}">`;
        }
        else if (ingredients_number != "" && ingredients_unit != "") {
            add_p2 = `<p class="align-middle">${ingredients_number}</p>
                          <p class="align-middle">${ingredients_unit}</p>
                          <input type="hidden" class="IngredientsNumber" name="IngredientsNumber" value="${ingredients_number}">
                          <input type="hidden" class="IngredientsUnit" name="IngredientsUnit" value="${ingredients_unit}">`;
        }


        if (add_p1 != "" && add_p2 != "") {
            $(`#ingredients_content_${row_num} .ingredients_name_row p`).detach();
            $(`#ingredients_content_${row_num} .ingredients_name_row input`).detach();
            $(`#ingredients_content_${row_num} .ingredients_name_row `).append(add_p1);
            $(`#ingredients_content_${row_num} .ingredients_num_unit p`).detach();
            $(`#ingredients_content_${row_num} .ingredients_num_unit input`).detach();
            $(`#ingredients_content_${row_num} .ingredients_num_unit `).append(add_p2);
        }

        if (add_p2 != "") {
            return;
        }
        console.log("p1:" + add_p1);
        console.log("p2:" + add_p2);
    })

    // 將用戶自訂的食材名稱存入customize_ingredients            
    $(`#ingredients_content_${row_num} #customize_ingredients`).on("input", function () {
        customize_ingredients = $(this).val();
    })
    // 將用戶自訂的食材卡洛里存入customize_kcalg            
    $(`#ingredients_content_${row_num} #customize_kcalg`).on("input", function () {
        customize_kcalg = $(this).val();
    })
    // 將用戶自訂的食材數量存入customize_ingredients_number            
    $(`#ingredients_content_${row_num} #customize_ingredients_number`).on("input", function () {
        customize_ingredients_number = $(this).val();
    })
    // 將用戶自訂的食材單位存入customize_ingredients_unit            
    $(`#ingredients_content_${row_num} #customize_ingredients_unit`).on("change", function () {
        customize_ingredients_unit = $(this).val();
    })
    // 將用戶自訂的食材資訊顯示在畫面上            
    $(`#ingredients_content_${row_num}`).on("click", ".submit_customize_ingredients", function () {
        let add_p3 = "";
        let add_p4 = "";

        if (customize_kcalg == "") {
            customize_ingredients_kcalg = "不顯示";
        }
        else if (customize_ingredients_unit == "g" || customize_ingredients_unit == "ml") {
            customize_ingredients_kcalg = (customize_kcalg / 100 * customize_ingredients_number).toFixed(1);
        }
        else if (customize_ingredients_unit == "kg" || customize_ingredients_unit == "L") {
            customize_ingredients_kcalg = (customize_kcalg * 10 * customize_ingredients_number).toFixed(1);
        }
        else { customize_ingredients_kcalg = "0"; }

        if (customize_ingredients != "" && customize_kcalg == "") {
            add_p3 = `<p class="align-middle ingredients_name">${customize_ingredients}</p>
                      <p class=" kcalg d_none">${customize_ingredients_kcalg}</p>
                      <input type="hidden" class="IngredientsName" name="IngredientsName" value="${customize_ingredients}">
                      
                      `;
        }
        else if (customize_ingredients != "" && customize_kcalg != "") {
            add_p3 = `<p class="align-middle ingredients_name">${customize_ingredients}</p>
                      <p class="ingredients_name">100克 ${customize_kcalg}卡</p>
                      <p class="kcalg d_none">${customize_ingredients_kcalg}</p>
                      <input type="hidden" class="IngredientsName" name="IngredientsName" value="${customize_ingredients}">
                      <input type="hidden" class="Kcalg" name="IngredientKcalg" value="${customize_kcalg / 100}">
                      `;
        }

        if (customize_ingredients_unit == "適量") {
            add_p4 = `<p class="align-middle">${customize_ingredients_unit}</p>
                      <input type="hidden" class="IngredientsUnit" name="IngredientsUnit" value="${customize_ingredients_unit}">`;
        }
        else if (customize_ingredients_number != "" && customize_ingredients_unit != "") {
            add_p4 = `<p class="align-middle">${customize_ingredients_number}</p>
                          <p class="align-middle">${customize_ingredients_unit}</p>
                          <input type="hidden" class="IngredientsNumber" name="IngredientsNumber" value="${customize_ingredients_number}">
                          <input type="hidden" class="IngredientsUnit" name="IngredientsUnit" value="${customize_ingredients_unit}">`;
        }


        if (add_p3 != "" && add_p4 != "") {
            $(`#ingredients_content_${row_num} .ingredients_name_row p`).detach();
            $(`#ingredients_content_${row_num} .ingredients_name_row input`).detach();
            $(`#ingredients_content_${row_num} .ingredients_name_row `).append(add_p3);
            $(`#ingredients_content_${row_num} .ingredients_num_unit p`).detach();
            $(`#ingredients_content_${row_num} .ingredients_num_unit input`).detach();
            $(`#ingredients_content_${row_num} .ingredients_num_unit `).append(add_p4);
        }
        console.log("p3:" + add_p3);
        console.log("p4:" + add_p4);

    })
    //食材輸入end

    // 食材標題切換start
    $(`#ingredients_content_${row_num} .ingredients_row_change`).on("change", function () {
        if ($(`#ingredients_content_${row_num} .ingredients_row_change`).val() == "標題") {
            $(`#ingredients_content_${row_num} .ingredients_content_anchor button`).remove();
            $(`#ingredients_content_${row_num} .ingredients_content_anchor h4`).remove();
            $(`#ingredients_content_${row_num} .ingredients_content_anchor`).append(`<h4 id="ingredients_title_${ingredients_title_last_num}" class="col-9 ingredients_title" style="margin-bottom: 0px;">
                    <input class="TitleName" type="text" name="TitleName" placeholder="新增標題">
                    </h4>`);
            ingredients_title_last_num += 1;
        }
        if ($(`#ingredients_content_${row_num} .ingredients_row_change`).val() == "食材") {
            $(`#ingredients_content_${row_num} .ingredients_content_anchor h4`).remove();
            $(`#ingredients_content_${row_num} .ingredients_content_anchor button`).remove();
            $(`#ingredients_content_${row_num} .ingredients_content_anchor`).append(`<button class="ingredients_content" type="button" data-bs-toggle="collapse"
                            data-bs-target="#card_${row_num}" aria-expanded="false" aria-controls="card_${row_num}"
                            style="width: 100%;">
                            <div class="row ">
                                <div class="col-5 ingredients_name_row ">
                                    <p class="align-middle ingredients_name">加入食材</p>
                                </div>
                                <div class="col-7 ingredients_num_unit d-flex align-items-center">
                                    <p class="align-middle">數量</p>
                                    <p class="align-middle">單位</p>
                                </div>
                            </div>
                        </button>`);
        }
    })
    // 食材標題切換end             
})
// #ingredients事件代理end

// 食材欄刪除start

$("#ingredients").on("click", ".ingredients_reduce", function () {
    if (confirm("確定要刪除嗎?")) {
        $(this).parent().parent().remove();
        add_ingredients_num();
    }
})

// 食材欄刪除end

// ===============================================================================================================================

$(document).mouseup(function (e) {
    var container = $(".collapse_anchor");
    // 如果點擊區域不在摺疊區域內就摺疊
    if (!container.is(e.target) && container.has(e.target).length === 0) {
        container.collapse('hide');
    }
});

// ===============================================================================================================================

// 增加食材列start

$("#add_ingredients").on("click", function () {
    $(".ingredients_row_decorate_last").before(`<div id="ingredients_content_${ingredients_content_last_num}" class="row ingredients_row_decorate ingredients_content_num ingredients_row">
                    <span class="d_none" data-row-num=""></span>
                    <div class="col-9 ingredients_content_center ingredients_content_anchor">
                        <button class="ingredients_content" type="button" data-bs-toggle="collapse"
                            data-bs-target="#card_${ingredients_content_last_num}" aria-expanded="false" aria-controls="card_${ingredients_content_last_num}" style="width: 100%;">
                            <div class="row ">
                            	
                                <div class="col-5 ingredients_name_row ">
                                    <p class="align-middle ingredients_name">加入食材</p>
                                </div>
                                <div class="col-7 ingredients_num_unit d-flex align-items-center">
                                    <p class="align-middle">數量</p>
                                    <p class="align-middle">單位</p>
                                </div>
                            </div>
                        </button>
                    </div>
                    <div class="col-2 text-end d-flex align-items-center justify-content-end">
                        <select class="ingredients_row_select ingredients_row_change" name="" id="">
                            <option value="食材">食材</option>
                            <option value="標題">標題</option>
                        </select>
                    </div>
                    <div class="col-1 d-flex align-items-center justify-content-center">
                        <button type="button" id="reduce_${ingredients_content_last_num}" class="plus_minus_btn ingredients_reduce"><img class="plus_minus_img" src="/img/recipe_icon/Minus.ico"
                                alt=""></button>
                    </div>
                    <div class="collapse_anchor collapse" id="card_${ingredients_content_last_num}">
                        <div class="card card-body card_style">
                            <div class="row">
                                <div class="col-5">
                                    <input id="ingredients_searchbar_${ingredients_content_last_num}" class="ingredients_searchbar ingredients_searchbar_anchor " type="text" placeholder="請輸入食材"
                                        list="ingredients_list">
                                    <div class="row">
                                        <div class="col-9" style="padding-right: 2.5px;">
                                            <input id="ingredients_number"
                                                class="ingredients_searchbar ingredients_number " type="text"
                                                placeholder="請輸入數量" list="ingredients_list">
                                        </div>
                                        <div class="col-3" style="padding-left: 2.5px;">
                                            <select class="ingredients_row_select ingredients_unit" 
                                                id="ingredients_unit">
                                                <option selected hidden disabled value="">單位</option>
                                                <option value="g">g</option>
                                                <option value="kg">kg</option>
                                                <option value="ml">ml</option>
                                                <option value="L">L</option>
                                                <option value="適量">適量</option>
                                            </select>
                                        </div>
                                        <div class="col-12 d-flex align-items-center justify-content-center">
                                            <button type="button" class="button_style submit_ingredients"
                                                style="width: 50%;">送出</button>
                                        </div>
                                    </div>
                                </div>
                                <ul id="ingredients_name_${ingredients_content_last_num}" class="ingredients_options col-7 ingredients_name">
                                    
                                </ul>
                                <hr>
                                <p class="illustrate d-flex align-items-center justify-content-center">
                                    找不到食材嗎? 請輸入需要的食材
                                </p>
                                <div
                                    class="col-12 d-flex align-items-center justify-content-center customize_ingredients_row">
                                    <div class="row">
                                        <div class="col-3" style="padding-left: 5px; padding-right: 5px;">
                                            <input id="customize_ingredients" class="ingredients_searchbar " type="text"
                                                placeholder="請輸入食材">
                                        </div>
                                        <div class="col-4" style="padding-left: 5px; padding-right: 5px;">
                                            <input id="customize_kcalg" class="ingredients_searchbar  col-5" type="text"
                                                placeholder="每100克卡洛里(非必要)">
                                        </div>
                                        <div class="col-5" style="padding-left: 5px; padding-right: 5px;">
                                            <div class="row">
                                                <div class="col-9">
                                                    <input id="customize_ingredients_number"
                                                        class="ingredients_searchbar " type="text" placeholder="請輸入數量"
                                                        list="ingredients_list">
                                                </div>
                                                <div class="col-3" style="padding: 0px;">
                                                    <select class="ingredients_row_select " 
                                                        id="customize_ingredients_unit">
                                                        <option selected hidden disabled value="">單位</option>
                                                        <option value="g">g</option>
                                                        <option value="kg">kg</option>
                                                        <option value="ml">ml</option>
                                                        <option value="L">L</option>
                                                        <option value="適量">適量</option>
                                                    </select>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-12 d-flex align-items-center justify-content-center">
                                    <button type="button" class="button_style submit_ingredients submit_customize_ingredients"
                                        style="width: 25%;">送出</button>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>`);
    ingredients_content_last_num = ingredients_content_last_num + 1;
    add_ingredients_num();
})

// 增加食材列end 

// 增加步驟列start

$("#add_step").on("click", function () {
    $(".recipe_steps_decorate_last").before(`<div id="step_row_${step_content_last_num}" class="row recipe_steps_decorate step_row_anchor">
                        <div class="col-5 position-relative img_cut_anchor">
                            <img id="steps_img_${step_content_last_num}" class="steps_img" src="/img/455X325.jpg" alt="">
                            <input id="steps_img_${step_content_last_num}_save" type="hidden" class="steps_img_save" name="step_table[].StepBase64" value="0">
                            <button type="button" class="opacity-50 img_upload_btn position-absolute top-50 start-50 translate-middle">
                                <svg viewBox="0 0 24 24" aria-hidden="true"
                                    class="r-4qtqp9 r-yyyyoo r-dnmrzs r-bnwqim r-1plcrui r-lrvibr r-18yzcnr r-yc9v9c"
                                    style="color: rgb(255, 255, 255);">
                                    <g>
                                        <path
                                            d="M9.697 3H11v2h-.697l-3 2H5c-.276 0-.5.224-.5.5v11c0 .276.224.5.5.5h14c.276 0 .5-.224.5-.5V10h2v8.5c0 1.381-1.119 2.5-2.5 2.5H5c-1.381 0-2.5-1.119-2.5-2.5v-11C2.5 6.119 3.619 5 5 5h1.697l3-2zM12 10.5c-1.105 0-2 .895-2 2s.895 2 2 2 2-.895 2-2-.895-2-2-2zm-4 2c0-2.209 1.791-4 4-4s4 1.791 4 4-1.791 4-4 4-4-1.791-4-4zM17 2c0 1.657-1.343 3-3 3v1c1.657 0 3 1.343 3 3h1c0-1.657 1.343-3 3-3V5c-1.657 0-3-1.343-3-3h-1z">
                                        </path>
                                    </g>
                                </svg>
                            </button>
                        </div>
                        <div class="col-7">
                            <h4></h4>
                            <textarea class="recipe_steps_enter " name="step_table[].StepText" id="steps_text_${step_content_last_num}_save" placeholder="請輸入步驟(150字以內)"></textarea>
                        </div>
                        <div class="col-12 d-flex align-items-center justify-content-end">
                            <button type="button" id="reduce_step_${step_content_last_num}" class="plus_minus_btn reduce_step"><img class="plus_minus_img"
                                    src="/img/recipe_icon/Minus.ico" alt=""></button>
                        </div>
                    </div>
                    `);

    step_content_last_num = step_content_last_num + 1;
    
    add_stap_num();
    
})

// 增加步驟列end 

// 刪除步驟列start 

$("#recipe_steps").on("click", ".reduce_step", function () {

    if (confirm("確定要刪除嗎?")) {
        $(this).parent().parent().remove();
        add_stap_num();
    }
})

// 刪除步驟列end 

// 取消儲存食譜start 

$("#cancel_recipe_btn").on("click", function () {

    if (confirm("確定不儲存嗎?")) {
        window.location.href = "#";
    }
})

// 取消儲存食譜end 

// 編輯後離開頁面警告start

var page_change = false;

$(".ingredients_page").on("change", function () {
    page_change = true;
})

$(window).on("beforeunload", e => {
    if (page_change) {
        let page_change_warn = "確定要離開頁面嗎?您所做的變更將不會被保留"

        return page_change_warn;
    }
})

// 編輯後離開頁面警告end

//圖片上傳start

$(function () {
    var myCrop;
    var img_cut;
    var img_save;

    //圖片儲存
    $("#save_recipe_cover").on("click", function () {
        myCrop.croppie("result", {
            type: "canvas",
            format: "jpeg", //type_img
            quality: 0.85   //compress_ratio
        }).then(function (src) {
            img_save = "";
            displayCropImg(src, $(`#${img_cut}`));
            img_save = $(`#${img_cut}`).attr("src").replace("data:image/jpeg;base64,", "");
            $(`#${img_cut}_save`).val(img_save);
            console.log($(`#${img_cut}_save`).val());
            console.log(img_save);
        });
        $("#img_upload").hide();
        $("#modalBackground").hide();
    });

    //圖片編輯
    $(".ingredients_page").on("click", ".img_upload_btn", function () {
        $("#recipe_cover").val("");
        $("#recipe_cover").trigger('click');
        img_cut = $(this).closest(".img_cut_anchor").find("img").attr("id");
    });

    //圖片輸入
    $("#recipe_cover").on("change", function () {
        $("#modalBackground").fadeIn("fast");
        $("#img_upload").show();
        readFile(this);
    });

    //取消圖片儲存
    $("#cancel_recipe_cover").on("click", function () {
        $("#img_upload").hide();
        $("#modalBackground").hide();
        $("#recipe_cover").val("");
    })

    //圖片裁切
    function readFile(input) {

        let width_crop = 450, // 圖片裁切寬度 px 值
            height_crop = 337.5, // 圖片裁切高度 px 值
            type_crop = "square", // 裁切形狀: square 為方形, circle 為圓形
            width_preview = 450, // 預覽區塊寬度 px 值
            height_preview = 500, // 預覽區塊高度 px 值
            compress_ratio = 0.85, // 圖片壓縮比例 0~1
            type_img = "jpeg", // 圖檔格式 jpeg png webp
            oldImg = new Image(),
            file, oldImgDataUrl;

        $('#edit_image').croppie('destroy')

        myCrop = $("#edit_image").croppie({
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
            return;
        }

        if (file.type.match("image.*")) {
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
    }

    //裁切完的圖片輸出
    function displayCropImg(src, img_obj) {
        img_obj.prop('src', src);

    }
})
//圖片上傳end
//取消公開或私人按start
$(".clear_select").on("click", function () {
    $("#public_private").val("");
})
//取消公開或私人按鈕end

//食材搜尋start

$("#ingredients").on("input", ".ingredients_searchbar_anchor", function () {
    let content_id = $(this).attr("id");
    let row_num = content_id.replace(/[^0-9]/ig, "");
    var query = $(this).val();
    $.ajax({
        url: "/RecipeCreatePage/Create_select",
        type: "GET",
        data: { ingredient_select: query },
        success: function (data) {
            $(`#ingredients_name_${row_num}`).html(data);
        }
    })
})

//食材搜尋end

//送出後關閉卡片start

$("#ingredients").on("click", ".submit_ingredients", function () {
    $(this).closest(".collapse_anchor").collapse('hide');
})
//送出後關閉卡片end

//計算卡洛里總和start

$(function () {
    calculate_kcalg();
    function calculate_kcalg() {
        let total = 0;
        $(".kcalg").each(function () {
            let kcalgval = parseFloat($(this).text());
            if (isNaN(kcalgval)) {
                total = "不顯示";
                return false;
            } else {
                total += kcalgval;
            }
        })

        if (total !== "不顯示") {
            $("#total_kcalg_save").detach();
            $("#total_kcalg").text(Math.round(total));
            $("#kcalg_description").text("卡");
            $(`#kcalg_description`).after(`<input type="hidden" id="total_kcalg_save" name="Calories" value="${Math.round(total)}">
            `);
            console.log($("#total_kcalg_save").val());
        } else {
            $("#total_kcalg").text("");
            $("#kcalg_description").text("有缺少卡洛里的食材");
        }
    }

    const callback = function (mutationsList, observer) {
        for (let mutation of mutationsList) {
            if (mutation.type === 'childList') {

                mutation.addedNodes.forEach(node => {
                    if (node.nodeType === 1 && $(node).hasClass('kcalg')) {

                        calculate_kcalg();
                    }
                })
                mutation.removedNodes.forEach(node => {
                    if (node.nodeType === 1 && $(node).hasClass('ingredients_content_num')) {
                        calculate_kcalg();
                    }
                })
            }
        }
    }

    const target = document.getElementById("ingredients");
    const config = { childList: true, subtree: true };
    const observer = new MutationObserver(callback);
    observer.observe(target, config);


})

//計算卡洛里總和end


//計算步驟欄位數量並重新編號start

function add_stap_num() {
    $(".step_row_anchor").each(function (num) {
        $(this).find("h4").empty();
        $(this).find("h4").append(num + 1);
        $(this).find(".steps_img_save").removeAttr("name");
        $(this).find(".steps_img_save").attr("name", `step_table[${num}].StepBase64`);
        $(this).find(".recipe_steps_enter").removeAttr("name");
        $(this).find(".recipe_steps_enter").attr("name", `step_table[${num}].StepText`);
    });
}

//計算步驟欄位數量並重新編號end

//計算食材欄位數量並重新編號start
function add_ingredients_num() {
    $(".ingredients_row").each(function (num) {
        $(this).find("span").removeAttr("data-row-num");
        $(this).find("span").attr("data-row-num", num);
    })
}

//計算食材欄位數量並重新編號end
//計算食材欄位數量並按順序給名start

function add_ingredients_name() {
    $(".ingredients_row").each(function (num) {
        $(this).find(".TitleName").removeAttr("name");
        $(this).find(".TitleName").attr("name", `ingredients_table[${num}].TitleName`);
        $(this).find(".IngredientsId").removeAttr("name");
        $(this).find(".IngredientsId").attr("name", `ingredients_table[${num}].IngredientsId`);
        $(this).find(".IngredientsName").removeAttr("name");
        $(this).find(".IngredientsName").attr("name", `ingredients_table[${num}].IngredientsName`);
        $(this).find(".IngredientsNumber").removeAttr("name");
        $(this).find(".IngredientsNumber").attr("name", `ingredients_table[${num}].IngredientsNumber`);
        $(this).find(".IngredientsUnit").removeAttr("name");
        $(this).find(".IngredientsUnit").attr("name", `ingredients_table[${num}].IngredientsUnit`);
        $(this).find(".IngredientsTableId").removeAttr("name");
        $(this).find(".IngredientsTableId").attr("name", `ingredients_table[${num}].IngredientsTableId`);
        $(this).find(".IngredientUserId").removeAttr("name");
        $(this).find(".IngredientUserId").attr("name", `ingredients_table[${num}].IngredientUserId`);
        $(this).find(".IngredientRecipeId").removeAttr("name");
        $(this).find(".IngredientRecipeId").attr("name", `ingredients_table[${num}].IngredientRecipeId`);
        $(this).find(".TitleId").removeAttr("name");
        $(this).find(".TitleId").attr("name", `ingredients_table[${num}].TitleId`);
        $(this).find(".Kcalg").removeAttr("name");
        $(this).find(".Kcalg").attr("name", `ingredients_table[${num}].IngredientKcalg`);
    })
}

//計算食材欄位數量並按順序給名end
//儲存食譜按鈕
$("#save_recipe_btn").on("click", function () {
    add_ingredients_name();
})



