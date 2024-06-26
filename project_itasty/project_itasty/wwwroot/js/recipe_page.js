//主留言列最後一列的ID號碼start
var message_content_num = [];
var message_content_last_num = "";
$(".chats_anchor").each(function () {
    let id = $(this).attr("id").replace(/[^0-9]/ig, "");
    message_content_num.push(id);
})
message_content_last_num = Math.max.apply(null, message_content_num) + 1;
//主留言最後一列的ID號碼end        
//回覆列最後一列的ID號碼start
var reply_content_num = [];
var reply_content_last_num = "";
$(".chat_reply_anchor").each(function () {
    let id = $(this).attr("id").replace(/[^0-9]/ig, "");
    reply_content_num.push(id);
})
reply_content_last_num = Math.max.apply(null, reply_content_num) + 1;
//回覆最後一列的ID號碼end        

//留言數計算start

var message_num = [];
var message_total = "";

function chats_num() {
    message_num = [];
    $(".chats_num").each(function () {
        let id = $(this).attr("id");
        message_num.push("id");
    })
    message_total = message_num.length;
}
//留言數計算end

//留言總數初始化start
chats_num();
$("#message_num").append(message_total);

//留言總數初始化end

//留言給作者start        

//$("#recipe_anchor").on("click", "#message_button", function () {
//    let message_enter = "";
//    message_enter = $("#message_enter").val();
//    if (message_enter != "") {
//        $("#chats_row").append(`            <hr>
//            <div id="chats_${message_content_last_num}" class="chats row chats_anchor chats_num">
//                <div class="col-2 d-flex justify-content-center deleted_userimg_anchor" style="margin-top:15px;">
//                    <img class="chats_user_photo " src="~/img/user/user03.png" alt="">
//                </div>
//                <div class="col-10 deleted_messages_anchor" style="margin-top:15px; padding-left: 0px;">
//                    <div class="row">
//                        <div class="col-11">
//                            <h5>短瀏海</h5>
//                            <p class="text-break message_content" style="margin-bottom:0px ;">${message_enter}</p>
//                            <div class="chats_time">
//                                <p>一週前</p>
//                            </div>
//                            <button class="reply_btn " href="">
//                                <img src="~/img/recipe_icon/chat_reply.ico" alt="">
//                                回覆
//                            </button>
//                        </div>
//                        <div class="col-1 d-flex align-items-start justify-content-end">
//                            <div class="popup more_btn">
//                                <img class="icon_size" src="~/img/recipe_icon/more.ico" alt="">
//                                <span class="popuptext row" id="message_edit_Popup">
//                                    <div class="col-12 message_edit">
//                                        <button class="edit_chats">編輯</button>
//                                        <hr>
//                                        <button class="delete_chats">刪除</button>
//                                        <hr>
//                                        <button class="report_chats" data-bs-toggle="modal"
//                                        data-bs-target="#report_chats">檢舉</button>
//                                    </div>
//                                </span>
//                            </div>
//                        </div>
//                    </div>
//                </div>
//                <!-- 留言回覆視窗start -->
//                <div class="col-12 reply_content reply_content_none">
//                    <div class="row">
//                        <div class="col-2"></div>
//                        <div class="col-10">
//                            <div class="row reply_row">
//                                <textarea class="col-12 reply_enter" name="" id="" placeholder="輸入留言"></textarea>
//                                <div class="col-11 d-flex justify-content-end reply_content_btn">
//                                    <button class="button_style reply_cancel_btn">取消</button>
//                                    <button class="button_style reply_enter_btn">送出留言</button>
//                                </div>
//                                <div class="col-1">
//                                </div>
//                            </div>
//                        </div>
//                    </div>
//                </div>
//                <!-- 留言回覆視窗end -->
//            </div>
//            `);
//        message_content_last_num = message_content_last_num + 1;
//        message_total = message_total + 1;

//        $("#message_enter").val("");

