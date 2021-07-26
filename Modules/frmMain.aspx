<%@ Page Title="e-Track-Home " Language="C#" MasterPageFile="~/Modules/MasterPage.master" AutoEventWireup="true" CodeFile="frmMain.aspx.cs" Inherits="frmMain" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="../App_Themes/Styles/Style_Controls.css" rel="stylesheet" type="text/css" />
    <link href="../App_Themes/Styles/Style_Design.css" rel="stylesheet" type="text/css" />
    <link href="../App_Themes/Styles/Style_Grid.css" rel="stylesheet" type="text/css" />
    <link href="../App_Themes/Styles/Style_PopUp.css" rel="stylesheet" type="text/css" />
    <link href="../App_Themes/Styles/Menu3.css" rel="stylesheet" type="text/css" />
    <script src="../Masters/Javascript/JScript.js" type="text/javascript"></script>
    <script type="text/javascript">
        function load(url) {
          
            $('#DisableDiv').fadeIn('slow', .8);
            $('#DisableDiv').append('<div class="form-group" style="position: fixed; text-align: center; height: 100%; width: 100%; top:0; right: 0; left: 0; background-color: #DEE9F1  ;z-index: 9999999; opacity:0.8"  align="center"><img src="../App_Themes/Styles/Images/round.gif" style="padding: 10px;position:fixed;top:20%;left:40%;"  /></div>');
            document.getElementById("DisableDiv").style.visibility = 'visible';
           
            var req = new XMLHttpRequest();
            req.open("POST", url, true);

            req.onreadystatechange = function () {
                if (req.readyState == 4 && req.status == 200) {
                    
                    if (req.responseText) {

                        document.getElementById('DisableDiv').visible = false;
                    }
                }
            };
            request.send(vars);
        }
        
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
         <table width="100%" cellpadding="0" cellspacing="0" style="visibility: hidden">
           <tr>
                    <td style="height:30px; background-image: url('../App_Themes/Styles/Images/Form_Title.JPG'); background-repeat: repeat-x">
                        <asp:Label ID="Label35" runat="server" Font-Bold="True" Font-Italic="True" 
                            Font-Names="Calibri" Font-Size="14pt" Text="&amp;nbsp;My Dashboard" 
                            ForeColor="White"></asp:Label>
                    </td>
                </tr>
             <tr>
                 <td style="background-repeat: repeat-x; background-image: url('../App_Themes/Styles/Images/Menu_Out.png');" 
                     class="style4">
                     <table>
                         <tr>
                             <td>
                                 &nbsp;</td>
                             <td>
                                 <asp:Image ID="Image2" runat="server" Height="24px" 
                                     ImageUrl="~/App_Themes/Styles/Images/gtk_refresh.png" Width="24px" />
                             </td>
                             <td>
                                 &nbsp;</td>
                             <td>
                                 <asp:Image ID="Image4" runat="server" Height="24px" 
                                     ImageUrl="~/App_Themes/Styles/Images/excel.png" Width="24px" />
                             </td>
                             <td>
                                 &nbsp;</td>
                         </tr>
                     </table>
                     </td>
             </tr>
             <tr>
                 <td style="border: 1px solid #CCFF99; height:250px; padding-top: 5px; padding-left: 5px;" 
                     valign="top">
                         &nbsp;</td>
             </tr>
            <tr>
                <td>
                    
                </td>
            </tr>
        </table>
        <div id="DisableDiv"></div>
    </ContentTemplate>
</asp:UpdatePanel>
</asp:Content>

