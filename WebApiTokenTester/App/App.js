
$(document).ready(function() {
    $("#Login").click(function() {
        var userName = $("#UserName").val();
        var password = $("#Password").val();
        
        $.ajax({
            url: "http://localhost:11050/token",
            type: "POST",
            crossDomain: true,
            data: {
                "username": userName,
                "password": password,
                "grant_type": "password"
            },
            dataType: "json",
            success: function (result) {
                alert("Logged in successfully");
                localStorage.setItem('Token', JSON.stringify(result));
            },
            error: function (xhr, status, error) {
                alert(status+" " +error);
            }
        });
        
    });
    $("#Logout").click(function () {
        if (localStorage.getItem('Token')==null) {
            alert("You already logged out");
            return;
        }
        alert("Logged out successfully");
        localStorage.removeItem('Token');
    });
    $("#Get").click(function () {
        var token = $.parseJSON(localStorage.getItem('Token'));
        if (token == null) {
            alert("Unauthorized Request");
            return;
        }
        var accessToken = token.access_token;
        
       
        $.ajax({
            url: "http://localhost:11050/api/Fatura",
            type: "Get",
            crossDomain: true,
            dataType: "json",
            headers: {
                "accept": "application/json",
                "content-type": "application/json",
                "authorization": "Bearer "+ accessToken
                },
            success: function (result) {
                alert(JSON.stringify(result));
            },
            error: function (xhr, status, error) {
                alert(status + " " + error);
            }
        });
    });
});