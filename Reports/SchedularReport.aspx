<%@ Page Title="" Language="C#" MasterPageFile="~/Modules/MasterPage.master" AutoEventWireup="true" CodeFile="SchedularReport.aspx.cs" Inherits="Reports_SchedularReport" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<table width="100%">
        <tr>
            <td style="width: 24px;">
                <asp:ImageButton ID="imgCloseV2" runat="server" Height="24px" ImageUrl="~/App_Themes/Styles/Images/exit.ico"
                    ToolTip="Close Page" Width="24px" PostBackUrl="~/Modules/frmMain.aspx" />
            </td>
            <td style="width: 100%; background-image: url('../App_Themes/Styles/Images/Form_Title.JPG')">
                <asp:Label ID="Label35" runat="server" CssClass="HeadingLabel" Text="Schedular Reports"></asp:Label>
            </td>
        </tr>
    </table>
    <table align="left">
    <tr>
        <td>To Date</td>
        <td><asp:TextBox ID="txtToDate" runat="server"></asp:TextBox>
                            
                            <asp:CalendarExtender ID="CalendarExtender7" runat="server" Enabled="True" TargetControlID="txtToDate"
                             Format="yyyy-MM-dd">
                            </asp:CalendarExtender>


                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtToDate"
                                ErrorMessage="RequiredFieldValidator" ToolTip="To date is required" ValidationGroup="Post"><img 
                                src="../App_Themes/Styles/Images/err1.png" title="To date is required!!!" />
                            </asp:RequiredFieldValidator>
        
        </td>
        <td> From Date</td>
        <td><asp:TextBox ID="txtFromDate" runat="server"></asp:TextBox>
                          
                          <asp:CalendarExtender ID="CalendarExtender6" runat="server" Enabled="True" TargetControlID="txtFromDate"  
                             Format="yyyy-MM-dd">
                            </asp:CalendarExtender>


                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtFromDate"
                                ErrorMessage="RequiredFieldValidator" ToolTip="To date is required" ValidationGroup="Post"><img 
                                src="../App_Themes/Styles/Images/err1.png" title="From date is required!!!" />
                            </asp:RequiredFieldValidator>
        
        </td>
        <td>Select Batch</td>
        <td>
          
          <asp:TextBox ID="txtBatch" runat="server"></asp:TextBox>
            <%--<asp:DropDownList ID="dwnBatch" runat="server" AppendDataBoundItems="true">
                 <asp:ListItem Value="" Text="Select Batch" />
            </asp:DropDownList>--%>
        </td>
        
    </tr>
    <tr>
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
<br />
    <br />
    <br />
    <br />
    <br />
    <br />
<table align="left">
    
    <asp:GridView ID="GridView1" runat="server" HeaderStyle-BackColor="Blue" HeaderStyle-ForeColor="White" PagerSettings-Mode="Numeric"  PageButtonCount="2">
    </asp:GridView>
</table >


</asp:Content>

