<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Barcode System | Version 1.0</title>
    
    <link href="../App_Themes/Styles/LoginStyleSheet.css" rel="stylesheet" type="text/css" />
    <link href="../App_Themes/Styles/Style_Controls.css" rel="stylesheet" type="text/css" />
    <link href="../App_Themes/Styles/Style_Design.css" rel="stylesheet" type="text/css" />
    <link href="../App_Themes/Styles/Style_Grid.css" rel="stylesheet" type="text/css" />
    <link href="../App_Themes/Styles/bootstraplogin.min.css" rel="stylesheet" type="text/css" />
    <script src="../App_Themes/Styles/bootstraplogin.min.js" type="text/javascript"></script>
    <script src="../App_Themes/Styles/jquerylogin-1.11.1.min.js" type="text/javascript"></script>
    <script src="../Masters/Javascript/JScript.js" type="text/javascript"></script>
    <script src="../App_Themes/Styles/jquery.min.js" type="text/javascript"></script>

    <script type="text/javascript">
        function pageLoad(sender, args) {
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_beginRequest(BeginRequestHandler);
            prm.add_endRequest(EndRequestHandler);
        }

        function BeginRequestHandler(sender, args) {
            $('#DisableDiv').fadeIn('slow', .8);
            $('#DisableDiv').append('<div class="form-group" style="position: fixed; text-align: center; height: 100%; width: 100%; top:0; right: 30; left: 0; background-color: #DEE9F1  ;z-index: 9999999; opacity:0.8"  align="center"><img src="../App_Themes/Styles/Images/round.gif" style="padding: 10px;position:fixed;top:20%;left:30%;"  /></div>');
            document.getElementById("DisableDiv").style.visibility = 'visible';
        }

        function EndRequestHandler(sender, args) {
            document.getElementById("DisableDiv").style.visibility = 'hidden';
        }
    </script>
  
    <style type="text/css">
         .style5
        {
            height: 23px;
        }
        .style8
        {
            width: 313px;
        }
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
    <div id="DisableDiv"></div>
    <div style="text-align: center; font-family: Calibri; font-size: 15px">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="container">
                    <asp:Panel ID="Panel1" runat="server">
                        <div class="col-md-10 col-md-offset-1 main">
                            <div class="col-md-6 left-side" style="text-align: center; vertical-align: middle;
                                margin-top: 150px">
                                <asp:Image ID="Image1" runat="server" ImageUrl="~/App_Themes/Styles/Images/LoginMain.png"
                                    Style="text-align: center" />
                                <%--<div style="margin-top: 250px; margin-left: 50px">
                                <asp:Image ID="Image2" ImageUrl="~/App_Themes/Styles/Images/download (2).png" runat="server" />
                            </div>--%>
                            </div>
                            <!--col-sm-6-->
                            <div class="col-md-6 right-side">
                                <h3>
                                    <u>Barcode Application System </u>
                                </h3>
                                <!--Form with header-->
                                <div class="form">
                                    <div class="form-group" style="margin-top: 20px">
                                        <asp:Label ID="Label1" runat="server" CssClass="Label" Text="User ID" ForeColor="White"
                                            Font-Bold="true" Font-Size="15px"></asp:Label>
                                        <asp:TextBox ID="txtUserID" runat="server" CssClass="form-control" Font-Bold="true"
                                            Font-Size="15px"></asp:TextBox>
                                    </div>
                                    <div class="form-group" style="margin-top: 20px">
                                        <asp:Label ID="Label3" runat="server" CssClass="Label" Text="Password" Font-Bold="true"
                                            ForeColor="White" Font-Size="15px"></asp:Label>
                                        <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password"
                                            Font-Bold="true" Font-Size="15px"></asp:TextBox>
                                    </div>
                                    <div class="form-group" style="margin-top: 20px">
                                        <asp:Label ID="Label4" runat="server" CssClass="Label" Text="Plant" Font-Bold="true"
                                            ForeColor="White" Font-Size="15px"></asp:Label>
                                        <asp:DropDownList ID="dd_PlantCode" runat="server" CssClass="form-control" ForeColor="Black"
                                            Font-Bold="true" Font-Size="15px">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="text-xs-center" style="margin-top: 50px">
                                        <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-deep-purple" Text="Cancel"
                                            Style="margin-left: 10px" ValidationGroup="Login" OnClick="btnCancel_Click" />
                                        <asp:Button ID="btnLogin" runat="server" CssClass="btn btn-deep-purple" Text="Login"
                                            ValidationGroup="Login" OnClick="btnLogin_Click" />
                                    </div>
                                    <div class="text-xs-left">
                                        <asp:LinkButton Text="Forget Passowrd" runat="server" Font-Bold="true" ID="Forgot"
                                            PostBackUrl="~/Modules/frmPassword.aspx" ForeColor="White" Font-Size="15px" Font-Underline="true"
                                            OnClick="Forgot_Click"></asp:LinkButton>
                                    </div>
                                    <div style="visibility: hidden">
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:Button ID="btnGetPlant" runat="server" float="left" class="btn-primary" Text="Get Plant"
                                                        Height="30px" Width="49%" ValidationGroup="SCAN" CssClass="HiddenButton" OnClick="btnGetPlant_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                                <!--/Form with header-->
                            </div>
                            <!--col-sm-6-->
                        </div>
                        <!--col-sm-8-->
                    </asp:Panel>
                </div>
                <div style="width: 100%; text-align: center ; margin-top:20px" class="container">
                    <asp:Panel ID="pnlConCurrent" runat="server" Visible="False" Height="200px" Style="text-align: center;
                        width: 100%">
                       
                        <div class="alert alert-danger fade in">
                            <button type="button" class="close" data-dismiss="alert" aria-hidden="true">
                                ×</button>
                            <h3> ! Error !</h3>
                             <hr class="message-inner-separator">
                            <h4>User  has  already  logged  in  from  another  system</h4>
                            <p></p>
                            <p>
                                <asp:LinkButton ID="lnkForce_Login" runat="server" CssClass="btn btn-danger" Font-Size="20pt"
                                    Font-Bold="true" Width="150px" OnClick="lnkForce_Login_Click">Click here</asp:LinkButton>
                            </p>
                            <p></p>
                            <h4> to  access  system  by  this  ID  forcefully.</h4>
                        </div>
                    </asp:Panel>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnLogin" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