//    }
//})

//留言給作者end

// 留言編輯彈出視窗start

$("#recipe_anchor").on("click", ".more_btn", function () {
    $(this).find(".popuptext").toggleClass("show");
})

// 留言編輯彈出視窗end



// 回覆留言start

$("#recipe_anchor").on("click", ".reply_btn", function () {
    $(this).closest(".chats_anchor").find(".reply_content").toggleClass("reply_content_none");
})

// 回覆留言end

// 取消回覆start

$("#chats_row").on("click", ".reply_cancel_btn", function () {
    $(this).closest(".chats_anchor").find(".reply_content").toggleClass("reply_content_none");
})

// 取消回覆end

// 送出回覆start

//$("#chats_row").on("click", ".reply_enter_btn", function () {

//    let reply_enter = "";
//    reply_enter = $(this).closest(".reply_row").find(".reply_enter").val();
//    $(this).closest(".chats_anchor").append(`<div id="chat_reply_${reply_content_last_num}" class="col-12 chat_reply_anchor chats_num">
//                    <div class="row">
//                        <div class="col-2"></div>
//                        <div class="col-10">
//                            <div class="row">
//                                <div class="col-1 deleted_userimg_anchor" style="padding-left: 0px;">
//                                    <img class="chats_user_photo" src="~/img/user/user01.png" alt="">
//                                </div>
//                                <div class="col-11 deleted_messages_anchor" style="padding-left: 32px;">
//                                    <div class="row">
//                                        <div class="col-10">
//                                            <h5>會員</h5>
//                                            <p class="text-break message_content" style="margin-bottom:0px ;">${reply_enter}</p>
//                                            <div class="chats_time">
//                                                <p>兩週前</p>
//                                            </div>
//                                        </div>
//                                        <div class="col-2 d-flex align-items-start justify-content-end">
//                                            <div class="popup more_btn">
//                                                <img class="icon_size" src="~/img/recipe_icon/more.ico" alt="">
//                                                <span class="popuptext row" id="message_edit_Popup">
//                                                    <div class="col-12 message_edit">
//                                                        <button class="edit_chats">編輯</button>
//                                                        <hr>
//                                                        <button class="delete_chats">刪除</button>
//                                                        <hr>
//                                                        <button class="report_chats" data-bs-toggle="modal"
//                                                        data-bs-target="#report_chats">檢舉</button>
//                                                    </div>
//                                                </span>
//                                            </div>
//                                        </div>
//                                    </div>
//                                </div>
//                            </div>
//                        </div>
//                    </div>
//                </div>`
//    );

//    reply_content_last_num = reply_content_last_num + 1;
//    message_total = message_total + 1;

//    $(this).closest(".reply_row").find(".reply_enter").val("");
//    $("#message_num").empty();
//    $("#message_num").append(message_total);

//    $(this).closest(".chats_anchor").find(".reply_content").toggleClass("reply_content_none");

//})

// 送出回覆end

