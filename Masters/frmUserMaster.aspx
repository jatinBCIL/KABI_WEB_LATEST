<%@ Page Title="" Language="C#" MasterPageFile="~/Modules/MasterPage.master" AutoEventWireup="true"
    CodeFile="frmUserMaster.aspx.cs" Inherits="frmUser_Master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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
        .style15
        {
            width: 12px;
        }
        .style18
        {
            height: 46px;
        }
        .style20
        {
            width: 133px;
            height: 46px;
        }
        .style21
        {
            width: 8px;
            height: 46px;
        }
        .style22
        {
            width: 32px;
        }
    </style>
    <script src="Javascript/JScript.js" type="text/javascript"></script>
    <script src="Javascript/dw_tooltip_c.js" type="text/javascript"></script>
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
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td style="height: 30px; background-image: url('../App_Themes/Styles/Images/Form_Title.JPG');
                        background-repeat: repeat-x">
                        <asp:Label ID="Label35" runat="server" CssClass="HeadingLabel" Text="&amp;nbsp;User Master"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:MultiView ID="MultiView1" runat="server">
                            <asp:View ID="View2" runat="server">
                                <table cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td height="30" style="height: 30px; background-image: url('../App_Themes/Styles/Images/Menu_Out.png');
                                            background-repeat: repeat-x">
                                            <table cellpadding="5">
                                                <tr>
                                                    <td>
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
                                                    <td class="style15">
                                                        &nbsp;
                                                    </td>
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                    <td class="style15">
                                                        &nbsp;
                                                    </td>
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                    <td width="100%">
                                                        &nbsp;
                                                    </td>
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                            </table>
                                            <table cellpadding="5">
                                                <tr>
                                                    <td>
                                                        <table width="100%">
                                                            <tr valign="top">
                                                                <td style="width: 40%;">
                                                                    <asp:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" EnableTheming="True"
                                                                        Height="1000px" Width="950px">
                                                                        <asp:TabPanel ID="TabPanel1" runat="server" HeaderText="TabPanel1">
                                                                            <HeaderTemplate>
                                                                                User Master
                                                                            </HeaderTemplate>
                                                                            <ContentTemplate>
                                                                                <table>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <asp:Label ID="Label3" runat="server" CssClass="Label" Text="User&amp;nbsp;ID"></asp:Label>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:TextBox ID="txtUserId" runat="server" CssClass="TextBox" ValidationGroup="Save"></asp:TextBox>
                                                                                        </td>
                                                                                        <td valign="top">
                                                                                            <asp:Image ID="Image2" runat="server" Height="10px" Width="10px" ToolTip="Mandatory Field!!!"
                                                                                                ImageUrl="../App_Themes/Styles/Images/img_mand.png" />
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtUserId"
                                                                                                ErrorMessage="User ID is required!!!" Text="&lt;img src='../App_Themes/Styles/Images/err1.png' title='User ID is required!!!' /&gt;"
                                                                                                ToolTip="User ID is required!!!" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                                                                        </td>
                                                                                        <td>
                                                                                        </td>
                                                                                        <td>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <asp:Label ID="Label5" runat="server" CssClass="Label" Text="Password"></asp:Label>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:TextBox ID="txtpwd" runat="server" CssClass="TextBox" ValidationGroup="Save"></asp:TextBox>
                                                                                        </td>
                                                                                        <td valign="top">
                                                                                            <asp:Image ID="Image4" runat="server" Height="10px" Width="10px" ToolTip="Mandatory Field!!!"
                                                                                                ImageUrl="../App_Themes/Styles/Images/img_mand.png" />
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtpwd"
                                                                                                ErrorMessage="Password is required!!!" Text="&lt;img src='../App_Themes/Styles/Images/err1.png' title='Password is required!!!' /&gt;"
                                                                                                ToolTip="Password is required!!!" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                                                                        </td>
                                                                                        <td>
                                                                                        </td>
                                                                                        <td>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <asp:Label ID="Label4" runat="server" CssClass="Label" Text="First&amp;nbsp;Name"></asp:Label>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:TextBox ID="txtFirstName" runat="server" CssClass="TextBox" ValidationGroup="Save"></asp:TextBox>
                                                                                        </td>
                                                                                        <td valign="top">
                                                                                            <asp:Image ID="Image3" runat="server" Height="10px" Width="10px" ToolTip="Mandatory Field!!!"
                                                                                                ImageUrl="../App_Themes/Styles/Images/img_mand.png" />
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:RequiredFieldValidator ID="rfvUname" runat="server" ControlToValidate="txtFirstName"
                                                                                                ErrorMessage="First Name is required!!!" Text="&lt;img src='../App_Themes/Styles/Images/err1.png' title='User Name is required!!!' /&gt;"
                                                                                                ToolTip="First Name is required!!!" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <asp:Label ID="Label19" runat="server" CssClass="Label" Text="Last&amp;nbsp;Name"></asp:Label>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:TextBox ID="txtLastName" runat="server" CssClass="TextBox" ValidationGroup="Save"></asp:TextBox>
                                                                                        </td>
                                                                                        <td valign="top">
                                                                                            <asp:Image ID="Image15" runat="server" Height="10px" Width="10px" ToolTip="Mandatory Field!!!"
                                                                                                ImageUrl="../App_Themes/Styles/Images/img_mand.png" />
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:RequiredFieldValidator ID="rfvLastname" runat="server" ControlToValidate="txtLastName"
                                                                                                ErrorMessage="Last Name is required!!!" Text="&lt;img src='../App_Themes/Styles/Images/err1.png' title='Last Name is required!!!' /&gt;"
                                                                                                ToolTip="Last Name is required!!!" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <asp:Label ID="Label1" runat="server" CssClass="Label" Text="EMP&amp;nbsp;ID"></asp:Label>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:TextBox ID="txtEmpId" runat="server" CssClass="TextBox" ValidationGroup="Save"></asp:TextBox>
                                                                                        </td>
                                                                                        <td valign="top">
                                                                                            <asp:Image ID="Image1" runat="server" Height="10px" Width="10px" ToolTip="Mandatory Field!!!"
                                                                                                ImageUrl="../App_Themes/Styles/Images/img_mand.png" />
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:RequiredFieldValidator ID="rfvEmpid" runat="server" ControlToValidate="txtEmpId"
                                                                                                ErrorMessage="Employee ID is required!!!" Text="&lt;img src='../App_Themes/Styles/Images/err1.png' title='Employee ID is required!!!' /&gt;"
                                                                                                ToolTip="Employee ID is required!!!" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <asp:Label ID="Label10" runat="server" CssClass="Label" Text="Email.&amp;nbsp;Address"></asp:Label>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:TextBox ID="txtEmpAdd" runat="server" CssClass="TextBox"></asp:TextBox>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" Text="&lt;img src='../App_Themes/Styles/Images/err1.png' title='Email Id not valid!!!' /&gt;"
                                                                                                ErrorMessage="Email ID Not Valid" ControlToValidate="txtEmpAdd" ValidationGroup="Save"
                                                                                                ValidationExpression="^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"></asp:RegularExpressionValidator>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <asp:Label ID="Label14" runat="server" CssClass="Label" Text="Plant"></asp:Label>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:DropDownList ID="ddPlantcode" runat="server" AutoPostBack="True" CssClass="Combobox"
                                                                                                Height="25px" Width="224px">
                                                                                            </asp:DropDownList>
                                                                                        </td>
                                                                                        <td valign="top">
                                                                                            <asp:Image ID="Image10" runat="server" Height="10px" Width="10px" ToolTip="Mandatory Field!!!"
                                                                                                ImageUrl="../App_Themes/Styles/Images/img_mand.png" />
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:RequiredFieldValidator ID="rfvPlantcode" runat="server" ControlToValidate="ddPlantcode"
                                                                                                InitialValue="Select" ErrorMessage="Plant Code is required!!!" Text="&lt;img src='../App_Themes/Styles/Images/err1.png' title='Plant Code is required!!!' /&gt;"
                                                                                                ToolTip="Plant Code is required!!!" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <asp:Label ID="Label17" runat="server" CssClass="Label" Text="Module"></asp:Label>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:DropDownList ID="ddModule" runat="server" AutoPostBack="True" CssClass="Combobox"
                                                                                                Height="25px" Width="224px">
                                                                                                <asp:ListItem>Select</asp:ListItem>
                                                                                                <asp:ListItem>Stores</asp:ListItem>
                                                                                                <asp:ListItem>Formulation</asp:ListItem>
                                                                                                <asp:ListItem>API</asp:ListItem>
                                                                                                <asp:ListItem>Quality</asp:ListItem>
                                                                                            </asp:DropDownList>
                                                                                        </td>
                                                                                        <td valign="top">
                                                                                            <asp:Image ID="Image12" runat="server" Height="10px" Width="10px" ToolTip="Mandatory Field!!!"
                                                                                                ImageUrl="../App_Themes/Styles/Images/img_mand.png" />
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:RequiredFieldValidator ID="rfvModule" runat="server" ControlToValidate="ddModule"
                                                                                                InitialValue="Select" ErrorMessage="Module is required!!!" Text="&lt;img src='../App_Themes/Styles/Images/err1.png' title='Module is required!!!' /&gt;"
                                                                                                ToolTip="Module is required!!!" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <asp:Label ID="Label18" runat="server" CssClass="Label" Text="Type"></asp:Label>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:DropDownList ID="ddType" runat="server" AutoPostBack="True" CssClass="Combobox"
                                                                                                Height="25px" Width="224px">
                                                                                                <asp:ListItem>Select</asp:ListItem>
                                                                                                <asp:ListItem>User</asp:ListItem>
                                                                                                <asp:ListItem>Administrator</asp:ListItem>
                                                                                            </asp:DropDownList>
                                                                                        </td>
                                                                                        <td valign="top">
                                                                                            <asp:Image ID="Image13" runat="server" Height="10px" Width="10px" ToolTip="Mandatory Field!!!"
                                                                                                ImageUrl="../App_Themes/Styles/Images/img_mand.png" />
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:RequiredFieldValidator ID="rfvType" runat="server" ControlToValidate="ddType"
                                                                                                InitialValue="Select" ErrorMessage="Type is required!!!" Text="&lt;img src='../App_Themes/Styles/Images/err1.png' title='Type is required!!!' /&gt;"
                                                                                                ToolTip="Type is required!!!" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <asp:Label ID="Label9" runat="server" CssClass="Label" Text="User&amp;nbsp;Status"></asp:Label>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:RadioButton ID="RBactivate" Checked="True" Text="Activate" runat="server" ValidationGroup="A"
                                                                                                GroupName="RDgrp" />&nbsp;&nbsp;
                                                                                            <asp:RadioButton ID="RBdeactivate" Text="De-Activate" runat="server" ValidationGroup="A"
                                                                                                GroupName="RDgrp" />
                                                                                        </td>
                                                                                        <td valign="top">
                                                                                            <asp:Image ID="Image14" runat="server" Height="10px" Width="10px" ToolTip="Mandatory Field!!!"
                                                                                                ImageUrl="../App_Themes/Styles/Images/img_mand.png" />
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td colspan="4" style="height: 10px; background-image: url('../App_Themes/Styles/Images/Form_Title.JPG');
                                                                                            background-repeat: repeat-x">
                                                                                            <asp:Label ID="Label22" runat="server" CssClass="HeadingLabel" Font-Size="15px" Text="&amp;nbsp;Import Excel"></asp:Label>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td colspan="4" class="style18">
                                                                                            <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                                                                                <ContentTemplate>
                                                                                                    <table>
                                                                                                        <tr>
                                                                                                            <td class="style21">
                                                                                                                &nbsp;&nbsp;
                                                                                                            </td>
                                                                                                            <td class="style18">
                                                                                                                <asp:Label ID="lblBrowse" runat="server" CssClass="Label" Text="Browse:"></asp:Label>
                                                                                                            </td>
                                                                                                            <td style="width: 300px">
                                                                                                                <asp:FileUpload ID="FlUpload" runat="server" />
                                                                                                            </td>
                                                                                                            <td>
                                                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="FlUpload"
                                                                                                                    ErrorMessage="User ID is required!!!" Text="&lt;img src='../App_Themes/Styles/Images/err1.png' title='User ID is required!!!' /&gt;"
                                                                                                                    ToolTip="User ID is required!!!" ValidationGroup="Import"></asp:RequiredFieldValidator>
                                                                                                            </td>
                                                                                                            <td style="width: 200px">
                                                                                                                <asp:Button ID="btnImp" runat="server" CssClass="Button" OnClick="btnImp_Click1"
                                                                                                                    Text="Import" Width="60px" ValidationGroup="Import" />
                                                                                                                <asp:Button ID="btnTemplate1" runat="server" CssClass="Button" Style="margin-left: 30px"
                                                                                                                    Text="Template" Width="100px" OnClick="btnTemplate1_Click" />
                                                                                                            </td>
                                                                                                            <td class="style18">
                                                                                                            </td>
                                                                                                    </table>
                                                                                                </ContentTemplate>
                                                                                                <Triggers>
                                                                                                    <asp:PostBackTrigger ControlID="btnImp" />
                                                                                                </Triggers>
                                                                                            </asp:UpdatePanel>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td colspan="4" style="height: 10px; background-image: url('../App_Themes/Styles/Images/Form_Title.JPG');
                                                                                            background-repeat: repeat-x">
                                                                                        </td>
                                                                                        <tr>
                                                                                            <td>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </tr>
                                                                                </table>
                                                                                <table>
                                                                                    <tr>
                                                                                        <td style="width: 50%;" valign="top">
                                                                                            <asp:Panel ID="pnlImport" runat="server" ScrollBars="Auto" Height="400px">
                                                                                                <asp:GridView ID="grPlant1" runat="server" AllowPaging="True" AllowSorting="True"
                                                                                                    AutoGenerateColumns="False" CaptionAlign="Left" CellPadding="4" CssClass="style22"
                                                                                                    EmptyDataText="There are no items to show in this view." Height="16px" PageSize="10"
                                                                                                    ShowFooter="True" Width="900px" OnPageIndexChanging="grPlant1_PageIndexChanging"
                                                                                                    OnRowDataBound="grPlant1_RowDataBound">
                                                                                                    <Columns>
                                                                                                        <asp:BoundField DataField="USER_ID" HeaderText="USER ID" SortExpression="USER ID" />
                                                                                                        <asp:BoundField DataField="FIRSTNAME" HeaderText="FIRST NAME" SortExpression="FIRST NAME" />
                                                                                                        <asp:BoundField DataField="LASTNAME" HeaderText="LAST NAME" SortExpression="LAST NAME" />
                                                                                                        <asp:BoundField DataField="EMP_ID" HeaderText="EMP ID" SortExpression="EMP ID" />
                                                                                                        <asp:BoundField DataField="EMAIL_ID" HeaderText="EMAIL" SortExpression="EMAIL" />
                                                                                                        <asp:BoundField DataField="PLANTCODE" HeaderText="PLANT" SortExpression="PLANT" />
                                                                                                        <asp:BoundField DataField="DEPARTMENT" HeaderText="DEPARTMENT" SortExpression="DEPARTMENT">
                                                                                                            <HeaderStyle Width="120px" />
                                                                                                        </asp:BoundField>
                                                                                                        <asp:BoundField DataField="MODULE" HeaderText="MODULE" SortExpression="MODULE" />
                                                                                                        <asp:BoundField DataField="USER_TYPE" HeaderText="TYPE" SortExpression="TYPE" />
                                                                                                        <asp:BoundField DataField="STATUS" HeaderText="TYPE" SortExpression="STATUS" />
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
                                                                                                &nbsp;&nbsp;&nbsp;&nbsp;
                                                                                                <asp:Button ID="btnSave" runat="server" CssClass="Button" OnClick="btnSave_Click"
                                                                                                    Text="Save" Width="60px" />
                                                                                                &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;
                                                                                                <asp:Button ID="btnCancel" runat="server" CssClass="Button" Text="Cancel" Width="60px"
                                                                                                    OnClick="btnCancel_Click" />
                                                                                            </asp:Panel>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </ContentTemplate>
                                                                        </asp:TabPanel>
                                                                        <asp:TabPanel ID="TabPanel4" runat="server" HeaderText="TabPanel3">
                                                                            <HeaderTemplate>
                                                                                Rights
                                                                            </HeaderTemplate>
                                                                            <ContentTemplate>
                                                                                <div style="width: 100%; font-family: Calibri; font-size: 12px">
                                                                                    <table>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <asp:Label ID="Label8" runat="server" CssClass="Label" Text="User ID:"></asp:Label>
                                                                                                <asp:DropDownList ID="ddlUserid" runat="server" CssClass="Combobox" AutoPostBack="true"
                                                                                                    OnSelectedIndexChanged="ddlUserid_SelectedIndexChanged1">
                                                                                                </asp:DropDownList>
                                                                                                <asp:Image ID="Image18" runat="server" Height="10px" Width="10px" ToolTip="Mandatory Field!!!"
                                                                                                    ImageUrl="../App_Themes/Styles/Images/img_mand.png" />
                                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="ddlUserid"
                                                                                                    InitialValue="Select" ErrorMessage="User ID is required!!!" Text="&lt;img src='../App_Themes/Styles/Images/err1.png' title='User ID required!!!' /&gt;"
                                                                                                    ToolTip="User ID required!!!" ValidationGroup="SaveRights"></asp:RequiredFieldValidator>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <table>
                                                                                                    <tr>
                                                                                                        <td style="height: 10px;">
                                                                                                            <div id="grdCharges" runat="server" style="border: 1px solid #999999; width: 500px;
                                                                                                                overflow: auto; height: 150px;">
                                                                                                                <asp:GridView ID="grdUserDtls" runat="server" AllowPaging="True" AllowSorting="True"
                                                                                                                    AutoGenerateColumns="False" CaptionAlign="Left" CellPadding="4" CssClass="GridViewStyle"
                                                                                                                    EmptyDataText="There are no items to show in this view." PageSize="20" ShowFooter="True">
                                                                                                                    <Columns>
                                                                                                                        <asp:BoundField DataField="USER_ID" HeaderText="User ID" SortExpression="USER_ID" />
                                                                                                                        <asp:BoundField DataField="FIRSTNAME" HeaderText="First Name" SortExpression="FIRSTNAME" />
                                                                                                                        <asp:BoundField DataField="LASTNAME" HeaderText="Last Name" SortExpression="LASTNAME" />
                                                                                                                        <asp:BoundField DataField="EMP_ID" HeaderText="Emp Id" SortExpression="EMP_ID" />
                                                                                                                        <asp:BoundField DataField="DEPARTMENT" HeaderText="Department" SortExpression="DEPARTMENT" />
                                                                                                                        <asp:BoundField DataField="PLANTCODE" HeaderText="Plant" SortExpression="PLANTCODE" />
                                                                                                                        <asp:BoundField DataField="EMAIL_ADDRESS" HeaderText="Email Address" SortExpression="EMAIL_ADDRESS">
                                                                                                                            <HeaderStyle Width="120px" />
                                                                                                                        </asp:BoundField>
                                                                                                                        <asp:BoundField DataField="USER_TYPE" HeaderText="User Type" SortExpression="USER_TYPE" />
                                                                                                                        <asp:BoundField DataField="MODULE" HeaderText="Module" SortExpression="MODULE" />
                                                                                                                        <asp:BoundField DataField="STATUS" HeaderText="Activate" SortExpression="STATUS" />
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
                                                                                                            </div>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </table>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <div style="border: 2px solid black">
                                                                                                    <table>
                                                                                                        <tr>
                                                                                                            <td align="center" colspan="4">
                                                                                                                <h3>
                                                                                                                    <asp:Label ID="Label11" runat="server" Font-Size="15px" Text="Transaction"></asp:Label></h3>
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                        <tr>
                                                                                                            <td>
                                                                                                                <asp:Label ID="Label2" runat="server" CssClass="Label" Text="Plant Id:"></asp:Label>
                                                                                                            </td>
                                                                                                            <td>
                                                                                                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                                                                                    <ContentTemplate>
                                                                                                                        <asp:TextBox ID="txtTplant" ReadOnly="true" runat="server" ValidationGroup="Save"></asp:TextBox>
                                                                                                                        <asp:Button ID="btnTPlant" runat="server" Text="Select" CssClass="Button" Width="80px" />
                                                                                                                        <cc1:ModalPopupExtender ID="mp1" runat="server" PopupControlID="pnlPlant1" TargetControlID="btnTPlant"
                                                                                                                            CancelControlID="btnTPlant_Cancel" BackgroundCssClass="modalBackground">
                                                                                                                        </cc1:ModalPopupExtender>
                                                                                                                        <asp:Panel ID="pnlPlant1" runat="server" CssClass="modalPopup" ScrollBars="Auto"
                                                                                                                            Height="500px" align="center" Style="display: none" Width="500px">
                                                                                                                            <table class="Popup_Table">
                                                                                                                                <tr>
                                                                                                                                    <td colspan="2">
                                                                                                                                        <asp:CheckBoxList ID="cblTPlant" runat="server" CssClass="CheckList" Width="300px">
                                                                                                                                        </asp:CheckBoxList>
                                                                                                                                    </td>
                                                                                                                                    <td>
                                                                                                                                        <%--  <asp:CheckBoxList ID="cbTAssociatePlant" runat="server" CssClass="CheckList" Width="300px">
                                                                                                                                        </asp:CheckBoxList>--%>
                                                                                                                                    </td>
                                                                                                                                </tr>
                                                                                                                                <tr>
                                                                                                                                    <td>
                                                                                                                                        &nbsp
                                                                                                                                    </td>
                                                                                                                                    <td class="style4">
                                                                                                                                        <asp:Button ID="btnTPlantOk" runat="server" CssClass="Button" Text="OK" UseSubmitBehavior="False"
                                                                                                                                            OnClick="btnTPlantOk_Click" />
                                                                                                                                    </td>
                                                                                                                                    <td>
                                                                                                                                        <asp:Button ID="btnTPlant_Cancel" runat="server" CssClass="Button" Text="Cancel"
                                                                                                                                            OnClick="btnTPlant_Cancel_Click" UseSubmitBehavior="False" />
                                                                                                                                    </td>
                                                                                                                                </tr>
                                                                                                                            </table>
                                                                                                                        </asp:Panel>
                                                                                                                    </ContentTemplate>
                                                                                                                    <Triggers>
                                                                                                                        <asp:AsyncPostBackTrigger ControlID="btnTPlantOk" EventName="Click" />
                                                                                                                        <asp:AsyncPostBackTrigger EventName="Click" ControlID="btnTPlant_Cancel" />
                                                                                                                    </Triggers>
                                                                                                                </asp:UpdatePanel>
                                                                                                            </td>
                                                                                                            <td valign="top">
                                                                                                                <%--  <asp:Image ID="Image5" runat="server" Height="10px" Width="10px" ToolTip="Mandatory Field!!!"
                                                                                                                    ImageUrl="../App_Themes/Styles/Images/img_mand.png" />--%>
                                                                                                            </td>
                                                                                                            <td>
                                                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtTplant"
                                                                                                                    ErrorMessage="Plant is required!!!" Text="&lt;img src='../App_Themes/Styles/Images/err1.png' title='Plant is required!!!' /&gt;"
                                                                                                                    ToolTip="Plant is required!!!" ValidationGroup="SaveRights"></asp:RequiredFieldValidator>
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                        <tr>
                                                                                                            <td>
                                                                                                                <asp:Label ID="Label6" runat="server" CssClass="Label" Text="Role:"></asp:Label>
                                                                                                            </td>
                                                                                                            <td>
                                                                                                                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                                                                                                    <ContentTemplate>
                                                                                                                        <asp:TextBox ID="txtTRole" ReadOnly="true" runat="server" ValidationGroup="Save"></asp:TextBox>
                                                                                                                        <%--<asp:Button ID="btnTrole" runat="server" Text="Select" CssClass="Button" Width="80px"
                                                                                                                           />--%>
                                                                                                                        <asp:Button ID="btnTrole" runat="server" Text="Select" CssClass="Button" Width="80px" />
                                                                                                                        <cc1:ModalPopupExtender ID="ModalPopupExtender2" runat="server" PopupControlID="Panel3"
                                                                                                                            TargetControlID="btnTrole" CancelControlID="btnTRoleClose" BackgroundCssClass="modalBackground">
                                                                                                                        </cc1:ModalPopupExtender>
                                                                                                                        <asp:Panel ID="Panel3" runat="server" CssClass="modalPopup" ScrollBars="Auto" Height="500px"
                                                                                                                            Width="500px" align="center" Style="display: none">
                                                                                                                            <table class="Popup_Table">
                                                                                                                                <tr>
                                                                                                                                    <td colspan="2">
                                                                                                                                        <asp:CheckBoxList ID="cblTRole" runat="server" CssClass="CheckList" Width="300px">
                                                                                                                                            <asp:ListItem>Admin</asp:ListItem>
                                                                                                                                            <asp:ListItem>User</asp:ListItem>
                                                                                                                                            <asp:ListItem>SuperUser</asp:ListItem>
                                                                                                                                        </asp:CheckBoxList>
                                                                                                                                    </td>
                                                                                                                                </tr>
                                                                                                                                <tr>
                                                                                                                                    <td>
                                                                                                                                        &nbsp
                                                                                                                                    </td>
                                                                                                                                    <td class="style4">
                                                                                                                                        <asp:Button ID="btnTRoleOk" runat="server" CssClass="Button" Text="OK" UseSubmitBehavior="false"
                                                                                                                                            OnClick="btnTRoleOk_Click" />
                                                                                                                                    </td>
                                                                                                                                    <td>
                                                                                                                                        <asp:Button ID="btnTRoleClose" runat="server" CssClass="Button" Text="Cancel" UseSubmitBehavior="false"
                                                                                                                                            OnClick="btnTRoleCancel_Click" />
                                                                                                                                    </td>
                                                                                                                                </tr>
                                                                                                                            </table>
                                                                                                                        </asp:Panel>
                                                                                                                    </ContentTemplate>
                                                                                                                    <Triggers>
                                                                                                                        <asp:AsyncPostBackTrigger ControlID="btnTRoleOk" EventName="Click" />
                                                                                                                        <asp:AsyncPostBackTrigger EventName="Click" ControlID="btnTRoleClose" />
                                                                                                                    </Triggers>
                                                                                                                </asp:UpdatePanel>
                                                                                                            </td>
                                                                                                            <td valign="top">
                                                                                                                <%--<asp:Image ID="Image6" runat="server" Height="10px" Width="10px" ToolTip="Mandatory Field!!!"
                                                                                                                    ImageUrl="../App_Themes/Styles/Images/img_mand.png" />--%>
                                                                                                            </td>
                                                                                                            <td>
                                                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtTRole"
                                                                                                                    ErrorMessage="Role is required!!!" Text="&lt;img src='../App_Themes/Styles/Images/err1.png' title='Role is required!!!' /&gt;"
                                                                                                                    ToolTip="Role is required!!!" ValidationGroup="SaveRights"></asp:RequiredFieldValidator>
                                                                                                            </td>
                                                                                                            <%--For Display--%>
                                                                                                            <%-- Close Display--%>
                                                                                                        </tr>
                                                                                                        <tr>
                                                                                                            <td>
                                                                                                                <asp:Label ID="Label21" runat="server" CssClass="Label" Text="Access Upto:"></asp:Label>
                                                                                                            </td>
                                                                                                            <td>
                                                                                                                <asp:TextBox ID="txtTDate" runat="server" CssClass="DateTimeText"></asp:TextBox>
                                                                                                                <asp:CalendarExtender ID="CalendarExtender4" runat="server" Enabled="True" TargetControlID="txtTDate"
                                                                                                                    Format="dd/MM/yyyy">
                                                                                                                </asp:CalendarExtender>
                                                                                                            </td>
                                                                                                            <td valign="top">
                                                                                                                <%-- <asp:Image ID="Image16" runat="server" Height="10px" Width="10px" ToolTip="Mandatory Field!!!"
                                                                                                                    ImageUrl="../App_Themes/Styles/Images/img_mand.png" />--%>
                                                                                                            </td>
                                                                                                            <td>
                                                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtTDate"
                                                                                                                    ErrorMessage="Access Upto Date is required!!!" Text="&lt;img src='../App_Themes/Styles/Images/err1.png' title='Access Upto Date is required!!!' /&gt;"
                                                                                                                    ToolTip="Access Upto Date is required!!!" ValidationGroup="SaveRights"></asp:RequiredFieldValidator>
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                    </table>
                                                                                                </div>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <asp:Button ID="btnSaveRight" runat="server" CssClass="Button" Text="Save" UseSubmitBehavior="False"
                                                                                                    Visible="true" OnClick="btnSaveRight_Click1" />
                                                                                                <asp:Button ID="btnCancelRight" runat="server" CssClass="Button" Text="Cancel" UseSubmitBehavior="False"
                                                                                                    Visible="true" OnClick="btnCancelRight_Click" />
                                                                                            </td>
                                                                                        </tr>
                                                                                        <td colspan="4" style="height: 10px; background-image: url('../App_Themes/Styles/Images/Form_Title.JPG');
                                                                                            background-repeat: repeat-x">
                                                                                            <asp:Label ID="Label24" runat="server" CssClass="HeadingLabel" Font-Size="15px" Text="&amp;nbsp;Import Excel"></asp:Label>
                                                                                        </td>
                                                                                        <tr>
                                                                                            <td colspan="4" class="style18">
                                                                                                <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                                                                                                    <ContentTemplate>
                                                                                                        <table>
                                                                                                            <tr>
                                                                                                                <td class="style21">
                                                                                                                    &nbsp;&nbsp;
                                                                                                                </td>
                                                                                                                <td class="style18">
                                                                                                                    <asp:Label ID="Label23" runat="server" CssClass="Label" Text="Browse:"></asp:Label>
                                                                                                                </td>
                                                                                                                <td style="width: 300px">
                                                                                                                    <asp:FileUpload ID="uploadRight" runat="server" />
                                                                                                                </td>
                                                                                                                <td>
                                                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="uploadRight"
                                                                                                                        ErrorMessage="User ID is required!!!" Text="&lt;img src='../App_Themes/Styles/Images/err1.png' title='User ID is required!!!' /&gt;"
                                                                                                                        ToolTip="User ID is required!!!" ValidationGroup="Import1"></asp:RequiredFieldValidator>
                                                                                                                </td>
                                                                                                                <td style="width: 200px">
                                                                                                                    <asp:Button ID="btnImpRight" runat="server" CssClass="Button" OnClick="btnImpRight_Click"
                                                                                                                        Text="Import" Width="60px" ValidationGroup="Import1" />
                                                                                                                    <asp:Button ID="btnTemplate" runat="server" CssClass="Button" Style="margin-left: 30px"
                                                                                                                        Text="Template" Width="100px" OnClick="btnTemplateUserRight_Click" />
                                                                                                                </td>
                                                                                                                <td class="style18">
                                                                                                                </td>
                                                                                                        </table>
                                                                                                    </ContentTemplate>
                                                                                                    <Triggers>
                                                                                                        <asp:PostBackTrigger ControlID="btnImpRight" />
                                                                                                    </Triggers>
                                                                                                </asp:UpdatePanel>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td colspan="4" style="height: 10px; background-image: url('../App_Themes/Styles/Images/Form_Title.JPG');
                                                                                                background-repeat: repeat-x">
                                                                                                <asp:Label ID="Label25" runat="server" CssClass="HeadingLabel" Font-Size="15px" Text="&amp;nbsp;"></asp:Label>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <table>
                                                                                            <tr>
                                                                                                <td style="width: 50%;" valign="top">
                                                                                                    <asp:Panel ID="Panel1" runat="server" ScrollBars="Auto" Height="400px">
                                                                                                        <asp:GridView ID="grdExcelRight" runat="server" AllowPaging="True" AllowSorting="True"
                                                                                                            AutoGenerateColumns="False" CaptionAlign="Left" CellPadding="4" CssClass="style22"
                                                                                                            EmptyDataText="There are no items to show in this view." Height="16px" PageSize="10"
                                                                                                            ShowFooter="True" Width="900px" OnPageIndexChanging="grdExcelRight_PageIndexChanging"
                                                                                                            OnRowDataBound="grdExcelRight_RowDataBound">
                                                                                                            <Columns>
                                                                                                                <asp:BoundField DataField="USER_ID" HeaderText="USER ID" SortExpression="USER_ID" />
                                                                                                                <asp:BoundField DataField="T_PLANTID" HeaderText="PLANT ID" SortExpression="T_PLANTID" />
                                                                                                                <asp:BoundField DataField="T_DEPARTMENT" HeaderText="DEPARTMENT TRAN" SortExpression="T_DEPARTMENT" />
                                                                                                                <asp:BoundField DataField="T_ACCESSUPTO" HeaderText="ACCESS  UPTO TRAN" SortExpression="T_ACCESSUPTO" />
                                                                                                                <asp:BoundField DataField="D_PLANTID" HeaderText="PLANT ID DISPLAY" SortExpression="D_PLANTID" />
                                                                                                                <asp:BoundField DataField="D_DEPARTMENT" HeaderText="DEPARTMENT DISPLAY" SortExpression="D_DEPARTMENT" />
                                                                                                                <asp:BoundField DataField="D_ACCESSUPTO" HeaderText="ACCESS UPTO DISPLAY" SortExpression="D_ACCESSUPTO">
                                                                                                                    <HeaderStyle Width="120px" />
                                                                                                                </asp:BoundField>
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
                                                                                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                                                                                        <asp:Button ID="btnSaveRightExcel" runat="server" CssClass="Button" OnClick="btnSaveRightExcel_Click"
                                                                                                            Text="Save" Width="60px" />
                                                                                                        &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;
                                                                                                        <asp:Button ID="btnCancelRightExcel" runat="server" CssClass="Button" Text="Cancel"
                                                                                                            Width="60px" OnClick="btnCancelRightExcel_Click" />
                                                                                                    </asp:Panel>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </table>
                                                                                </div>
                                                                            </ContentTemplate>
                                                                        </asp:TabPanel>
                                                                    </asp:TabContainer>
                                                                </td>
                                                                <td style="width: 60%">
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 30px;">
                                            &nbsp;
                                        </td>
                                    </tr>
                                </table>
                            </asp:View>
                            <asp:View ID="View1" runat="server">
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
                                            <asp:GridView ID="GrUser" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False"
                                                CaptionAlign="Left" CellPadding="4" CssClass="GridViewStyle" EmptyDataText="There are no items to show in this view."
                                                PageSize="20" ShowFooter="True" OnRowDataBound="GrUser_RowDataBound" DataKeyNames="RECNO"
                                                OnPageIndexChanging="GrUser_PageIndexChanging" OnRowCommand="GrUser_RowCommand"
                                                OnSorting="GrUser_Sorting" Width="100%">
                                                <Columns>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="LinkButton1" CommandName="Select" Text="Select" runat="server" />
                                                            <asp:Button ID="btnUnlock" CommandName="Unlock" Text="Unlock" runat="server" CssClass="Button" Width="80px" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="USER_ID" HeaderText="USER ID" SortExpression="USER_ID" />
                                                    <asp:BoundField DataField="USER_NAME" HeaderText="USERNAME" SortExpression="USER_NAME" />
                                                    <asp:BoundField DataField="FIRSTNAME" HeaderText="FIRST NAME" SortExpression="FIRSTNAME" />
                                                    <asp:BoundField DataField="LASTNAME" HeaderText="LAST NAME" SortExpression="LASTNAME" />
                                                    <asp:BoundField DataField="EMP_ID" HeaderText="EMPLOYEE ID" SortExpression="EMP_ID" />
                                                    <asp:BoundField DataField="EMAIL_ADDRESS" HeaderText="EMAIL ADDRESS" SortExpression="EMAIL_ADDRESS" />
                                                    <asp:BoundField DataField="PLANTCODE" HeaderText="PLANT CODE" SortExpression="PLANTCODE" />
                                                    <%--<asp:BoundField DataField="DEPARTMENT" HeaderText="DEPARTMENT" SortExpression="DEPARTMENT" />--%>
                                                    <asp:BoundField DataField="AUTH_PLANT" HeaderText="AUTHORISED PLANTS" SortExpression="AUTH_PLANT" />
                                                    <%--<asp:BoundField DataField="AUTH_DEPT" HeaderText="AUTHORISED DEPARTMENTS" SortExpression="AUTH_DEPT" />
                                                    <asp:BoundField DataField="MODULE" HeaderText="MODULE" SortExpression="MODULE" />--%>
                                                    <asp:BoundField DataField="USER_TYPE" HeaderText="USER TYPE" SortExpression="USER_TYPE" />
                                                    <asp:BoundField DataField="STATUS" HeaderText="STATUS" SortExpression="STATUS" />
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
                        </asp:MultiView>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="imgExport" />
            <asp:PostBackTrigger ControlID="imgSave" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
