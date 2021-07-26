<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmPassword.aspx.cs" Inherits="Modules_Default" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <!------------------<<Import Style sheet Reference>>------------------------>
    <title>Barcode System | Version 1.0</title>
    <link href="../App_Themes/Styles/bootstraplogin.min.css" rel="stylesheet" type="text/css" />
    <script src="../App_Themes/Styles/bootstraplogin.min.js" type="text/javascript"></script>
    <script src="../App_Themes/Styles/jquerylogin-1.11.1.min.js" type="text/javascript"></script>
    <link href="../App_Themes/Styles/LoginStyleSheet.css" rel="stylesheet" type="text/css" />
    <link href="../App_Themes/Styles/Style_Controls.css" rel="stylesheet" type="text/css" />
    <link href="../App_Themes/Styles/Style_Design.css" rel="stylesheet" type="text/css" />
    <link href="../App_Themes/Styles/Style_Grid.css" rel="stylesheet" type="text/css" />
    <script src="../Masters/Javascript/JScript.js" type="text/javascript"></script>
    <style type="text/css">
        .style5
        {
            height: 23px;
        }
        .style8
        {
            width: 313px;
        }
    </style>
    <style type="text/css">
        .Progress_Login
        {
            font-family: "Arial";
        }
        #Background
        {
            background-color: #F2F2F2;
        }
        #test
        {
            width: 100%;
            margin: 0px auto;
            height: 50px;
            position: fixed;
            bottom: 0;
        }
        .style1
        {
            font-family: "Arial";
            font-size: 11pt;
        }
        @import url('https://fonts.googleapis.com/css?family=Raleway:100,100i,200,200i,300,300i,400,400i,500,500i,600,600i,700,700i,800,800i,900,900i&subset=latin-ext');


    #playground-container {
        height: 500px;
        overflow: hidden !important;
    
    }
    .main{margin-top:70px; 
    -webkit-box-shadow: 0px 0px 14px 0px rgba(0,0,0,0.24);
    -moz-box-shadow: 0px 0px 14px 0px rgba(0,0,0,0.24);
    box-shadow: 0px 0px 14px 0px rgba(0,0,0,0.24);
    padding:0px;
    background:white;
    }
    .fb:focus, .fb:hover{color:;}
    body{
    font-family: 'Raleway', sans-serif;
    }

    .left-side{
	padding:0px 0px 0px;
    background-size:cover;
    background-size:cover;
    }
    .left-side h3{
	    font-size: 30px;
        font-weight: 700;
	    color:#FFF;
	    padding: 50px 10px 00px 26px;
	    }
	
	    .left-side p{
        font-weight:600;
	    color:#FFF;
	    padding:10px 10px 10px 26px;
	    }
	.fb{background: #2d6bb7;
    color: #FFF;
    padding: 10px 15px;
    border-radius: 18px;
    font-size: 12px;
    font-weight: 600;
    margin-right: 15px;
	margin-left:26px;-webkit-box-shadow: 0px 0px 14px 0px rgba(0,0,0,0.24);
    -moz-box-shadow: 0px 0px 14px 0px rgba(0,0,0,0.24);
    box-shadow: 0px 0px 14px 0px rgba(0,0,0,0.24);}
	.tw{background: #20c1ed;
    color: #FFF;
    padding: 10px 15px;
    border-radius: 18px;
    font-size: 12px;
    font-weight: 600;
    margin-right: 15px;-webkit-box-shadow: 0px 0px 14px 0px rgba(0,0,0,0.24);
    -moz-box-shadow: 0px 0px 14px 0px rgba(0,0,0,0.24);
    box-shadow: 0px 0px 14px 0px rgba(0,0,0,0.24);}
	
	.right-side{
	padding:0px 0px 0px;
	background:#0063BE;
	background-size:cover;
	min-height:514px;
    }
	.right-side h3{
	font-size: 25px;
    font-weight: 700;
	color:white;
	padding: 50px 10px 00px 50px;
	}
	.right-side p{
    font-weight:600;
	color:white;
	padding:10px 50px 10px 50px;
	}
	.form{padding:10px 50px 10px 50px;}
    .form-control{box-shadow: none !important;
    border-radius: 0px !important;
    border-bottom: 3px solid white !important;
    border-top: 1px !important;
    border-left: none !important;
    border-right: none !important;}
	.btn-deep-purple {
    background: white;
    border-radius: 18px;
    padding: 5px 19px;
    color: #0063BE;
    font-weight: 600;
    float: right;
	-webkit-box-shadow: 0px 0px 14px 0px rgba(0,0,0,0.24);
    -moz-box-shadow: 0px 0px 14px 0px rgba(0,0,0,0.24);
    box-shadow: 0px 0px 14px 0px rgba(0,0,0,0.24);
    }
    .btn 
    {
    	margin-left: 20px;
    	font-size:15px;
    }
    .text-xs-left
    {
    	margin-top:20px
    }
    </style>