// 編輯留言start
var orig_message = "";
$("#chats_row").on("click", ".edit_chats", function () {
    orig_message = $(this).closest(".chats_num").find(".message_content").text();
    let chats_id_word = $(this).closest(".chats_num").attr("id").replace(/\d+/g, "");
    let message_id = $(this).closest(".chats_num").find(".message_id").val();
    console.log(message_id);

    if (chats_id_word == "chats_") {
        $(this).closest(".chats_num").find(".chats_time").remove();
        $(this).closest(".chats_num").find(".reply_btn").remove();
        $(this).closest(".chats_num").find(".message_content").replaceWith(`
                    <div class="row edit_row">
                        <form id="edit_message"  action="/RecipePage/Edit_message" method="post">
                        <input class="edit_message_id" type="hidden" name="message_id" value="${message_id}">
                        <input class="col-12 edit_message" name="message_content" type="text" value="${orig_message}">
                        <div class="col-12 d-flex justify-content-end edit_content_btn">
                            <button class="button_style edit_cancel_btn">取消</button>
                            <button type="submit" class="button_style edit_enter_btn">送出留言</button>
                        </div>
                        </form>
                    </div>`
        );
        $(this).closest(".chats_num").find(".edit_message_id").val($(this).closest(".chats_num").find(".message_id").val());
        //    console.log($(this).closest(".chats_num").find(".edit_message_id").val());
    }
    else if (chats_id_word == "chat_reply_") {
        let message_id = $(this).closest(".chats_num").find(".message_id").val();
        console.log(message_id);

        $(this).closest(".chats_num").find(".chats_time").remove();
        $(this).closest(".chats_num").find(".message_content").replaceWith(`
                    <div class="row edit_row">
                        <form id="edit_message"  action="/RecipePage/Edit_message" method="post">
                        <input class="edit_message_id" type="hidden" name="message_id" value="${message_id}">
                        <input class="col-12 edit_message" name="message_content" type="text" value="${orig_message}">
                        <div class="col-12 d-flex justify-content-end edit_content_btn">
                            <button class="button_style edit_cancel_btn">取消</button>
                            <button type="submit" class="button_style edit_enter_btn">送出留言</button>
                        </div>
                        </form>
                    </div>
                    `);

    }
})

// 編輯留言end

// 編輯留言完成start

//$("#chats_row").on("click", ".edit_enter_btn", function () {
//    let message_content = $(this).closest(".chats_num").find(".message_content").text();
//    let chats_id_word = $(this).closest(".chats_num").attr("id").replace(/\d+/g, "");

//    if (chats_id_word == "chats_") {
//        let edit_message = $(this).closest(".chats_num").find(".edit_message").val();
//        $(this).closest(".chats_num").find(".edit_row").replaceWith(`
//                            <p class="text-break message_content" style="margin-bottom:0px ;">${edit_message}</p>
//                            <div class="chats_time">
//                                <p>一週前</p>
//                            </div>
//                            <button class="reply_btn " href="">
//                                <img src="~/img/recipe_icon/chat_reply.ico" alt="">
//                                回覆
//                            </button>
//                            `)
//    } else if (chats_id_word == "chat_reply_") {
//        let edit_message = $(this).closest(".chats_num").find(".edit_message").val();
//        $(this).closest(".chats_num").find(".edit_row").replaceWith(`
//                            <p class="text-break message_content" style="margin-bottom:0px ;">${edit_message}</p>
//                            <div class="chats_time">
//                                <p>一週前</p>
//                            </div>
//                            `)
//    }
//})

// 編輯留言完成end

// 取消留言start

//$("#chats_row").on("click", ".edit_cancel_btn", function () {

//    let chats_id_word = $(this).closest(".chats_num").attr("id").replace(/\d+/g, "");

//    if (chats_id_word == "chats_") {
//        $(this).closest(".chats_num").find(".edit_row").replaceWith(`
//                            <p class="text-break message_content" style="margin-bottom:0px ;">${orig_message}</p>
//                            <div class="chats_time">
//                                <p>一週前</p>
//                            </div>
//                            <button class="reply_btn " href="">
//                                <img src="~/img/recipe_icon/chat_reply.ico" alt="">
//                                回覆
//                            </button>
//                            `)
//    } else if (chats_id_word == "chat_reply_") {
//        $(this).closest(".chats_num").find(".edit_row").replaceWith(`
//                            <p class="text-break message_content" style="margin-bottom:0px ;">${orig_message}</p>
//                            <div class="chats_time">
//                                <p>一週前</p>
//                            </div>
//                            `)
//    }


//})

// 取消留言end

// 檢舉視窗start

