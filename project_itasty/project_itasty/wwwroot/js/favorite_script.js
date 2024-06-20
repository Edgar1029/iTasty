//是否登入
//採買清單內的食譜數量、每個食譜的詳細資訊
//收藏的食譜內的食譜數量、每個食譜的詳細資訊
//編輯後的食譜的食譜數量、每個食譜的詳細資訊
//總共有幾個客製化收藏夾、名字分別是甚麼
//每個客製化資料夾的數量和每個的詳細資訊、每個食譜的詳細資訊
//客製化資料夾每個的詳細資訊:每個資料夾客製化的id
//每個食譜的詳細資訊:食譜圖片、食譜名字、食譜按讚數、食譜瀏覽數、食譜創建日期
$(document).ready(async function () {
    var userNum = sessionStorage.getItem("userId");
    //var userNum = 3;
    var checklogin = 0;
    var customFolderName = [];
    //$(".loginsignbtn").empty();
    //if(checklogin==0){
    //    $(".loginsignbtn").append(`<div class="sign" style="margin-right:20px;">LogOut</div>`)
    //}else{
    //    $(".loginsignbtn").append(`<div class="login">Login</div>
    //                                <div class="sign">Sign up</div>`)
    //}
    // 新增一個客製化資料夾------------------------------------------------------------------------------------------
    var idcount = 0;
    var selectedRadioId;
    $('#label-confirm-button').on("click", function () {
        for(var i=0;i<1;i++){
            idcount++;
            var clonedelement = $("#draggable").clone();
            clonedelement.attr("id", `draggable-${idcount}`);
            clonedelement.find("input").attr("id", `radio-customize-${idcount}`);
            clonedelement.find("label").attr("for",`radio-customize-${idcount}`);
            clonedelement.find("label").find(".container-left-buy").text(`${$("#new-file-name").val()}`);
            console.log(customFolderName);
            if (customFolderName.includes($("#new-file-name").val())) {
                alert("命名已重複!");
                break;
            } else {
                var word = `新增"${$("#new-file-name").val()}"資料夾`;
                showToast(word);
                addnewcustomfolder(userNum, $("#new-file-name").val());
                customFolderName.push($("#new-file-name").val());
                clonedelement.find("label").find(".container-left-buy").prepend(`<i class="fa-regular fa-pen-to-square" style="margin-left: 7%;margin-right: 5px;"></i>`);
                $(".add-label").css("display", "none");
                clonedelement.droppable({
                    drop: function (event, ui) {
                        $(ui.draggable).css("opacity", "1");
                        var isnotreceipebook = $(ui.draggable).attr("class") == "hotreceipebookmark col-xl-2 gx-0 ui-sortable-helper";
                        if (isnotreceipebook == true) {
                            console.log(`選取到的物件為bookmark`);
                            $(this).find("p").removeClass("turn-green");
                            const radioButtons = document.querySelectorAll('input[name="radio-bookmark"]');
                            const radioGroup = Array.from(radioButtons);
                            console.log(radioGroup);
                            console.log(`選取到的資料夾id為${radioGroup[radiocount].id}`);
                            var dropthefolder = 0;
                            for (var i = 0; i < array2D.length; i++) {
                                console.log(`${$(this).find("input").prop("id")} == ${array2D[i][0]} i=${i}`);
                                if ($(this).find("input").prop("id") == array2D[i][0]) {
                                    dropthefolder = i;
                                    console.log(`dropthefolder=${dropthefolder}`);
                                    break;
                                }
                            }
                            console.log($(this).find("p")[0].innerText);
                            console.log(`我要把第${radiocount + 1}個收藏夾裡(食譜id為${$(ui.draggable).prop("id")})放進${$(this).find("p")[0].innerText}資料夾`);
                            addrecipe2custom(userNum, $(this).find("p")[0].innerText, $(ui.draggable).prop("id"));
                            array2D[dropthefolder].push($(ui.draggable).prop("id"));
                            // console.log($(this));
                            let selectedRadio = document.querySelector('input[name="radio-bookmark"]:checked');
                            let label = document.querySelector(`label[for="${selectedRadio.id}"]`);
                            var templabel = label.innerText.trimStart();
                            var word = `已從"${templabel}"\n新增"${$(ui.draggable).find(".hotreceipe-introduction").text()}"\n進"${$(this).find("p")[0].innerText}"`;
                            showToast(word);
                        }
                    }
                    , over: function (event, ui) {
                        var isnotreceipebook = $(ui.draggable).attr("class") == "hotreceipebookmark col-xl-2 gx-0 ui-sortable-helper";
                        if (isnotreceipebook == true) {
                            $(this).find("p").addClass("turn-green");
                            $(ui.draggable).css("opacity","0.1");
                        }
                    }
                    , out: function (event, ui) {
                        var isnotreceipebook = $(ui.draggable).attr("class") == "hotreceipebookmark col-xl-2 gx-0 ui-sortable-helper";
                        if (isnotreceipebook == true) {
                            $(this).find("p").removeClass("turn-green");
                            $(ui.draggable).css("opacity", "1");
                        }
                    }
                }
                );
                $("#customize-sample").append(clonedelement);
                console.log($(".container-left").css("height"));
                var height = parseInt($(".container-left").css("height")) + 40;
                $(".container-left").css("height", height);
                array2D.push([clonedelement.find("input").prop("id")]);
            }
            
        }
    });
    // 新增一個客製化收藏夾彈出的取名頁面(顯示)------------------------------------------------------------------------------------------
    $("#add-more-bookmark").on("click",function(){
        $(".add-label").css("display","block");
    })
    // 新增一個客製化收藏夾彈出的取名頁面(消失)------------------------------------------------------------------------------------------
    $("#label-deny-button").on("click",function(){
        $(".add-label").css("display","none");
    })
    // 客製化收藏夾的排版移動------------------------------------------------------------------------------------------
    $( function() {
        $( "#customize-sample" ).sortable();
      } );
    // 客製化收藏夾內的排版移動------------------------------------------------------------------------------------------
    $( function() {
        $( "#receipe-collection" ).sortable();
      } );
    // 客製化收藏夾放進垃圾桶的操作------------------------------------------------------------------------------------------
    $( "#trash-bin" ).droppable({
        drop: function (event, ui) {
            var isnotreceipebook = $(ui.draggable).attr("class") == "hotreceipebookmark col-xl-2 gx-0 ui-sortable-helper";
            if (isnotreceipebook == false) {
                deletenewcustomfolder(userNum, $(ui.draggable).find("p")[0].innerText);
                customFolderName.pop($(ui.draggable).find("p").text());
                $(ui.draggable).remove();
                array2D.splice(radiocount, 1);
                var height = parseInt($(".container-left").css("height")) - 40;
                $(".container-left").css("height", height);
                var word = `刪除"${$(ui.draggable).find("p").text()}"資料夾`;
                showToast(word);
                console.log(customFolderName);
            }
        }
        ,over:function( event, ui ) {
            console.log(ui.draggable);
            $(ui.draggable).find("p").addClass("turn-red");
        }
        ,out:function( event, ui ) {
            console.log(ui.draggable);
            $(ui.draggable).find("p").removeClass("turn-red");
        }
        }
    );
    $( "#radio-edit-id" ).droppable({
        drop: function (event, ui) {
            var isnotreceipebook = $(ui.draggable).attr("class") == "row gx-0 draggable-something ui-droppable";
            if (isnotreceipebook != true) {
                //alert("導向到編輯食譜頁面");
                window.alert("導向到編輯食譜頁面");
                $(this).find("p").removeClass("turn-yellow");
                $(ui.draggable).css("opacity", "1");
            }
        }
        ,over:function( event, ui ) {
            console.log(ui.draggable);
            $(this).find("p").addClass("turn-yellow");
            $(ui.draggable).css("opacity", "0.1");
        }
        ,out:function( event, ui ) {
            console.log(ui.draggable);
            $(this).find("p").removeClass("turn-yellow");
            $(ui.draggable).css("opacity", "1");
        }
        }
    );
    //$( "#radio-buy-id" ).droppable({
    //    drop: function( event, ui ) {
    //        $(this).find("p").removeClass("turn-yellow");
    //    }
    //    ,over:function( event, ui ) {
    //        console.log(ui.draggable);
    //        $(this).find("p").addClass("turn-yellow");
    //    }
    //    ,out:function( event, ui ) {
    //        console.log(ui.draggable);
    //        $(this).find("p").removeClass("turn-yellow");
    //    }
    //    }
    //);
    // 頁面說明欄的部分---------------------------------------------------------------------------------------------------------------------------
    var selectedRadioId = "radio-favorite";
    var morelabel = 0;
    $("#more-label").on("click",function(){
        console.log(selectedRadioId);
        if(morelabel==0){
            if(selectedRadioId == "radio-buy")
                $(".container-header").after(`<div class="more-information-label"><div class="label-title">採買清單</div><div class="label-information">這個頁面專門用來管理你在食譜中需要購買的所有食材；<br>你可以根據不同收藏夾的食譜生成採買清單，並隨時更新或刪除不需要的食譜；<br>這樣可以方便你在購物時確保沒有遺漏任何必要的食材。</div></div>`);
            else if(selectedRadioId == "radio-favorite")
                $(".container-header").after(`<div class="more-information-label"><div class="label-title">收藏的食譜</div><div class="label-information">這個頁面是您所有喜愛食譜的集中地。您可以將食譜拖曳至「採買清單」,生成採買明細；<br>也可以拖曳至下方的「客製化食譜收藏夾」進行分類；<br>或者拖曳至「編輯後的食譜」以微調食材，創造新的食譜。</div></div>`);
            else if(selectedRadioId == "radio-edit")
                $(".container-header").after(`<div class="more-information-label"><div class="label-title">編輯後的食譜</div><div class="label-information">這個頁面用來管理你對現有食譜進行的所有修改，並且創造新的食譜；<br>你可以在這裡保存和查看所有編輯過的食譜。</div></div>`);
            else
                $(".container-header").after(`<div class="more-information-label"><div class="label-title">客製化食譜資料夾</div><div class="label-information">這個頁面專門用來管理你自製的食譜收藏夾；<br>你可以根據不同的分類和需求創建多個收藏夾；<br>將食譜按主題或或各種個性化分類進行整理和管理，讓你能夠更快捷地找到所需的食譜。</div></div>`);
            morelabel=1;
        }else{
            $(document).find(".more-information-label").remove();
            console.log("%");
            morelabel=0;
        }
    })
    // 左邊選取到的資料夾是哪一個(採買清單、收藏食譜...)------------------------------------------------------------------------------------------

    $(document).on('change', 'input[type="radio"][name="radio-bookmark"]', function () {
        
        var selectedRadioName = $('input[name="radio-bookmark"]:checked').next().find("p").text();
        selectedRadioId = $('input[name="radio-bookmark"]:checked').prop("id");
        $(document).find(".more-information-label").remove();
        morelabel=0;
        $("#select-menu").text(selectedRadioName);
    });
    // 根據radio選擇的資料夾顯示在右邊欄位內-------------------------------------------------------------------------------------------------------------------------------
    let array2D = new Array(3).fill(null).map(() => []);
    array2D = [["radio-favorite"],["radio-buy"],["radio-edit"]];
    var radiocount = 0;
    $(document).on('change', 'input[type="radio"][name="radio-bookmark"]', function () {
        for(var i=0;i<array2D.length;i++){
            for(var j=0;j<array2D[i].length;j++){
                if(array2D[i][j]=="null"){
                    array2D[i].splice(j,1);
                }
            }    
        }
        $(".hot_receipe_space").empty();
        const radios = document.querySelectorAll('input[type="radio"][name="radio-bookmark"]');
        radios.forEach((radio, index) => {
                if (radio.checked) {
                    radiocount = index;
                }
        });
        const radioButtons = document.querySelectorAll('input[name="radio-bookmark"]');
        const radioGroup = Array.from(radioButtons);
        console.log(radioGroup);
        console.log(`選取到的id為${radioGroup[radiocount].id}`);

        for(var i=0;i<radioGroup.length;i++){
            console.log(`${radioGroup[radiocount].id} == ${array2D[i][0]}`);
            if(radioGroup[radiocount].id==array2D[i][0]){
                radiocount=i;
                break;
            }
        }
        if (radiocount == 1) {
            getShopRecipe(userNum);
        } else {
            for (var i = 1; i < array2D[radiocount].length; i++) {
                var x = array2D[radiocount][i];
                console.log(`第${radiocount + 1}個收藏夾的第${i}個元素id為${array2D[radiocount][i]}的食譜`);
                fetch(`/api/UserFolder/recipe/${userNum}/${x}`)
                    .then(response => {
                        if (!response.ok) {
                            throw new Error('Network response was not ok');
                        }
                        return response.json();
                    })
                    .then(recipe => {
                        //------------------------------------------------------------------------------------------------------------------
                        //console.log(`recipe.RecipeCoverImage=${recipe.recipeCoverImage}`);
                        //<div class="hotreceipe-img" style="background-image: url('data:image/png;base64,${recipe.recipeCoverImage}');"></div>
                        $("#receipe-collection").append(`
                                        <!-- ----------------------------------------------------------- -->
                                        <div class="hotreceipebookmark col-xl-2 gx-0" id="${recipe.recipeId}">
                                        <i class="show-list fa-solid fa-list-check"  style="color:#000000;position:absolute; top:235px; right:5px;"></i>
                                            <div class="hotreceipe-img" style="background-image: url('data:image/png;base64,${recipe.recipeCoverImage}');"></div>
                                            <div class="hotreceipe-information row gx-0">
                                                <ul class="hot-receipe-data row gx-0">
                                                    <li class="col-3 gx-0"><i class="fa-regular fa-eye"></i>${recipe.views}</li>
                                                    <li class="col-3 gx-0"><i class="fa-regular fa-thumbs-up"></i>${recipe.favorites}</li>
                                                </ul>
                                            </div>
                                            <div class="hotreceipe-introduction">${recipe.recipeName}</div>
                                        </div>
                                        <!-- ----------------------------------------------------------- -->
                        `);
                        //$(".hotreceipebookmark").prepend(`<i class="show-list fa-solid fa-list-check"  style="color:#000000;position:absolute; top:235px; right:5px;"></i>`);
                        adjustContainerHeight();
                        //------------------------------------------------------------------------------------------------------------------
                    })
                    .catch(error => {
                        console.error('There was a problem with the fetch operation:', error);
                    });
                //});
                //------------------------------------------------------------------------------------------------------------------
            }
        }
        // 從example-receipe.json載入各食譜的相關資訊並匯入到各個recipebookmark-------------------------------------------------------------------------------------------------------------------------------
        
        console.log(`點擊到第${radiocount + 1}個收藏夾，收藏夾裡有的食譜id為${array2D[radiocount]}`);
        console.log(`第${radiocount+1}收藏夾有${array2D[radiocount].length-1}個食譜`);
    });
    // 客製化收藏夾內把食譜刪除icon(顯示、消失)------------------------------------------------------------------------------------------
    var checkediticon = 0;
    $("#edit-file").on("click",function(){
        $("#edit-file").toggleClass("turn-red");
        if(checkediticon==0){
            $(".hotreceipebookmark").each(function(index,value){
                $(this).prepend(`<i class="fa-regular fa-trash-can edit-icon" id="edit-iconid${index}"></i>`);
            })
            $(".hotshopreceipebookmark").each(function (index, value) {
                $(this).prepend(`<i class="fa-regular fa-trash-can edit-icon" id="edit-iconid${index}"></i>`);
            })
            checkediticon=1;
        }
        else{
            $(".edit-icon").each(function(){
                $(this).remove();
            })
            checkediticon=0;
        }
    })
    // 客製化收藏夾內把食譜刪除的操作------------------------------------------------------------------------------------------
    $(document).on('click', '.edit-icon', async function() {
        console.log(`移除掉第${radiocount+1}個收藏夾的id為${$(this).closest('.hotreceipebookmark').prop("id")}個食譜`);
        /*array2D[radiocount][$(this).closest('.hotreceipebookmark').prop("id")]="null";*/
        let selectedRadio = document.querySelector('input[name="radio-bookmark"]:checked');
        let label = document.querySelector(`label[for="${selectedRadio.id}"]`);
        //console.log(label.innerText);
        //console.log(`----------------------------------${userNum}${label.innerText}${$(this).closest('.hotreceipebookmark').prop("id")}----------------------------------`);
        const elementToRemove = $(this).closest('.hotreceipebookmark').prop("id");
        const shopelementToRemove = $(this).closest('.hotshopreceipebookmark').prop("id");
        if (radiocount == 1) {
            //console.log(`${$(this).closest(".folderName").find(".folderName-title").text()}==============${elementToRemove}`);
            await deleteshoprecipefolder(userNum, $(this).closest(".folderName").find(".folderName-title").text(), shopelementToRemove);
            $(this).closest('.hotshopreceipebookmark').remove();
        } else {
            for (let j = 0; j < array2D[radiocount].length; j++) {
                if (array2D[radiocount][j] == elementToRemove) {
                    array2D[radiocount].splice(j, 1);
                    break;
                }
            }
            deleterecipe(userNum, label.innerText, elementToRemove);
            console.log(array2D);
            console.log(array2D[radiocount]);
            //for(var i=0;i<array2D[radiocount].length;i++){
            //    console.log(`第${radiocount+1}個收藏夾的第${i}個元素id為${array2D[radiocount][i]}的食譜`);
            //}
            $(this).closest('.hotreceipebookmark').remove();
            adjustContainerHeight();
        }
    });
    // 根據該食譜多寡來調整container-right的height(***超重要***)------------------------------------------------------------------------------------------
    function adjustContainerHeight() {
        const rightContainerMenu = document.querySelector('.right-container-menu');
        const containerRight = document.querySelector('.container-right');
        containerRight.style.height = '700px';
        const menuHeight = rightContainerMenu.clientHeight;
        //console.log(`menuHeight=${menuHeight}px`);
        // console.log(menuHeight);
        if (menuHeight > 700) {
            containerRight.style.height = `${menuHeight}px`;
            console.log(`containerRight=${menuHeight}px`);
        }else{
            containerRight.style.height = '700px';
            //console.log(`containerRight=${containerRight}px`);
        }
    }
    adjustContainerHeight();
    //----------------------------------------------------------------------------------------------------------------------------------------------------------
    $(document).on("click", ".hotreceipe-img, .show-list", function () {
        if (radiocount == 1) {
            var $bookmark = $(this).closest(".hotshopreceipebookmark");
            var $ingredients = $bookmark.find('.ingredient');
            if ($ingredients.length > 0) {
                $ingredients.remove();
                var receipeheight = parseInt($bookmark.css("height")) - $ingredients.length * 40;
                $bookmark.css("height", receipeheight + "px");
            } else {
                var receipeheight = parseInt($bookmark.css("height"));
                console.log(`hotbookmark=${$bookmark.prop("id")}`);
                var category = $(this).closest(".folderName").find(".folderName-title").text();
                fetch(`/api/UserFolder/shopingredient/${userNum}/${category}/${$bookmark.prop("id")}`)
                    .then(response => {
                        if (!response.ok) {
                            throw new Error('Network response was not ok');
                        }
                        return response.json();
                    })
                    .then(ingredient => {
                        console.log('ingredient details:', ingredient);
                        for (var temp = 0; temp < ingredient.length; temp++) {
                            console.log('ingredient details:', ingredient[temp]);
                            console.log(ingredient[temp].checkbox);
                            if (ingredient[temp].checkbox) {
                                $bookmark.append(`<div class="ingredient" id="${ingredient[temp].recipeName}"><input type="checkbox" id="ingredientCheckbox" name="" class="ingredient-prepare" checked><span class="ingredient-name">${ingredient[temp].shoppingIngredientsName}</span><span class="ingredient-count">${ingredient[temp].shoppingIngredientsNumber}</span><span class="ingredient-unit">${ingredient[temp].shoppingIngredientsUnit}</span></div>`);
                            }
                            else {
                                $bookmark.append(`<div class="ingredient" id="${ingredient[temp].recipeName}"><input type="checkbox" id="ingredientCheckbox" name="" class="ingredient-prepare"><span class="ingredient-name">${ingredient[temp].shoppingIngredientsName}</span><span class="ingredient-count">${ingredient[temp].shoppingIngredientsNumber}</span><span class="ingredient-unit">${ingredient[temp].shoppingIngredientsUnit}</span></div>`);
                            }
                            adjustContainerHeight();
                            receipeheight += 40;
                            $bookmark.css("height", receipeheight + "px");
                        }
                    })
                    .catch(error => {
                        console.error('There was a problem with the fetch operation:', error);
                    });
            }
            $bookmark.on('transitionend', function () {
                adjustContainerHeight();
                $bookmark.off('transitionend');
            });
        } else {
            var $bookmark = $(this).closest(".hotreceipebookmark");
            var $ingredients = $bookmark.find('.ingredient');
            if ($ingredients.length > 0) {
                $ingredients.remove();
                var receipeheight = parseInt($bookmark.css("height")) - $ingredients.length * 40;
                $bookmark.css("height", receipeheight + "px");
            } else {
                var receipeheight = parseInt($bookmark.css("height"));
                console.log(`hotbookmark=${$bookmark.prop("id")}`);

                fetch(`/api/UserFolder/ingredient/${userNum}/${$bookmark.prop("id")}`)
                    .then(response => {
                        if (!response.ok) {
                            throw new Error('Network response was not ok');
                        }
                        return response.json();
                    })
                    .then(ingredient => {
                        console.log('ingredient details:', ingredient);
                        for (var temp = 0; temp < ingredient.length; temp++) {
                            console.log('ingredient details:', ingredient[temp]);
                            console.log(ingredient[temp].checkbox);
                            if (ingredient[temp].checkbox) {
                                $bookmark.append(`<div class="ingredient" id="${ingredient[temp].ingredientsName}"><input type="checkbox" id="ingredientCheckbox" name="" class="ingredient-prepare" checked><span class="ingredient-name">${ingredient[temp].ingredientsName}</span><span class="ingredient-count">${ingredient[temp].ingredientsNumber}</span><span class="ingredient-unit">${ingredient[temp].ingredientsUnit}</span></div>`);
                            }
                            else {
                                $bookmark.append(`<div class="ingredient" id="${ingredient[temp].ingredientsName}"><input type="checkbox" id="ingredientCheckbox" name="" class="ingredient-prepare"><span class="ingredient-name">${ingredient[temp].ingredientsName}</span><span class="ingredient-count">${ingredient[temp].ingredientsNumber}</span><span class="ingredient-unit">${ingredient[temp].ingredientsUnit}</span></div>`);
                            }
                            adjustContainerHeight();
                            receipeheight += 40;
                            $bookmark.css("height", receipeheight + "px");
                        }
                    })
                    .catch(error => {
                        console.error('There was a problem with the fetch operation:', error);
                    });
            }
            $bookmark.on('transitionend', function () {
                adjustContainerHeight();
                $bookmark.off('transitionend');
            });
        }
        //}
    });
    // ------------------------------------------------------------
    $(document).on("change", ".ingredient", function () {
        var checkbox = $(this).find("#ingredientCheckbox");
        //console.log(checkbox);
        console.log("User Id status:", userNum);
        console.log("Recipe Id status:", $(this).closest(".hotreceipebookmark").prop("id"));
        console.log("Checkbox Id status:", $(this).prop("id"));
        console.log("Checkbox checked status:", $(this).find("#ingredientCheckbox").prop("checked"));
        changecheckbox(userNum, $(this).closest(".hotreceipebookmark").prop("id"), $(this).prop("id"), $(this).find("#ingredientCheckbox").prop("checked"));
    });
    // ------------------------------------------------------------
    $(document).on("change", ".folderIngredient", async function () {
        var category = $(this).closest(".folderName").find(".folderName-title").text();
        var ingredientName = $(this).find(".folderIngredientName").text();
        ingredientName = ` ${ingredientName}`;
        var ingredientcheck = $(this).find(".folderPrepare").prop("checked");
        console.log(`Check: ${ingredientcheck}, Category: ${category}, Ingredient: ${ingredientName}`);


        //if (ingredientcheck == true) {
        //    $(this).append(`<div class="ingredienttime" style="position:absolute; font-size:10px; color:green; right:10px; bottom:-15px;">${ingredientdetail.ingredientTime.slice(-8)}</div>`);
        //} else {
        //    $(this).remove(".ingredienttime");
        //}



        await changeshopbox(userNum, category, ingredientName, ingredientcheck);
        //await getShopRecipe(userNum);
        var check = $(this).closest(".folderName").find(".switch").find("input").prop("checked");
        console.log(`.switch-space is ${check}`);
        greenrecipe.length = 0;
        console.log(greenrecipe);
        if (check == true) {
            $(this).closest(".folderName").find(".hotshopreceipebookmark").remove();
            await GreenRecipe(userNum, category);
            await getShopFolderRecipe(userNum, category);
        } else {
            $(this).closest(".folderName").find(".hotshopreceipebookmark").remove();
        } 
    });
    // ------------------------------------------------------------
    var displayleft=0;
    $(".nav-left").on("click",function(){
        $(this).toggleClass("change");
        if(displayleft==0){
            $(".left-title").css("display","block");
            displayleft=1;
        }
        else{
            $(".left-title").css("display","none");
            displayleft=0;
        }
    })
    var displayright=0;
    $(".nav-right").on("click",function(){
        $(".nav-profile-icon").remove();
        if(displayright==0){
            $(".right-title").css("display","block");
            $(".nav-right").prepend(`<i class="fa-solid fa-user fa-2x nav-profile-icon"></i>`);
            displayright=1;
        }
        else{
            $(".right-title").css("display","none");
            $(".nav-right").prepend(`<i class="fa-regular fa-user fa-2x nav-profile-icon"></i>`);
            displayright=0;
        }
    })
    //依據userId匯入相關資料----------------------------------------------------------------------------------------------------------------------------------------------------------
    await getUserFolder(userNum);
    //function getUserFolder(userId) {
    //    fetch(`/api/UserFolder/${userId}`)
    //        .then(response => response.json())
    //        .then(data => {
    //            console.log(data);
    //            console.log(array2D);
    //            for (var i = 0; i < data.favoriteRecipe.length; i++) {
    //                array2D[0].push(data.favoriteRecipe[i]);
    //                //console.log(`array[0].push=${data.favoriteRecipe[i]}`);
    //            }
    //            for (var i = 0; i < data.shopRecipe.length; i++) {
    //                array2D[1].push(data.shopRecipe[i]);
    //            }
    //            for (var i = 0; i < data.editRecipe.length; i++) {
    //                array2D[2].push(data.editRecipe[i]);
    //            }
    //            for (var i = 0; i < data.customRecipeName.length; i++) {
    //                array2D.push([data.customRecipeName[i]]);
    //                for (var j = 0; j < data.customRecipe[i].length; j++) {
    //                    array2D[i+3].push(data.customRecipe[i][j]);
    //                }
    //            }
    //            addcustomizeFetch();
    //        })
    //        .catch(error => console.error('Error fetching user folder data:', error));

    //    console.log(array2D);
    //}
    async function getUserFolder(userId) {
        try {
            const response = await fetch(`/api/UserFolder/${userId}`);
            if (!response.ok) {
                throw new Error('Network response was not ok');
            }
            const data = await response.json();
            console.log(data);
            console.log(array2D);

            for (let i = 0; i < data.favoriteRecipe.length; i++) {
                array2D[0].push(data.favoriteRecipe[i]);
            }
            for (let i = 0; i < data.shopRecipe.length; i++) {
                array2D[1].push(data.shopRecipe[i]);
            }
            for (let i = 0; i < data.editRecipe.length; i++) {
                array2D[2].push(data.editRecipe[i]);
            }
            for (let i = 0; i < data.customRecipeName.length; i++) {
                array2D.push([data.customRecipeName[i]]);
                for (let j = 0; j < data.customRecipe[i].length; j++) {
                    array2D[i + 3].push(data.customRecipe[i][j]);
                }
            }
            addcustomizeFetch();
        } catch (error) {
            console.error('Error fetching user folder data:', error);
        }

        console.log(array2D);
    }

    //依據recipeId匯入view資料----------------------------------------------------------------------------------------------------------------------------------------------------------
    //getrecipeIdView(recipeId);
    var recipeView;
    function getRecipeIdView(recipeId) {
        fetch(`/api/UserFolder/recipeView/${recipeId}`)
            .then(response => response.json())
            .then(data => {
                recipeView = data;
            })
            .catch(error => console.error('Error fetching user folder data:', error));

        console.log(array2D);
    }
    //依據recipeId匯入相關資料----------------------------------------------------------------------------------------------------------------------------------------------------------
    //getrecipebyid();
    //var view;
    //var favorites;
    //var recipeName;
    //var recipeimg;
    //function getrecipebyid(recipeId) {
    //    fetch(`/api/UserFolder/recipe/${recipeId}`)
    //        .then(response => {
    //            if (!response.ok) {
    //                throw new Error('Network response was not ok');
    //            }
    //            return response.json();
    //        })
    //        .then(recipe => {
    //            console.log('Recipe details:', recipe);
    //            view = recipe.views;
    //            favorites = recipe.favorites;
    //            recipeName = recipe.recipeName;
    //            recipeimg = recipe.recipeCoverImage;
    //        })
    //        .catch(error => {
    //            console.error('There was a problem with the fetch operation:', error);
    //        });
    //}
    // 依據userId匯入相關資料並且把製作的食譜收藏夾數據顯示愛畫面上-------------------------------------------------------------------------------------------------------------------------------
    function addcustomizeFetch() {
        console.log(`array2D.length=${array2D.length}`);
        for (var i = 3; i < array2D.length; i++) {
            idcount++;
            var clonedelement = $("#draggable").clone();
            clonedelement.attr("id", `draggable-${idcount}`);
            clonedelement.find("input").attr("id", `radio-customize-${idcount}`);
            clonedelement.find("label").attr("for", `radio-customize-${idcount}`);
            clonedelement.find("label").find(".container-left-buy").text(`${array2D[i][0]}`);
            customFolderName.push(array2D[i][0]);
            array2D[i][0] = `radio-customize-${idcount}`;
            clonedelement.find("label").find(".container-left-buy").prepend(`<i class="fa-regular fa-pen-to-square" style="margin-left: 7%;margin-right: 5px;"></i>`);
            $(".add-label").css("display", "none");
            clonedelement.droppable({
                drop: function (event, ui) {
                    $(ui.draggable).css("opacity", "1");
                    var isnotreceipebook = $(ui.draggable).attr("class") == "hotreceipebookmark col-xl-2 gx-0 ui-sortable-helper";
                    if (isnotreceipebook == true) {
                        console.log(`選取到的物件為bookmark`);
                        $(this).find("p").removeClass("turn-green");
                        const radioButtons = document.querySelectorAll('input[name="radio-bookmark"]');
                        const radioGroup = Array.from(radioButtons);
                        console.log(radioGroup);
                        console.log(`選取到的資料夾id為${radioGroup[radiocount].id}`);
                        var dropthefolder = 0;
                        for (var i = 0; i < array2D.length; i++) {
                            console.log(`${$(this).find("input").prop("id")} == ${array2D[i][0]} i=${i}`);
                            if ($(this).find("input").prop("id") == array2D[i][0]) {
                                dropthefolder = i;
                                console.log(`dropthefolder=${dropthefolder}`);
                                break;
                            }
                        }
                        console.log($(this).find("p")[0].innerText);
                        console.log(`我要把第${radiocount + 1}個收藏夾裡(食譜id為${$(ui.draggable).prop("id")})放進${$(this).find("p")[0].innerText}資料夾`);
                        addrecipe2custom(userNum, $(this).find("p")[0].innerText, $(ui.draggable).prop("id"));
                        array2D[dropthefolder].push($(ui.draggable).prop("id"));
                        // console.log($(this));
                        let selectedRadio = document.querySelector('input[name="radio-bookmark"]:checked');
                        let label = document.querySelector(`label[for="${selectedRadio.id}"]`);
                        console.log(label.innerText);
                        var templabel = label.innerText.trimStart();
                        var word = `已從"${templabel}"\n新增"${$(ui.draggable).find(".hotreceipe-introduction").text()}"\n進"${$(this).find("p")[0].innerText}"`;
                        showToast(word);
                    }
                }
                , over: function (event, ui) {
                    var isnotreceipebook = $(ui.draggable).attr("class") == "hotreceipebookmark col-xl-2 gx-0 ui-sortable-helper";
                    if (isnotreceipebook == true) {
                        $(this).find("p").addClass("turn-green");
                        $(ui.draggable).css("opacity", "0.1");
                    }
                }
                , out: function (event, ui) {
                    var isnotreceipebook = $(ui.draggable).attr("class") == "hotreceipebookmark col-xl-2 gx-0 ui-sortable-helper";
                    if (isnotreceipebook == true) {
                        $(this).find("p").removeClass("turn-green");
                        $(ui.draggable).css("opacity", "1");
                    }
                }
            }
            );
            $("#customize-sample").append(clonedelement);
            //console.log($(".container-left").css("height"));
            var height = parseInt($(".container-left").css("height")) + 40;
            $(".container-left").css("height", height);
        }
    }
    //從收藏夾拖曳食譜至客製化收藏夾---------------------------------------------------------------------------------------------------------------------
    function addrecipe2custom(userid, customfolderName, recipeid) {
        const recipeDetail = {
            UserId: parseInt(userid),
            CustomFolderName: customfolderName,
            RecipeId: parseInt(recipeid)
        };
        console.log(recipeDetail);
        console.log(JSON.stringify(recipeDetail));
        var url = "/api/UserFolder/addrecipecustom";
        fetch(url, {
            method: 'POST',
            headers: {
                'Content-Type':'application/json'
            },
            body: JSON.stringify(recipeDetail)
        })
            .then(response => {
                if (!response.ok) {
                    throw new Error('Network response was not ok');
                }
                return response.json();
            })
            .then(data => {
                console.log('Success:', data);
                //alert('Recipe added to custom folder successfully');
            })
            .catch(error => {
                console.error('There was a problem with the fetch operation:', error);
            });
    }
    //增加收藏資料夾的事件---------------------------------------------------------------------------------------------------------------------
    function addnewcustomfolder(userid, customfolderName) {
        const folderDetail = {
            UserId: parseInt(userid),
            CustomFolderName: customfolderName
        };
        console.log(folderDetail);
        console.log(JSON.stringify(folderDetail));
        var url = "/api/UserFolder/addfoldercustom";
        fetch(url, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(folderDetail)
        })
            .then(response => {
                if (!response.ok) {
                    throw new Error('Network response was not ok');
                }
                return response.json();
            })
            .then(data => {
                console.log('Success:', data);
                //alert('Added new custom folder successfully');
            })
            .catch(error => {
                console.error('There was a problem with the fetch operation:', error);
            });
    }
    //刪除資料夾的事件---------------------------------------------------------------------------------------------------------------------
    function deletenewcustomfolder(userid, customfolderName) {
        const folderDetail = {
            UserId: parseInt(userid),
            CustomFolderName: customfolderName
        };
        console.log(folderDetail);
        console.log(JSON.stringify(folderDetail));
        var url = "/api/UserFolder/deletefoldercustom";
        fetch(url, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(folderDetail)
        })
            .then(response => {
                if (!response.ok) {
                    throw new Error('Network response was not ok');
                }
                return response.json();
            })
            .then(data => {
                console.log('Success:', data);
                //alert('delete custom folder successfully');
            })
            .catch(error => {
                console.error('There was a problem with the fetch operation:', error);
            });
    }
    //刪除資料夾內食譜的事件---------------------------------------------------------------------------------------------------------------------
    function deleterecipe(userid,customfolderName, recipeid) {
        const recipeDetail = {
            UserId: parseInt(userid),
            CustomFolderName: customfolderName,
            RecipeId: parseInt(recipeid)
        };
        console.log(recipeDetail);
        console.log(JSON.stringify(recipeDetail));
        var url = "/api/UserFolder/deleterecipe";
        fetch(url, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(recipeDetail)
        })
            .then(response => {
                if (!response.ok) {
                    throw new Error('Network response was not ok');
                }
                return response.json();
            })
            .then(data => {
                console.log('Success:', data);
                //alert('Delete recipe successfully');
            })
            .catch(error => {
                console.error('There was a problem with the fetch operation:', error);
            });
    }
    //調整ingredient的checkbox---------------------------------------------------------------------------------------------------------------------
    //function changecheckbox(userid,recipeid,ingrdientname,ingredientcheck) {
    //    const ingredientDetail = {
    //        UserId: parseInt(userid),
    //        RecipeId: parseInt(recipeid),
    //        IngredientsName: ingrdientname,
    //        Checkbox: ingredientcheck.toString()
    //    };
    //    console.log(ingredientDetail);
    //    console.log(JSON.stringify(ingredientDetail));
    //    var url = "/api/UserFolder/changecheckbox";
    //    fetch(url, {
    //        method: 'POST',
    //        headers: {
    //            'Content-Type': 'application/json'
    //        },
    //        body: JSON.stringify(ingredientDetail)
    //    })
    //        .then(response => {
    //            if (!response.ok) {
    //                throw new Error('Network response was not ok');
    //            }
    //            return response.json();
    //        })
    //        .then(data => {
    //            console.log('Success:', data);
    //            //alert('Change checkbox recipe successfully');
    //        })
    //        .catch(error => {
    //            console.error('There was a problem with the fetch operation:', error);
    //        });
    //}
    async function changecheckbox(userid, recipeid, ingrdientname, ingredientcheck) {
        const ingredientDetail = {
            UserId: parseInt(userid),
            RecipeId: parseInt(recipeid),
            IngredientsName: ingrdientname,
            Checkbox: ingredientcheck.toString()
        };
        console.log(ingredientDetail);
        console.log(JSON.stringify(ingredientDetail));

        const url = "/api/UserFolder/changecheckbox";

        try {
            const response = await fetch(url, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(ingredientDetail)
            });

            if (!response.ok) {
                throw new Error('Network response was not ok');
            }

            const data = await response.json();
            console.log('Success:', data);
            // alert('Change checkbox recipe successfully');
        } catch (error) {
            console.error('There was a problem with the fetch operation:', error);
        }
    }

    //調整shopingredient的checkbox---------------------------------------------------------------------------------------------------------------------
    async function changeshopbox(userid, folderName, ingredientName, check) {
        try {
            const url = `/api/UserFolder/changeshopcheckbox/${userid}/${folderName}/${ingredientName}/${check}`;
            const response = await fetch(url, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify()
            });

            if (!response.ok) {
                throw new Error('Network response was not ok');
            }

            const data = await response.json();
            console.log('Success:', data);
            //alert('Change checkbox recipe successfully');
        } catch (error) {
            console.error('There was a problem with the fetch operation:', error);
        }
    }

    //function changeshopbox(userid, folderName, ingredientName, check) {
    //    var url = `/api/UserFolder/changeshopcheckbox/${userid}/${folderName}/${ingredientName}/${check}`;
    //    fetch(url, {
    //        method: 'POST',
    //        headers: {
    //            'Content-Type': 'application/json'
    //        },
    //        body: JSON.stringify()
    //    })
    //        .then(response => {
    //            if (!response.ok) {
    //                throw new Error('Network response was not ok');
    //            }
    //            return response.json();
    //        })
    //        .then(data => {
    //            console.log('Success:', data);
    //            //alert('Change checkbox recipe successfully');
    //        })
    //        .catch(error => {
    //            console.error('There was a problem with the fetch operation:', error);
    //        });

    //}
    //設置使用者可以把食譜拉進採買清單---------------------------------------------------------------------------------------------------------------------
    $("#radio-buy-id").droppable({
        drop: async function (event, ui) {
            $(ui.draggable).css("opacity", "1");
            var isnotreceipebook = $(ui.draggable).attr("class") == "hotreceipebookmark col-xl-2 gx-0 ui-sortable-helper";
            if (isnotreceipebook == true) {
                console.log(`選取到的物件為bookmark`);
                $(this).find("p").removeClass("turn-yellow");
                const radioButtons = document.querySelectorAll('input[name="radio-bookmark"]');
                const radioGroup = Array.from(radioButtons);
                console.log(radioGroup);
                console.log(`選取到的資料夾id為${radioGroup[radiocount].id}`);
                var dropthefolder = 0;
                for (var i = 0; i < array2D.length; i++) {
                    console.log(`${$(this).find("input").prop("id")} == ${array2D[i][0]} i=${i}`);
                    if ($(this).find("input").prop("id") == array2D[i][0]) {
                        dropthefolder = i;
                        console.log(`dropthefolder=${dropthefolder}`);
                        break;
                    }
                }
                console.log($(this).find("p")[0].innerText);
                console.log(`我要把第${radiocount + 1}個收藏夾裡(食譜id為${$(ui.draggable).prop("id")})放進${$(this).find("p")[0].innerText}資料夾`);
                await addrecipe2custom(userNum, $(this).find("p")[0].innerText, $(ui.draggable).prop("id"));
                let selectedRadio = document.querySelector('input[name="radio-bookmark"]:checked');
                let label = document.querySelector(`label[for="${selectedRadio.id}"]`);
                console.log(label.innerText);
                await shoppingRecipeDetail(userNum, $(ui.draggable).prop("id"), label.innerText);
                var templabel = label.innerText.trimStart();
                var word = `已從"${templabel}"\n新增"${$(ui.draggable).find(".hotreceipe-introduction").text()}"\n進"採買清單"`;
                showToast(word);
            }
        }
        , over: function (event, ui) {
            var isnotreceipebook = $(ui.draggable).attr("class") == "hotreceipebookmark col-xl-2 gx-0 ui-sortable-helper";
            if (isnotreceipebook == true) {
                $(this).find("p").addClass("turn-yellow");
                $(ui.draggable).css("opacity", "0.1");
            }
        }
        , out: function (event, ui) {
            var isnotreceipebook = $(ui.draggable).attr("class") == "hotreceipebookmark col-xl-2 gx-0 ui-sortable-helper";
            if (isnotreceipebook == true) {
                $(this).find("p").removeClass("turn-yellow");
                $(ui.draggable).css("opacity", "1");
            }
        }
    }
    );
    //調整ingredient的checkbox---------------------------------------------------------------------------------------------------------------------
    async function shoppingRecipeDetail(userid, recipeid, foldername) {
        const shopIngredientDetail = {
            UserId: parseInt(userid),
            RecipeId: parseInt(recipeid),
            FolderName: foldername.toString()
        };
        console.log(shopIngredientDetail);
        console.log(JSON.stringify(shopIngredientDetail));
        const url = "/api/UserFolder/addToShoppingList";

        try {
            const response = await fetch(url, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(shopIngredientDetail)
            });

            if (!response.ok) {
                throw new Error('Network response was not ok');
            }

            const data = await response.json();
            console.log('Success:', data);
            //alert('Change checkbox recipe successfully');
        } catch (error) {
            console.error('There was a problem with the fetch operation:', error);
        }
    }

    //function shoppingRecipeDetail(userid, recipeid, foldername) {
    //    const shopIngredientDetail = {
    //        UserId: parseInt(userid),
    //        RecipeId: parseInt(recipeid),
    //        FolderName: foldername.toString()
    //    };
    //    console.log(shopIngredientDetail);
    //    console.log(JSON.stringify(shopIngredientDetail));
    //    var url = "/api/UserFolder/addToShoppingList";
    //    fetch(url, {
    //        method: 'POST',
    //        headers: {
    //            'Content-Type': 'application/json'
    //        },
    //        body: JSON.stringify(shopIngredientDetail)
    //    })
    //        .then(response => {
    //            if (!response.ok) {
    //                throw new Error('Network response was not ok');
    //            }
    //            return response.json();
    //        })
    //        .then(data => {
    //            console.log('Success:', data);
    //            //alert('Change checkbox recipe successfully');
    //        })
    //        .catch(error => {
    //            console.error('There was a problem with the fetch operation:', error);
    //        });
    //}
    //----------------------------------------------------------------------------------------------------------------------------------------------
    async function getShopRecipe(userId) {
        $(".hot_receipe_space").empty();

        try {
            let response = await fetch(`/api/UserFolder/ShowShoppingList/${userId}`);
            if (!response.ok) {
                throw new Error('Network response was not ok');
            }

            let data = await response.json();

            console.log(data);
            console.log(showFirstType(data));
            //console.log(showSecondType(data));
            //console.log(findSuitableRecipe(data, "Lettuce", 200));
        } catch (error) {
            console.error('Error fetching user folder data:', error);
        }
    }
    //function getShopRecipe(userId) {
    //    $(".hot_receipe_space").empty();
    //    fetch(`/api/UserFolder/ShowShoppingList/${userId}`)
    //        .then(response => response.json())
    //        .then(data => {
    //            console.log(data);
    //            console.log(showFirstType(data));
    //            //console.log(showSecondType(data));
    //            //console.log(findSuitableRecipe(data, "Lettuce", 200));
    //        })
    //        .catch(error => console.error('Error fetching user folder data:', error));
    //}
    async function getShopRecipe(userId) {
        $(".hot_receipe_space").empty();

        try {
            const response = await fetch(`/api/UserFolder/ShowShoppingList/${userId}`);

            if (!response.ok) {
                throw new Error('Network response was not ok');
            }

            const data = await response.json();
            console.log(data);
            console.log(showFirstType(data));
            // console.log(showSecondType(data));
            // console.log(findSuitableRecipe(data, "Lettuce", 200));
        } catch (error) {
            console.error('Error fetching user folder data:', error);
        }
    }

    function showFirstType(data) {
        const groupedByFolder = data.reduce((acc, curr) => {
            const folderName = curr.folderName.trim();
            if (!acc[folderName]) {
                acc[folderName] = [];
            }
            acc[folderName].push(curr);
            return acc;
        }, {});
        const result = {};
        for (const folderName in groupedByFolder) {
            const items = groupedByFolder[folderName];
            const summedItems = items.reduce((acc, curr) => {
                const key = curr.shoppingIngredientsName;
                if (!acc[key]) {
                    acc[key] = {
                        shoppingIngredientsName: curr.shoppingIngredientsName,
                        shoppingIngredientsNumber: 0,
                        shoppingIngredientsUnit: curr.shoppingIngredientsUnit,
                        checkbox: curr.checkbox,
                        ingredientTime: curr.ingredientTime
                    };
                }
                acc[key].shoppingIngredientsNumber += curr.shoppingIngredientsNumber;
                return acc;
            }, {});
            result[folderName] = Object.values(summedItems);
        }
        const recipeCategories = Object.keys(result);
        //console.log(recipeCategories);
        recipeCategories.forEach(category => {
            $("#receipe-collection").append(`<div class="folderName " id="folder-${category}"><span class="folderName-title">${category}</span><span class="switch-space">顯示食譜<label class="switch"><input type="checkbox"><span class="slider round"></span></label></span><i class="fa-solid fa-delete-left fa-lg delete-shopfolder" style=""></i></div>`);
            $(".folderName").sortable();
            //收藏的食譜[0].shoppingIngredientsName
            const recipes = result[category];
            recipes.forEach(ingredientdetail => {
                var temp = `#folder-${category}`;
                if(ingredientdetail.checkbox==true)
                    $(temp).append(`<div class="folderIngredient" id="ingredientdetail" style="position:relative;"><div class="ingredienttime" style="position:absolute; font-size:10px; color:green; right:10px; bottom:-5px;">${ingredientdetail.ingredientTime.slice(-8,-3)}</div><input type="checkbox" name="" class="folderPrepare" checked><span class="folderIngredientName">${ingredientdetail.shoppingIngredientsName} </span><span class="folderCount">${ingredientdetail.shoppingIngredientsNumber}</span><span class="folderUnit">${ingredientdetail.shoppingIngredientsUnit}</span></div>`);
                else
                    $(temp).append(`<div class="folderIngredient" id="ingredientdetail" style="position:relative;"><input type="checkbox" name="" class="folderPrepare"><span class="folderIngredientName">${ingredientdetail.shoppingIngredientsName}</span><span class="folderCount">${ingredientdetail.shoppingIngredientsNumber}</span><span class="folderUnit">${ingredientdetail.shoppingIngredientsUnit}</span></div>`);
                //if (ingredientdetail.checkbox == true) {
                //    $(".folderIngredient").find(".ingredienttime").remove();
                //    $(".folderIngredient").append(`<div class="ingredienttime" style="position:absolute; font-size:10px; color:green; right:100px; bottom:-5px;">${ingredientdetail.ingredientTime.slice(-8)}</div>`);
                //} else {
                //    $(".folderIngredient").find(".ingredienttime").remove();
                //}
                adjustContainerHeight();
            })
        });
        return result;
    }
    
    function showSecondType(data) {
        return data.reduce((acc, curr) => {
            const folderName = curr.folderName.trim();
            if (!acc[folderName]) {
                acc[folderName] = [];
            }
            acc[folderName].push(curr);
            return acc;
        }, {});
    }
    function groupAndSumByFolderName(data) {
        const groupedByFolder = data.reduce((acc, curr) => {
            const folderName = curr.folderName.trim();
            if (!acc[folderName]) {
                acc[folderName] = [];
            }
            acc[folderName].push(curr);
            return acc;
        }, {});
        const result = {};
        for (const folderName in groupedByFolder) {
            const items = groupedByFolder[folderName];
            const summedItems = items.reduce((acc, curr) => {
                const key = curr.shoppingIngredientsName;
                if (!acc[key]) {
                    acc[key] = {
                        shoppingIngredientsName: curr.shoppingIngredientsName,
                        shoppingIngredientsNumber: 0,
                        shoppingIngredientsUnit: curr.shoppingIngredientsUnit,
                        checkbox: curr.checkbox,
                        ingredientTime: curr.ingredientTime
                    };
                }
                acc[key].shoppingIngredientsNumber += curr.shoppingIngredientsNumber;
                return acc;
            }, {});
            result[folderName] = Object.values(summedItems);
        }
        return result;
    }
    function findSuitableRecipe(data, ingredientName, availableQuantity) {
        const groupedData = groupAndSumByFolderName(data);
        const suitableRecipes = [];
        for (const folderName in groupedData) {
            const ingredients = groupedData[folderName];
            for (const ingredient of ingredients) {
                if (ingredient.shoppingIngredientsName === ingredientName && ingredient.shoppingIngredientsNumber <= availableQuantity) {
                    suitableRecipes.push({
                        folderName,
                        recipeName: ingredient.recipeName,
                        shoppingIngredientsNumber: ingredient.shoppingIngredientsNumber,
                        shoppingIngredientsUnit: ingredient.shoppingIngredientsUnit
                    });
                }
            }
        }
        return suitableRecipes.length > 0 ? suitableRecipes[0] : {};
        //const availableQuantity = 125;
        const suitableRecipe = findSuitableRecipe(data, "Lettuce", availableQuantity);
        //console.log(suitableRecipe);
    }
    //----------------------------------------------------------------------------------------------------------------------------------------------
    let greenrecipe = [];
    $(document).on("change", ".switch", async function () {
        var category = $(this).closest(".folderName").find(".folderName-title").text();
        var check = $(this).find("input").prop("checked");
        console.log(greenrecipe);
        if (check == true) {
            greenrecipe.length = 0;
            await GreenRecipe(userNum, category);
            await getShopFolderRecipe(userNum, category);
        } else {
            $(this).closest(".folderName").find(".hotshopreceipebookmark").remove();
        } 
        console.log(`Category: ${category},Check: ${check}`);
    });
    //----------------------------------------------------------------------------------------------------------------------------------------------
    var colorgreen = 1;

    async function getShopFolderRecipe(userId, folderName) {
        try {
            const response = await fetch(`/api/UserFolder/getShopFolderRecipe/${userId}/${folderName}`);
            const data = await response.json();

            for (let i = 0; i < data.length; i++) {
                colorgreen = 0;
                console.log(`greenrecipe.length=${greenrecipe.length}`);
                for (let j = 0; j < greenrecipe.length; j++) {
                    if (data[i] == greenrecipe[j]) {
                        console.log(`${data[i]} need to be color`);
                        colorgreen = 1;
                        break;
                    } else {
                        console.log(`${data[i]} don't need to be color`);
                    }
                }
                console.log(`${data[i]} colorgreen is ${colorgreen}`);
                if (colorgreen == 1) {
                    const recipeResponse = await fetch(`/api/UserFolder/recipe/${userNum}/${data[i]}`);
                    if (!recipeResponse.ok) {
                        throw new Error('Network response was not ok');
                    }
                    const recipe = await recipeResponse.json();
                    var folderId = `#folder-${folderName}`;
                    $(folderId).append(`
                    <div class="hotshopreceipebookmark addgreencool col-xl-2 gx-0" data-ingredient="${recipe.recipeId}" id="${recipe.recipeId}">
                    <i class="fa-regular fa-circle-check fa-7x" style="position:absolute; color:green; left:57px; top:59px;"></i>
                        <i class="show-list fa-solid fa-list-check"  style="color:#000000;position:absolute; top:235px; right:5px;"></i>
                        <div class="hotreceipe-img" style="background-image: url('data:image/png;base64,${recipe.recipeCoverImage}');"></div>
                        <div class="hotreceipe-information row gx-0">
                            <ul class="hot-receipe-data row gx-0">
                                <li class="col-3 gx-0"><i class="fa-regular fa-eye"></i>${recipe.views}</li>
                                <li class="col-3 gx-0"><i class="fa-regular fa-thumbs-up"></i>${recipe.favorites}</li>
                            </ul>
                        </div>
                        <div class="hotreceipe-introduction">${recipe.recipeName}</div>
                    </div>
                `);
                    adjustContainerHeight();
                } else {
                    const recipeResponse = await fetch(`/api/UserFolder/recipe/${userNum}/${data[i]}`);
                    if (!recipeResponse.ok) {
                        throw new Error('Network response was not ok');
                    }
                    const recipe = await recipeResponse.json();
                    var folderId = `#folder-${folderName}`;
                    $(folderId).append(`
                    <div class="hotshopreceipebookmark addredcool col-xl-2 gx-0" data-ingredient="${recipe.recipeId}" id="${recipe.recipeId}">
                        <i class="show-list fa-solid fa-list-check"  style="color:#000000;position:absolute; top:235px; right:5px;"></i>
                        <div class="hotreceipe-img" style="background-image: url('data:image/png;base64,${recipe.recipeCoverImage}');"></div>
                        <div class="hotreceipe-information row gx-0">
                            <ul class="hot-receipe-data row gx-0">
                                <li class="col-3 gx-0"><i class="fa-regular fa-eye"></i>${recipe.views}</li>
                                <li class="col-3 gx-0"><i class="fa-regular fa-thumbs-up"></i>${recipe.favorites}</li>
                            </ul>
                        </div>
                        <div class="hotreceipe-introduction">${recipe.recipeName}</div>
                    </div>
                `);
                    adjustContainerHeight();
                }
            }
        } catch (error) {
            console.error('Error fetching user folder data:', error);
        }
    }