</head>
<body class="body" style="margin: 0; font-family: Arial; background-repeat: repeat-x;
    font-size: 12pt;">
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <div style="text-align: center; font-family: Calibri; font-size: 15px">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="container" style="text-align: center; width:500px">
                        <div class="col-md-2 col-md-offset-2 main" style="text-align:center; vertical-align:middle;">
                           <%--<div>
                           <asp:Image ID="Image1" runat="server" ImageUrl="~/App_Themes/Styles/Images/LoginMain.png" style=" text-align:center; height:80px ; width:160px" />
                           </div>--%>
                            <!--Form with header-->
                          
                            <div class="form">
                              <div class="form-group">
                              <asp:Label runat="server" CssClass="Label" Text="Change Password" ForeColor="#0063BE" 
                                        Font-Bold="true" Font-Size="23px"></asp:Label>
                            </div >
                                <div class="form-group" style="margin-top: 5px">
                                    <asp:Label ID="Label1" runat="server" CssClass="Label" Text="User ID" ForeColor="#0063BE"
                                        Font-Bold="true" Font-Size="15px"></asp:Label>
                                    <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control" Font-Bold="true"  style="border-bottom: 1px solid #0063BE; background-color:#CCCCFF; "
                                        Font-Size="15px"></asp:TextBox>
                                </div>
                                <div class="form-group" style="margin-top:5px">
                                    <asp:Label ID="Label6" runat="server" CssClass="Label" Text="Old Password" ForeColor="#0063BE"  Font-Bold="true" Font-Size="15px"></asp:Label>
                                    <asp:TextBox ID="txtPassword" runat="server" autocomplete="off" CssClass="form-control"
                                        onkeypress="Javascript:return isRestructionKey(event);" TextMode="Password" style="border-bottom: 1px solid #0063BE; background-color:#CCCCFF; "></asp:TextBox>
                                </div>
                                <div class="form-group" style="margin-top: 5px">
                                    <asp:Label ID="Label7" runat="server" CssClass="Label" Text="New Password" ForeColor="#0063BE"  Font-Bold="true" Font-Size="15px"></asp:Label>
                                    <asp:TextBox ID="txtNewPassword" runat="server" autocomplete="off" CssClass="form-control"
                                        MaxLength="8" onkeypress="Javascript:return isRestructionKey(event);" TextMode="Password" style=" background-color:#CCCCFF; border-bottom: 1px solid #0063BE;"></asp:TextBox>
                                    <asp:PasswordStrength ID="ps" runat="server" BehaviorID="lblPass" StrengthIndicatorType="Text"
                                        TargetControlID="txtNewPassword">
                                    </asp:PasswordStrength>
                                    <asp:Label ID="lblPass" runat="server"></asp:Label>
                                </div>
                                <div class="form-group" style="margin-top: 5px">
                                    <asp:Label ID="Label8" runat="server" CssClass="Label" Text="Confirm New Password" ForeColor="#0063BE"  Font-Bold="true" Font-Size="15px"></asp:Label>
                                    <asp:TextBox ID="txtConfirmPassword" runat="server" autocomplete="off" CssClass="form-control"
                                        MaxLength="8" onkeypress="Javascript:return isRestructionKey(event);" TextMode="Password" style="border-bottom: 1px solid #0063BE; background-color:#CCCCFF; "></asp:TextBox>
                                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txtNewPassword"
                                        ControlToValidate="txtConfirmPassword" ErrorMessage="*" Font-Bold="True" Font-Size="Large"
                                        ToolTip="The confirm new password must match with new password." ValidationGroup="Save"></asp:CompareValidator>
                                </div>
                                <div class="form-group" style=" text-align:center ; margin-top:5px">
                                <asp:Button ID="btnChangePassword" runat="server" CssClass="btn"  Text="Change Password"
                                     Style="background-color:#0063BE; color:White"   ValidationGroup="Login" OnClick="btnChangePassword_Click" />
                                    <asp:Button ID="btnCancel" runat="server"  Text="Cancel" CssClass="btn"
                                        Style="background-color:#0063BE; color:White" ValidationGroup="Login" OnClick="btnCancel_Click" />
                                    
                                </div>
                                <div class="form-group" style=" text-align:center; margin-top:5px" >
                                <asp:LinkButton ID="btnLoginAgain" runat="server" CssClass="btn"  Text="Click here to Login"
                                     Style="background-color:White; color:#0063BE; font-weight:bolder; font-size:20px;"   ValidationGroup="Login" 
                                        onclick="btnLoginAgain_Click" ></asp:LinkButton>
                                </div>
                                 
                            </div>
                            <!--/Form with header-->
                        </div>
                        <!--col-sm-6-->
                   
                    <!--col-sm-8-->
                </div>
                
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnChangePassword" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