$(".message_report_other").on("change", function () {
    if ($(".message_report_other").val() == "其他原因") {
        $(".modal-body .row").append(`<textarea class="col-12" name="" id="" style="width: 70%;height: 100px;margin-top: 10px;" placeholder="請輸入50字內的說明"></textarea>`);
    } else {
        $(".modal-body").find("textarea").remove();
    }
})
$(".recipe_report_other").on("change", function () {
    if ($(".recipe_report_other").val() == "其他原因") {
        $(".modal-body .row").append(`<textarea class="col-12" name="" id="" style="width: 70%;height: 100px;margin-top: 10px;" placeholder="請輸入50字內的說明"></textarea>`);
    } else {
        $(".modal-body").find("textarea").remove();
    }
})

// 檢舉視窗end

// 重置檢舉視窗選項start

$(".report_select_reset").on("click", function () {
    $(this).closest(".modal-content").find("select").val("");
    $(".modal-body").find("textarea").remove();
})

// 重置檢舉視窗選項end

//留言給作者start
$(function () {
    $(".enter_message_row").on("submit", "#message_submit", function (e) {
        e.preventDefault();
        $.ajax({
            url: this.action,
            type: this.method,
            data: $(this).serialize(),
            success: function (data) {
                $("#chats_row").html(data);
                $("#message_enter").val("");
                chats_num();
                $("#message_num").empty();
                $("#message_num").append(message_total);
                reply_submit();
            }
        })
    })

    //留言給作者end

    //回覆留言start
    reply_submit()
    //回覆留言end
})



//回覆留言function
function reply_submit() {
    $(".reply_submit").each(function () {

        //將當前所選form的ID取出來
        const formid = $(this).attr('id');
        console.log(formid);
        $(`#chats_row`).off("submit", `#${formid}`);
        $(`#chats_row`).on("submit", `#${formid}`, function (e) {
            e.preventDefault();
            $.ajax({
                url: this.action,
                type: this.method,
                data: $(this).serialize(),
                success: function (data) {
                    $(`#chats_row`).html(data);
                    $(this).closest(".reply_row").find(".reply_enter").val("");
                    chats_num();
                    $("#message_num").empty();
                    $("#message_num").append(message_total);
                    $(this).closest(".chats_anchor").find(".reply_content").toggleClass("reply_content_none");
                }
            })
        })

    })

}

//刪除留言AJAX
$("#chats_row").on("submit", ".message_delete", function (e) {
    console.log("第一步")
    e.preventDefault();
    var form = $(this);
    let confirm_delete;
    confirm_delete = confirm("確定要刪除嗎?");
    console.log(confirm_delete);
    if (confirm_delete == true) {
        var formData = form.serialize();
        $.ajax({
            url: form.attr('action'),
            type: form.attr('method'),
            data: formData,
            success: function (data) {
                $("#chats_row").html(data);
                chats_num();
                $("#message_num").empty();
                $("#message_num").append(message_total);
            }
        })

    }
})

//編輯留言AJAX
$("#chats_row").on("submit", "#edit_message", function (e) {
    console.log("編輯");
    e.preventDefault();
    var form = $(this);
    var formData = form.serialize();
    console.log(formData);
    $.ajax({
        url: form.attr('action'),
        type: form.attr('method'),
        data: formData,
        success: function (data) {
            $("#chats_row").html(data);
        }
    })

})

//追蹤作者AJAX
$(function () {
    $("#author_row").on("click", "#follow_btn", function (e) {

        var author = $("#author_id").val();
        var follower = $("#follower_id").val();

        console.log(author);
        console.log(follower);
        $.ajax({
            url: '/api/UserApi', ///UpdateFollower
            type: 'POST',
            data: { user_id: author, follower_id: follower },
            success: function (data) {
                if (data == null) {
                    $("#follow_btn").text("取消追蹤").removeClass("follow_button").addClass("unfollow_button");
                } else {
                    $("#follow_btn").text("我要追蹤").removeClass("unfollow_button").addClass("follow_button");
                }
               /* $("#follow_row").html(data);*/
            }

        })

    })
})










