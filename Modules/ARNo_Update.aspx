<%@ Page Title="" Language="C#" MasterPageFile="~/Modules/MasterPage.master" AutoEventWireup="true" CodeFile="ARNo_Update.aspx.cs" Inherits="Modules_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="../App_Themes/Styles/Style_Controls.css" rel="stylesheet" type="text/css" />
    <link href="../App_Themes/Styles/Style_Design.css" rel="stylesheet" type="text/css" />
    <link href="../App_Themes/Styles/Style_Grid.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        div#tipDiv
        {
            font-size: 13px;
            line-height: 1.2;
            color: #3333CC;
            background-color: #33CCCC;
            border: 1px solid #667295;
            padding: 4px;
            width: 270px;
            font-family: calibri;
        }
        .style18
        {
            height: 46px;
        }
    </style>
    <script src="../Masters/Javascript/dw_tooltip_c.js" type="text/javascript"></script>
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td>
                        <asp:MultiView ID="MultiView1" runat="server">
                            <asp:View ID="View1" runat="server">
                                <table cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td height="30" style="height: 30px; background-image: url('../App_Themes/Styles/Images/Menu_Out.png');
                                            background-repeat: repeat-x">
                                            <asp:Label ID="Label35" runat="server" CssClass="HeadingLabel" Text="&amp;nbsp;ARNo Updation"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td id="DivHeader" runat="server">
                                            <table cellpadding="6">
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label1" runat="server" CssClass="Label" Text="Itemcode :"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtItemcode" runat="server" CssClass="TextBox" Height="25px" SkinID="NAME"
                                                            Width="180px"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="Label2" runat="server" CssClass="Label" Text="SapBatchNo :"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtSapbthno" runat="server" CssClass="TextBox" Height="25px" SkinID="NAME"
                                                             Width="180px"></asp:TextBox>
                                                    </td>
                                                    </tr>
                                                    <tr>
                                                    <td>
                                                        <asp:Label ID="Label4" runat="server" CssClass="Label" Text="Inspection Lot :"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtInspectionlot" runat="server" CssClass="TextBox" Height="25px" SkinID="NAME"
                                                             Width="180px"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="Label3" runat="server" CssClass="Label" Text="ARNO :"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtARNo" runat="server" CssClass="TextBox" Height="25px" SkinID="NAME"
                                                             Width="180px"></asp:TextBox>
                                                    </td>
                                                    
                                                    <td>
                                                        <asp:Button ID="btnUpdate" runat="server" CssClass="Button" Text="UPDATE" 
                                                            Width="100px" onclick="btnUpdate_Click"
                                                             />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </asp:View>
                        </asp:MultiView>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

