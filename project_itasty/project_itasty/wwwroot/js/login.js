const container = document.getElementById('content_container');
const registerBtn = document.getElementById('toggle_register');
const loginBtn = document.getElementById('toggle_login');

registerBtn.addEventListener('click', () => {
    container.classList.add("active");
});

loginBtn.addEventListener('click', () => {
    container.classList.remove("active");
});


function register_btn_click() {


   

}

//$(document).ready(function () {
//    let users = [];
//
//    $('#form_register').input(function (event) {
//        
//
//        const username = $('#Register_UserName').val();
//        const email = $('#Register_Email').val();
//        const password =$('#Register_Password').val();
//
//        // const password = CryptoJS.SHA256($('#Register_Password').val()).toString();
//       
//            if (users.some(user => user.email === username)) {
//                $('#message').text('此信箱已被註冊');
//            } else {
//                users.push({ username, email, password });
//                // $('#message').text('註冊成功');
//                $('#form_register')[0].reset();
//
//                container.classList.remove("active");
//                console.log(password);
//                //alert("註冊成功")
//            }
//        
//    });
//
//    $('#form_login').input(function (event) {
//        event.preventDefault();
//
//        const email = $('#Login_Email').val();
//
//        const password = $('#Login_Password').val();
//        // const password = CryptoJS.SHA256($('#Login_Password').val()).toString();
//
//        const user = users.find(user => user.email === email && user.password === password);
//        if (user) {
//            alert('登入成功')
//            window.location.href = 'index.html';
//        } else {
//            alert('使用者名稱或密碼錯誤');
//
//        }
//
//        $('#form_login')[0].reset();
//    });
//});