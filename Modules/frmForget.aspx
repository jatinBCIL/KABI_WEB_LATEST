<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmForget.aspx.cs" Inherits="frmForget" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Barcode System | Version 1.0</title>
    <link href="../App_Themes/Styles/Style_Controls.css" rel="stylesheet" type="text/css" />
    <link href="../App_Themes/Styles/Style_Design.css" rel="stylesheet" type="text/css" />
    <link href="../App_Themes/Styles/Style_Grid.css" rel="stylesheet" type="text/css" />
    </head>
<body style="background-image:none; background-color:White; margin:0;font-family:Arial; font-size:12pt;">
    <form id="form1" runat="server">
    <div align="center">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager> 
        <table style="width: 538px">
            <tr>
                <td align="left">
                    &nbsp;
                </td>
            </tr>
            <tr>s
                <td align="left">
                    <asp:Image ID="Image1" runat="server" ImageUrl="~/App_Themes/Styles/Images/LoginMain.png"
                        Height="60px" Width="198px" />
                </td>
            </tr>
            <tr>
                <td align="left" height="5">
                    &nbsp;</td>
            </tr>
            <tr>
                <td align="left" valign="top" style="border: 1px ridge #C0C0C0;">
                    <table cellpadding="0" cellspacing="0" style="width: 546px">
                        <tr>
                            <td height="50px" style="background-image: url('App_Themes/Styles/Images/login_back.JPG');
                                background-repeat: repeat-x; "> <!--border-top-left-radius: 9px;border-top-right-radius: 9px;-->
                                <h1 style="margin: 0px; padding: 0px; font-size: 22px; font-weight: normal; color: rgb(255, 255, 255);
                                    font-family: Arial, Helvetica, sans-serif; font-style: normal; font-variant: normal;
                                    letter-spacing: normal; line-height: normal; orphans: auto; text-align: center;
                                    text-indent: 0px; text-transform: none; white-space: normal; widows: 1; word-spacing: 0px;
                                    -webkit-text-stroke-width: 0px;">
                                    Change My Password</h1>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <asp:Panel ID="Panel1" runat="server" Height="303px">
                                    <table cellpadding="5" cellspacing="10" 
    style="width: 545px">
                                        <tr>
                                            <td align="left">
                                                <table>
                                                    <tr>
                                                        <td colspan="2">
                                                            <div class="bold" 
                                                            
                                                                style="font-weight: bold; color: rgb(0, 66, 130); font-family: Arial, Helvetica, sans-serif;
                                                            font-size: 14px; font-style: normal; font-variant: normal; letter-spacing: normal;
                                                            line-height: normal; orphans: auto; text-align: start; text-indent: 0px; text-transform: none;
                                                            white-space: normal; widows: 1; word-spacing: 0px; -webkit-text-stroke-width: 0px;">
                                                                Enter Old Password</div>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td height="35">
                                                            <asp:TextBox ID="txtUserID" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                                            Width="250px" CssClass="TextBox" Font-Size="16pt" Height="25px" 
                                                                TextMode="Password"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            &nbsp;</td>
                                                    </tr>
                                                </table>
                                                <table>
                                                    <tr>
                                                        <td colspan="2">
                                                            &nbsp;
                                                        </td>
                                                        <td>
                                                            &nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            <div class="bold" 
                                                            
                                                                style="font-weight: bold; color: rgb(0, 66, 130); font-family: Arial, Helvetica, sans-serif;
                                                            font-size: 14px; font-style: normal; font-variant: normal; letter-spacing: normal;
                                                            line-height: normal; orphans: auto; text-align: start; text-indent: 0px; text-transform: none;
                                                            white-space: normal; widows: 1; word-spacing: 0px; -webkit-text-stroke-width: 0px;">
                                                                Enter New Password</div>
                                                        </td>
                                                        <td>
                                                            &nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td height="35">
                                                            <asp:TextBox ID="txtPassword" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                                            Width="250px" CssClass="TextBox" Font-Size="16pt" Height="25px" 
                                                            TextMode="Password"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            &nbsp;</td>
                                                        <td>
                                                            &nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            &nbsp;
                                                        </td>
                                                        <td>
                                                            &nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            <div class="bold" 
                                                                style="font-weight: bold; color: rgb(0, 66, 130); font-family: Arial, Helvetica, sans-serif;
                                                            font-size: 14px; font-style: normal; font-variant: normal; letter-spacing: normal;
                                                            line-height: normal; orphans: auto; text-align: start; text-indent: 0px; text-transform: none;
                                                            white-space: normal; widows: 1; word-spacing: 0px; -webkit-text-stroke-width: 0px;">
                                                                Enter Confirm Password</div>
                                                        </td>
                                                        <td>
                                                            &nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            <asp:TextBox ID="txtPassword0" runat="server" BorderStyle="Solid" 
                                                                BorderWidth="1px" CssClass="TextBox" Font-Size="16pt" Height="25px" 
                                                                TextMode="Password" Width="250px"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            &nbsp;</td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" colspan="0" rowspan="0">
                                                <asp:Button ID="Button1" runat="server" CssClass="Button" Height="25px" 
                                                    Text="Change Password" Width="150px" onclick="Button1_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="right" style="font-family: Arial; color: #999999; font-size: 10px;">
                    <hr />
                    Copyright @ 2015 Bar Code India Ltd
                </td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