//    var colorgreen = 1;
//    function getShopFolderRecipe(userId, folderName) {
//        fetch(`/api/UserFolder/getShopFolderRecipe/${userId}/${folderName}`)
//            .then(response => response.json())
//            .then(data => {
//                for (var i = 0; i < data.length; i++) {
//                    colorgreen = 0;
//                    console.log(`greenrecipe.length=${greenrecipe.length}`);
//                    for (var j = 0; j < greenrecipe.length; j++) {
//                        if (data[i] == greenrecipe[j]) {
//                            console.log(`${data[i]} need to be color`);
//                            colorgreen = 1;
//                            break;
//                        } else {
//                            console.log(`${data[i]} dont need to be color`);
//                        }
//                    }
//                    console.log(`${data[i]} colorgreen is ${colorgreen}`);
//                    if (colorgreen == 1) {
//                        fetch(`/api/UserFolder/recipe/${userNum}/${data[i]}`)
//                            .then(response => {
//                                if (!response.ok) {
//                                    throw new Error('Network response was not ok');
//                                }
//                                return response.json();
//                            })
//                            .then(recipe => {
//                                //------------------------------------------------------------------------------------------------------------------
//                                //console.log(`recipe.RecipeCoverImage=${recipe.recipeCoverImage}`);
//                                //<div class="hotreceipe-img" style="background-image: url('data:image/png;base64,${recipe.recipeCoverImage}');"></div>
//                                var folderId = `#folder-${folderName}`;
//                                //console.log(`folderId=${folderId}`);
//                                $(folderId).append(`
//                                        <div class="hotshopreceipebookmark addgreencool col-xl-2 gx-0" data-ingredient="${recipe.recipeId}" id="${recipe.recipeId}">
//                                        <i class="show-list fa-solid fa-list-check"  style="color:#000000;position:absolute; top:235px; right:5px;"></i>
//                                            <div class="hotreceipe-img" style="background-image: url(../img/steak.png);"></div>
//                                            <div class="hotreceipe-information row gx-0">
//                                                <ul class="hot-receipe-data row gx-0">
//                                                    <li class="col-3 gx-0"><i class="fa-regular fa-eye"></i>${recipe.views}</li>
//                                                    <li class="col-3 gx-0"><i class="fa-regular fa-thumbs-up"></i>${recipe.favorites}</li>
//                                                </ul>
//                                            </div>
//                                            <div class="hotreceipe-introduction">${recipe.recipeName}</div>
//                                        </div>
//                        `);
//                                //$(".hotreceipebookmark").prepend(`<i class="show-list fa-solid fa-list-check"  style="color:#000000;position:absolute; top:235px; right:5px;"></i>`);
//                                adjustContainerHeight();
//                                //------------------------------------------------------------------------------------------------------------------
//                            })
//                            .catch(error => {
//                                console.error('There was a problem with the fetch operation:', error);
//                            });
//                    } else {
//                        fetch(`/api/UserFolder/recipe/${userNum}/${data[i]}`)
//                            .then(response => {
//                                if (!response.ok) {
//                                    throw new Error('Network response was not ok');
//                                }
//                                return response.json();
//                            })
//                            .then(recipe => {
//                                //------------------------------------------------------------------------------------------------------------------
//                                //console.log(`recipe.RecipeCoverImage=${recipe.recipeCoverImage}`);
//                                //<div class="hotreceipe-img" style="background-image: url('data:image/png;base64,${recipe.recipeCoverImage}');"></div>
//                                var folderId = `#folder-${folderName}`;
///*                                console.log(`folderId=${folderId}`);*/
//                                $(folderId).append(`
//                                        <div class="hotshopreceipebookmark col-xl-2 gx-0" data-ingredient="${recipe.recipeId}" id="${recipe.recipeId}">
//                                        <i class="show-list fa-solid fa-list-check"  style="color:#000000;position:absolute; top:235px; right:5px;"></i>
//                                            <div class="hotreceipe-img" style="background-image: url(../img/steak.png);"></div>
//                                            <div class="hotreceipe-information row gx-0">
//                                                <ul class="hot-receipe-data row gx-0">
//                                                    <li class="col-3 gx-0"><i class="fa-regular fa-eye"></i>${recipe.views}</li>
//                                                    <li class="col-3 gx-0"><i class="fa-regular fa-thumbs-up"></i>${recipe.favorites}</li>
//                                                </ul>
//                                            </div>
//                                            <div class="hotreceipe-introduction">${recipe.recipeName}</div>
//                                        </div>
//                        `);
//                                //$(".hotreceipebookmark").prepend(`<i class="show-list fa-solid fa-list-check"  style="color:#000000;position:absolute; top:235px; right:5px;"></i>`);
//                                adjustContainerHeight();
//                                //------------------------------------------------------------------------------------------------------------------
//                            })
//                            .catch(error => {
//                                console.error('There was a problem with the fetch operation:', error);
//                            });
//                    }
//                }

