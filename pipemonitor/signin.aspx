<%@ Page Language="C#" AutoEventWireup="true" Inherits="signin" Codebehind="signin.aspx.cs" %>



<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html >
  <head>
   
    <title>登录</title>
     <link rel="stylesheet" href="Styles/login.css"/>
     <script type="text/javascript" src="jqwidgets/scripts/jquery-2.0.2.min.js"></script>
      <script type="text/javascript" src="Scripts/json2.js"></script>
     <script  type="text/javascript" src="Scripts/login.js"></script>
   
      <script type="text/javascript">
        function login() 
        {

            var UserName = $("#txtUser").val().trim();
            if (UserName == "") {
               alert("用户名不能为空");
                return false;
            }

            var Pwd = $("#txtPwd").val();
            if (Pwd == "" || Pwd.length < 6) {
                alert("密码不能少于6位");
                return false;
            }

            $.ajax(
            {
                type: "POST",
                contentType: "application/json",
                url: "signin.aspx/userLogin",
                data: "{'loginname':'" + UserName + "','password':'" + Pwd + "'}",
                dataType: "json",
                success: function (msg) {


                    var ret=  $.parseJSON(msg.d);
                    if (ret.msg != "ok") {
                        alert(ret.msg);
                    }
                    else {
                        var userInfo = $.parseJSON(msg.d);
                        //   window.open("default.aspx");
                        window.location.href = "default.aspx";

                    }
                }
            });

        }

    </script>
    
    
</head>

  <body style="background-image:url('Images/loginbk.jpg')">
 <%--   <div style="height:150px;"><img height=120 width=120 src="Images/wp4.png" /></div>--%>
    <div class="wrapper">
	<div class="container">
		<h1>欢迎使用</h1>
		
		<form class="form">
			<input style="background-color:transparent"  id="txtUser" type="text" placeholder="用户名"/><span style="font-size:12px;float:left" id="UserMsg"> </span>
			<input style="background-color:transparent" id="txtPwd"  type="password" placeholder="密码"/><span id="PwdMsg"> </span>
			<input  onclick="login()"  value="登录" type="button" id="login-button"/>
		</form>
	</div>
	
	<ul class="bg-bubbles">
		<li></li>
		<li></li>
		<li></li>
		<li></li>
		<li></li>
		<li></li>
		<li></li>
		<li></li>
		<li></li>
		<li></li>
	</ul>
</div>
   
   

    
    
    
  </body>
</html>
