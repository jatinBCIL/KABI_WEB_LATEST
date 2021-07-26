<%@ Page Title="" Language="C#" MasterPageFile="~/Modules/MasterPage.master" AutoEventWireup="true"
    CodeFile="frmUserRole.aspx.cs" Inherits="Masters_frmUserRole" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../Styles/Style_Controls.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/Style_Design.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/Style_Grid.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/ex.css" rel="stylesheet" type="text/css">
    <style type="text/css">
        .TreeView
        {
            font-family: Arial;
            font-size: 12px;
            font-style: normal;
            height: 100%;
            width: 100%;
        }
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
        .style9
        {
            width: 273px;
        }
        .style10
        {
            width: 190px;
        }
        .style11
        {
            width: 207px;
        }
    </style>
    <script src="../Javascript/dw_tooltip_c.js" type="text/javascript"></script>
    <script type="text/javascript">

        dw_Tooltip.defaultProps = {
            //supportTouch: true, // set false by default
            hoverable: true
        }

        dw_Tooltip.content_vars = {
            L1: 'Field Name : Test' + '\n' + 'Field Name : Test',
            L3: 'Field Name : Test',
            L4: 'Field Name : Test',
            L5: 'Field Name : Test',
            L6: 'test'
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td style="height: 30px; background-image: url('../App_Themes/Styles/Images/Form_Title.JPG');
                            background-repeat: repeat-x">
                            <asp:Label ID="Label35" runat="server" CssClass="HeadingLabel" Text="&amp;nbsp;User Roles "></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td height="30" style="height: 30px; background-image: url('../App_Themes/Styles/Images/Menu_Out.png');
                            background-repeat: repeat-x">
                            <asp:MultiView ID="MultiView1" runat="server">
                                <asp:View ID="View1" runat="server">
                                    <table cellpadding="0" cellspacing="0" width="100%">
                                        <tr>
                                            <td height="30" style="height: 30px; background-image: url('../App_Themes/Styles/Images/Menu_Out.png');
                                                background-repeat: repeat-x">
                                                <table cellpadding="5">
                                                    <tr>
                                                        <td class="style22">
                                                            <asp:ImageButton ID="imgCloseV2" runat="server" Height="24px" ImageUrl="~/App_Themes/Styles/Images/exit.ico"
                                                                ToolTip="Close Page" Width="24px" OnClick="imgCloseV2_Click" PostBackUrl="~/Modules/frmMain.aspx" />
                                                        </td>
                                                        <td>
                                                            <asp:ImageButton ID="imgSave" runat="server" Height="24px" ImageUrl="~/App_Themes/Styles/Images/media_floppy_green.jpg"
                                                                ToolTip="Save Transaction" Width="24px" ValidationGroup="Save" OnClick="imgSave_Click" />
                                                        </td>
                                                        <td>
                                                            <asp:ImageButton ID="imgDetails" runat="server" Width="27px" Height="27px" ImageUrl="~/App_Themes/Styles/Images/imgDtl.jpg"
                                                                ToolTip="Click For Details" OnClick="imgDetails_Click" />
                                                        </td>
                                                        <td>
                                                            &nbsp;
                                                        </td>
                                                        <td>
                                                            &nbsp;
                                                        </td>
                                                        <td>
                                                            &nbsp;
                                                        </td>
                                                        <td>
                                                            &nbsp;
                                                        </td>
                                                        <td>
                                                            &nbsp;
                                                        </td>
                                                        <td>
                                                            &nbsp;
                                                        </td>
                                                        <td width="100%">
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:Panel ID="Panel2" runat="server" ScrollBars="Auto" Height="550px" Width="580px"
                                        BorderStyle="Solid" BorderWidth="1px">
                                        <table>
                                            <tr>
                                                <td class="style9">
                                                    &nbsp;
                                                </td>
                                                <td class="style10" colspan="2">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style9">
                                                    <asp:Label ID="Label5" runat="server" CssClass="Label" Text="Role Name :" Width="100px"></asp:Label>
                                                </td>
                                                <td class="style10" colspan="2">
                                                    <asp:TextBox ID="txtRole" runat="server" Width="257px"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:Image ID="img_Str" runat="server" Height="10px" ImageUrl="~/App_Themes/Styles/Images/img_mand.png"
                                                        title="Role is required!!!" ToolTip="Mandatory Field!!!" Width="10px" />
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtRole"
                                                        ErrorMessage="RequiredFieldValidator" ToolTip="Please Enter Role!!!" ValidationGroup="Save"><img src="../App_Themes/Styles/Images/err1.png" 
                                                title="Role is required!!!" /></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style9">
                                                    &nbsp;
                                                </td>
                                                <td class="style10" colspan="2">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style9">
                                                    <asp:Label ID="Label2" runat="server" CssClass="Label" Text="User Type :" Width="100px"></asp:Label>
                                                </td>
                                                <td class="style11">
                                                    <asp:DropDownList ID="ddUserType" CssClass="Combobox" runat="server" OnSelectedIndexChanged="ddUserType_SelectedIndexChanged"
                                                        AutoPostBack="true">
                                                        <asp:ListItem>Select</asp:ListItem>
                                                        <asp:ListItem>Administrator</asp:ListItem>
                                                        <asp:ListItem>User</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:Image ID="Image1" runat="server" Height="10px" ImageUrl="~/App_Themes/Styles/Images/img_mand.png"
                                                        title="User Type required!!!" ToolTip="Mandatory Field!!!" Width="10px" />
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" InitialValue="Select"
                                                        ControlToValidate="ddUserType" ErrorMessage="RequiredFieldValidator" ToolTip="Please Select User Type!!!"
                                                        ValidationGroup="Save"><img src="../App_Themes/Styles/Images/err1.png" 
                                                title="User Type is required!!!" /></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style9">
                                                    &nbsp;
                                                </td>
                                                <td class="style10" colspan="2">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style9">
                                                    <asp:Label ID="Label36" runat="server" CssClass="Label" Text="Transaction Type :"
                                                        Width="200px"></asp:Label>
                                                </td>
                                                <td class="style10" colspan="2">
                                                    <asp:DropDownList ID="ddTransactionType" runat="server" CssClass="Combobox" AutoPostBack="True"
                                                        OnSelectedIndexChanged="ddTransactionType_SelectedIndexChanged">
                                                        <asp:ListItem>Select</asp:ListItem>
                                                        <asp:ListItem>Transaction</asp:ListItem>
                                                        <asp:ListItem>Display</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                    <asp:Image ID="Image2" runat="server" Height="10px" ImageUrl="~/App_Themes/Styles/Images/img_mand.png"
                                                        title="Trasaction Type is Required!!!" ToolTip="Mandatory Field!!!" Width="10px" />
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" InitialValue="Select"
                                                        ControlToValidate="ddTransactionType" ErrorMessage="RequiredFieldValidator" ToolTip="Please Select Transaction Type!!!"
                                                        ValidationGroup="Save"><img src="../App_Themes/Styles/Images/err1.png" 
                                                title="Transaction Type is required!!!" /></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style9">
                                                    &nbsp;
                                                </td>
                                                <td class="style10" colspan="2">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style9">
                                                    <asp:Label ID="Label3" runat="server" CssClass="Label" Text="Status :" Width="100px"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:RadioButton ID="RBactivate" Checked="True" Text="Activate" runat="server" ValidationGroup="A"
                                                        GroupName="RDgrp" />&nbsp;&nbsp;
                                                    <asp:RadioButton ID="RBdeactivate" Text="Deactivate" runat="server" ValidationGroup="A"
                                                        GroupName="RDgrp" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style9">
                                                    &nbsp;
                                                </td>
                                                <td class="style10" colspan="2">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <table>
                                                <tr>
                                                    <td>
                                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                                    </td>
                                                    <td colspan="2">
                                                        <asp:Panel ID="Panel1" runat="server" BorderStyle="Solid" BorderWidth="1px" Height="300px"
                                                            ScrollBars="Auto" Width="530px">
                                                            <asp:TreeView ID="trUserRoles" runat="server" AutoPostBack="true" BorderColor="#814594"
                                                                class="tree well" CssClass="TreeView" Font-Italic="False" Font-Overline="False"
                                                                Font-Strikeout="False" ForeColor="#6D4E94" ImageSet="Msdn" OnTreeNodeCheckChanged="trUserRoles_TreeNodeCheckChanged"
                                                                ShowCheckBoxes="Leaf" ShowLines="True" Width="530px">
                                                            </asp:TreeView>
                                                        </asp:Panel>
                                                    </td>
                                                </tr>
                                            </table>
                    </tr>
                    </table> </asp:Panel> </asp:View>
                    <asp:View ID="View2" runat="server">
                        <table cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td height="30" style="height: 30px; background-image: url('../App_Themes/Styles/Images/Menu_Out.png');
                                    background-repeat: repeat-x">
                                    <table cellpadding="5">
                                        <tr>
                                            <td class="style22">
                                                <asp:ImageButton ID="imgCloseV1" runat="server" Height="24px" ImageUrl="~/App_Themes/Styles/Images/exit.ico"
                                                    ToolTip="Close Page" Width="24px" OnClick="imgCloseV1_Click" />
                                            </td>
                                            <td>
                                                <asp:ImageButton ID="imgAdd1" runat="server" Height="24px" ImageUrl="~/App_Themes/Styles/Images/window_new.png"
                                                    ToolTip="Add Transaction" Width="24px" OnClick="imgAdd1_Click" />
                                            </td>
                                            <td>
                                                <asp:ImageButton ID="imgExport" runat="server" Height="24px" ImageUrl="~/App_Themes/Styles/Images/excel.png"
                                                    ToolTip="Download data in excel format" Width="24px" OnClick="imgExport_Click" />
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td width="100%">
                                                &nbsp;
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td height="30" style="height: 30px;">
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblSearch" runat="server" CssClass="Label" Text="Search Criteria"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="cboSearch" runat="server" CssClass="Combobox">
                                                    <asp:ListItem Selected="True" Value="Roles">Role</asp:ListItem>
                                                    <asp:ListItem Value="UserType">User Type</asp:ListItem>
                                                    <asp:ListItem Value="TransType">Transaction Type</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="cboCriteria" runat="server" CssClass="Combobox" Width="120px">
                                                    <asp:ListItem Selected="True" Value="Equal to">Equal to</asp:ListItem>
                                                    <asp:ListItem Value="Not equal to">Not equal to</asp:ListItem>
                                                    <asp:ListItem Value="Contains">Contains</asp:ListItem>
                                                    <asp:ListItem Value="Start with">Start with</asp:ListItem>
                                                    <asp:ListItem Value="End with">End with</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtSearch" runat="server" CssClass="TextBox" ValidationGroup="Search"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:ImageButton ID="imgSearch" runat="server" ImageUrl="~/App_Themes/Styles/Images/search.png"
                                                    OnClick="imgSearch_Click" />
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 30px;">
                                    <asp:GridView ID="GrUserRole" runat="server" AllowPaging="True" AllowSorting="True"
                                        AutoGenerateColumns="False" CaptionAlign="Left" CellPadding="4" CssClass="GridViewStyle"
                                        EmptyDataText="There are no items to show in this view." PageSize="20" ShowFooter="True"
                                        OnRowDataBound="GrUserRole_RowDataBound" DataKeyNames="Refno" OnPageIndexChanging="GrUserRole_PageIndexChanging1"
                                        OnRowCommand="GrUserRole_RowCommand" OnSorting="GrUserRole_Sorting" Width="100%">
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="LinkButton1" CommandName="Select" Text="Select" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Roles" HeaderText="Role" SortExpression="Role" />
                                            <asp:BoundField DataField="UserType" HeaderText="User Type" SortExpression="UserType" />
                                            <asp:BoundField DataField="TransType" HeaderText="Transaction Type" SortExpression="TransType" />
                                            <asp:BoundField DataField="Status" HeaderText="Activate" SortExpression="Status" />
                                        </Columns>
                                        <PagerSettings Position="TopAndBottom" />
                                        <RowStyle CssClass="GridViewRowStyle" />
                                        <FooterStyle CssClass="GridViewFooterStyle" />
                                        <PagerStyle CssClass="GridViewPagerStyle" />
                                        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                                        <HeaderStyle CssClass="GridViewHeaderStyle" />
                                        <EditRowStyle CssClass="GridViewEditRowStyle" />
                                        <EmptyDataRowStyle CssClass="EmptyDataRowStyle" />
                                        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                    </asp:View>
                    </asp:MultiView> </td> </tr>
                </table>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="imgExport" />
                <asp:PostBackTrigger ControlID="trUserRoles" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>