//            })
//            .catch(error => console.error('Error fetching user folder data:', error));
//    }
    //----------------------------------------------------------------------------------------------------------------------------------------------
    //function deleteshopfolder(userId,folderName) {
    //    fetch(`/api/UserFolder/deleteshopfolder/${userId}/${folderName}`, {
    //        method: 'POST',
    //        headers: {
    //            'Content-Type': 'application/json'
    //        },
    //        body: JSON.stringify()
    //    })
    //        .then(response => {
    //            if (!response.ok) {
    //                throw new Error('Network response was not ok');
    //            }
    //            return response.json();
    //        })
    //        .then(data => {
    //            console.log('Success:', data);
    //        })
    //        .catch(error => {
    //            console.error('There was a problem with the fetch operation:', error);
    //        });
    //}
    async function deleteshopfolder(userId, folderName) {
        try {
            const response = await fetch(`/api/UserFolder/deleteshopfolder/${userId}/${folderName}`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify()
            });

            if (!response.ok) {
                throw new Error('Network response was not ok');
            }

            const data = await response.json();
            console.log('Success:', data);
        } catch (error) {
            console.error('There was a problem with the fetch operation:', error);
        }
    }

    //----------------------------------------------------------------------------------------------------------------------------------------------
    $(document).on("click", ".delete-shopfolder", function () {
        var category = $(this).closest(".folderName").find(".folderName-title").text();
        $(this).closest(".folderName").remove();
        deleteshopfolder(userNum,category);
        console.log(`Category: ${category}`);
    });
    //----------------------------------------------------------------------------------------------------------------------------------------------
    //function deleteshoprecipefolder(userId, folderName,recipeId) {
    //    fetch(`/api/UserFolder/deleteshoprecipefolder/${userId}/${folderName}/${recipeId}`, {
    //        method: 'POST',
    //        headers: {
    //            'Content-Type': 'application/json'
    //        },
    //        body: JSON.stringify()
    //    })
    //        .then(response => {
    //            if (!response.ok) {
    //                throw new Error('Network response was not ok');
    //            }
    //            return response.json();
    //        })
    //        .then(data => {
    //            console.log('Success:', data);
    //        })
    //        .catch(error => {
    //            console.error('There was a problem with the fetch operation:', error);
    //        });
    //}
    async function deleteshoprecipefolder(userId, folderName, recipeId) {
        try {
            const response = await fetch(`/api/UserFolder/deleteshoprecipefolder/${userId}/${folderName}/${recipeId}`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify()
            });

            if (!response.ok) {
                throw new Error('Network response was not ok');
            }

            const data = await response.json();
            console.log('Success:', data);
        } catch (error) {
            console.error('There was a problem with the fetch operation:', error);
        }
        //var folderNameedit = `#folder-${folderName}`
        //var chosenfolder = $(folderNameedit);
        //console.log(`Category: ${chosenfolder}`);

        await getShopRecipe(userNum);
    }

    //----------------------------------------------------------------------------------------------------------------------------------------------
    async function GreenRecipe(userId, category) {
    try {
        const response = await fetch(`/api/UserFolder/filtered-recipes/${userId}/${category}`);
        const data = await response.json();
        for (let i = 0; i < data.length; i++) {
            greenrecipe.push(data[i]);
        }
    } catch (error) {
        console.error('Error fetching user folder data:', error);
    }
    }
    //----------------------------------------------------------------------------------------------------------------------------------------------
    function showToast(word) {
        Toastify({
            text: word,
            duration: 3000, // 持续时间
            //close: true, // 显示关闭按钮
            gravity: "top", // 显示位置
            position: "right", // 显示位置
            backgroundColor: "rgb(255,255,255)", // 背景颜色
            //color: "#000000",
            stopOnFocus: false, // 鼠标悬停时停止自动关闭
            className: "custom-toast",
            gravity: "top",
        }).showToast();
    }
    //var word="測試測試";
    //showToast(word);

   
    
});
