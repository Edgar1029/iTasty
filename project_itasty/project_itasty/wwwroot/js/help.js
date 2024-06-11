function sumbitform(){
    var text=document.getElementById("form_comment").value;
    if(text.trim()===""){
        alert("請說明問題或意見")
    }else{
        alert("已傳送表單")
        console.log(123);
        window.location.href ='index.html';
        
    }
}