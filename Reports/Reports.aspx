<%@ Page Title="" Language="C#" MasterPageFile="~/Modules/MasterPage.master" AutoEventWireup="true"
    CodeFile="Reports.aspx.cs" Inherits="Modules_Reports" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .hiddencol
        {
            display: none;
        }
        .calender
        {
            width: 400px;
        }
        .style4
        {
            width: 375px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="100%">
        <tr>
            <td style="width: 24px;">
                <asp:ImageButton ID="imgCloseV2" runat="server" Height="24px" ImageUrl="~/App_Themes/Styles/Images/exit.ico"
                    ToolTip="Close Page" Width="24px" PostBackUrl="~/Modules/frmMain.aspx" />
            </td>
            <td style="width: 100%; background-image: url('../App_Themes/Styles/Images/Form_Title.JPG')">
                <asp:Label ID="Label35" runat="server" CssClass="HeadingLabel" Text="Weighing Reports"></asp:Label>
            </td>
        </tr>
    </table>
    <table style=" padding:5px">
        <tr style="border: 1px solid black; border-collapse: collapse; padding: 5px;">
            <th colspan="6" style="background-image: url('../App_Themes/Styles/Images/Form_Title.JPG');
                height: 25px; width:600px ">
            </th>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblType" runat="server" CssClass="Label" Text=" Report Type:"></asp:Label>
            </td>
            <td style=" float:left">
                <asp:DropDownList ID="ddType" runat="server" CssClass="Combobox" Height="25px" Width="154px"
                    OnSelectedIndexChanged="ddType_SelectedIndexChanged" AutoPostBack="true" >
                    <asp:ListItem Value="0">Select</asp:ListItem>
                    <asp:ListItem Value="Daily">Daily</asp:ListItem>
                    <asp:ListItem Value="Montly">Monthly</asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValida6" runat="server" ValidationGroup="Post"
                    ControlToValidate="ddType" InitialValue="0">
                    <img src="../App_Themes/Styles/Images/err1.png" title="From date is required!!!" /></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr runat="server" id="rWeight">
            <td>
                <asp:Label ID="Label14" runat="server" CssClass="Label" Text="Weighing ID:"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtWeighingid" runat="server"  CssClass="TextBox"
                    Width="150px" style="margin-left:0px" ></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="Post"
                    ControlToValidate="txtWeighingid"><img src="../App_Themes/Styles/Images/err1.png" title="From date is required!!!" /></asp:RequiredFieldValidator>
            </td>
            <td >
            </td>
        </tr>
        <tr runat="server" id="rDate">
            <td>
                <asp:Label ID="Label4" runat="server" CssClass="Label" Text="From Date:"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtFDate" runat="server" CssClass="DateTimeText" Width="150px" AutoComplete="off"></asp:TextBox>
                <asp:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True" TargetControlID="txtFDate"
                    Format="dd-MMM-yyyy">
                </asp:CalendarExtender>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtFDate"
                    ErrorMessage="RequiredFieldValidator" ToolTip="To date is required" ValidationGroup="Post"><img 
                                                                                src="../App_Themes/Styles/Images/err1.png" title="From date is required!!!" />
                </asp:RequiredFieldValidator>
            </td>
            <td runat="server" id="tdTodate" class="style4">
                <table>
                    <tr >
                        <td>
                            <asp:Label ID="Label6" runat="server" CssClass="Label" Text="To Date:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtTdate" runat="server" CssClass="DateTimeText" Width="155px" AutoComplete="off"></asp:TextBox>
                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" TargetControlID="txtTdate"
                                Format="dd-MMM-yyyy">
                            </asp:CalendarExtender>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtTdate"
                                ErrorMessage="RequiredFieldValidator" ToolTip="To date is required" ValidationGroup="Post"><img 
                                src="../App_Themes/Styles/Images/err1.png" title="To date is required!!!" />
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
        <td></td>
            <td>
                <asp:Button ID="btnShow" runat="server" CausesValidation="true" CssClass="Button" Height="30px"
                    OnClick="btnShow_Click" Text="Show" ValidationGroup="Post" OnClientClick="document.forms[0].target = '_blank';" />
            </td>
        </tr>
        <tr>
            <td colspan="6" style="background-image: url('../App_Themes/Styles/Images/Accordion_Leave.png');
                height: 20px">
            </td>
        </tr>
    </table>
   
     <div>
        <rsweb:ReportViewer ID="ReportViewer1" runat="server">
        </rsweb:ReportViewer>
    </div>
</asp:Content>
