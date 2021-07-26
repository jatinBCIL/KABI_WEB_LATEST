<%@ Page Title="" Language="C#" MasterPageFile="~/Modules/MasterPage.master" AutoEventWireup="true" CodeFile="frmDepartmentMaster.aspx.cs" Inherits="frmDepartmentMaster" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../App_Themes/Styles/Style_Controls.css" rel="stylesheet" type="text/css" />
    <link href="../App_Themes/Styles/Style_Design.css" rel="stylesheet" type="text/css" />
    <link href="../App_Themes/Styles/Style_Grid.css" rel="stylesheet" type="text/css" />
    <link href="../App_Themes/Styles/Style_PopUp.css" rel="stylesheet" type="text/css" />
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
        .style19
        {
            height: 24px;
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
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td style="height: 30px; background-image: url('../App_Themes/Styles/Images/Form_Title.JPG');
                        background-repeat: repeat-x">
                        <asp:Label ID="Label35" runat="server" CssClass="HeadingLabel" Text="&amp;nbsp;Department Master"></asp:Label>
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
                                                                        Height="400px" Width="500px">
                                                                        <asp:TabPanel ID="TabPanel1" runat="server" HeaderText="TabPanel1">
                                                                            <HeaderTemplate>
                                                                                General</HeaderTemplate>
                                                                            <ContentTemplate>
                                                                                <table style="width: 421px; height: 179px">
                                                                                    <tr>
                                                                                        <td colspan="4" style="height: 10px; background-image: url('../App_Themes/Styles/Images/Form_Title.JPG');
                                                                                            background-repeat: repeat-x">
                                                                                            <asp:Label ID="Label5" runat="server" CssClass="HeadingLabel" Font-Size="15px" Text="&amp;nbsp;Import Excel"></asp:Label>
                                                                                        </td>
                                                                                    </tr>
                                                                                    </tr><tr>
                                                                                        <td colspan="4" class="style18">
                                                                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                                                                <ContentTemplate>
                                                                                                    <table>
                                                                                                        <tr>
                                                                                                            <td>
                                                                                                                <asp:Label ID="lblBrowse" runat="server" CssClass="Label" Text="Browse:"></asp:Label>
                                                                                                            </td>
                                                                                                            <td>
                                                                                                                <asp:FileUpload ID="FlUpload" runat="server" />
                                                                                                            </td>
                                                                                                            
                                                                                                            <td>
                                                                                                                <asp:Button ID="btnImp" runat="server" CssClass="Button" OnClick="btnImp_Click" Text="Import"
                                                                                                                    Width="60px" />
                                                                                                            </td>
                                                                                                            <td><asp:Button ID="btnTemplate" runat="server" CssClass="Button" Style="margin-left: 10px"
                                                                                                                    Text="Template" Width="70px" OnClick="btnTemplate_Click" />
                                                                                                                    </td>
                                                                                                            
                                                                                                        </tr>
                                                                                                    </table>
                                                                                                </ContentTemplate>
                                                                                                <Triggers>
                                                                                                    <asp:PostBackTrigger ControlID="btnImp" />
                                                                                                </Triggers>
                                                                                            </asp:UpdatePanel>
                                                                                        </td>
                                                                                        <tr>
                                                                                            <td colspan="4" style="height: 10px; background-image: url('../App_Themes/Styles/Images/Form_Title.JPG');
                                                                                                background-repeat: repeat-x">
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td class="style19" colspan="2">
                                                                                                <asp:Label ID="lblItemCode14" runat="server" CssClass="Label" Text="Plant Code :"></asp:Label>
                                                                                            </td>
                                                                                            <td class="style19" colspan="2">
                                                                                                <asp:TextBox ID="txtPlant" runat="server" ReadOnly="True" Height="20px" 
                                                                                                    Width="200px"></asp:TextBox>
                                                                                                  
                                                                                                <asp:Image ID="Image1" runat="server" Height="10px" ImageUrl="~/App_Themes/Styles/Images/img_mand.png"
                                                                                                    title="Plant Name is required!!!" ToolTip="Mandatory Field!!!" Width="10px" />
                                                                                                   <asp:RequiredFieldValidator
                                                                                                        ID="rvPlant" runat="server" ControlToValidate="txtPlant" ErrorMessage="Select Plant"
                                                                                                        ToolTip="Plant Name is required!!!" ValidationGroup="Save"><img 
                                                                                                         src="../App_Themes/Styles/Images/err1.png" title="Plant Name is required!!!" />
                                                                                                   </asp:RequiredFieldValidator>
                                                                                                <asp:PopupControlExtender ID="dd"  runat="server" Enabled="True"
                                                                                                    ExtenderControlID="" TargetControlID="txtPlant"  PopupControlID="pnlPlant" OffsetY="30"
                                                                                                    DynamicServicePath="" >
                                                                                                </asp:PopupControlExtender>
                                                                                                <asp:Panel ID="pnlPlant" runat="server" ScrollBars="Auto" CssClass="modalPopup" Height="116px"
                                                                                                    Width="213px" Style="display: none">
                                                                                                    <table>
                                                                                                        <tr valign="baseline">
                                                                                                            <td colspan="2">
                                                                                                                <asp:CheckBoxList ID="chkPlants" runat="server" AutoPostBack="True" OnSelectedIndexChanged="chkPlants_SelectedIndexChanged">
                                                                                                                </asp:CheckBoxList>
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                    </table>
                                                                                                </asp:Panel>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td colspan="2">
                                                                                                <asp:Label ID="lblItemCode13" runat="server" CssClass="Label" Text="Department ID :"></asp:Label>
                                                                                            </td>
                                                                                            <td colspan="2">
                                                                                                <asp:TextBox ID="txtDept_ID" runat="server" CssClass="TextBox" Height="25px" MaxLength="500"
                                                                                                    Width="213px" ReadOnly="True"></asp:TextBox><asp:Image ID="img_Str0" runat="server"
                                                                                                        Height="10px" ImageUrl="~/App_Themes/Styles/Images/img_mand.png" title="Department Code is required!!!"
                                                                                                        ToolTip="Mandatory Field!!!" Width="10px" />
                                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtDept_ID"
                                                                                                    ErrorMessage="RequiredFieldValidator" ToolTip="Department Code is required!!!"
                                                                                                    ValidationGroup="Save"><img src="../App_Themes/Styles/Images/err1.png" 
                                                                                title="Department Code is required!!!" /></asp:RequiredFieldValidator>
                                                                                            </td>
                                                                                            <td>
                                                                                                &#160;&#160;
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td colspan="2">
                                                                                                <asp:Label ID="Label1" runat="server" CssClass="Label" Text="Department Desc :"></asp:Label>
                                                                                            </td>
                                                                                            <td colspan="2">
                                                                                                <asp:TextBox ID="txtDeptName" runat="server" CssClass="TextBox" Height="25px" MaxLength="50"
                                                                                                    Width="212px"></asp:TextBox>
                                                                                                <asp:Image ID="img_Str1" runat="server" Height="10px" ImageUrl="~/App_Themes/Styles/Images/img_mand.png"
                                                                                                    title="Department Name is required!!!" ToolTip="Mandatory Field!!!" Width="10px" /><asp:RequiredFieldValidator
                                                                                                        ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtDeptName" ErrorMessage="RequiredFieldValidator"
                                                                                                        ToolTip="Department Name is required!!!" ValidationGroup="Save"><img 
                                                                                    src="../App_Themes/Styles/Images/err1.png" title="Department Name is required!!!" /></asp:RequiredFieldValidator>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <asp:Label ID="lblStatus" runat="server" CssClass="Label" Text="Status :"></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                &#160;&#160;
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:RadioButton ID="RBactivate" Checked="True" Text="Activate"  runat="server" ValidationGroup="A" 
                                                                                                GroupName="RDgrp" />&nbsp;&nbsp;
                                                                                            <asp:RadioButton ID="RBdeactivate" Text="De-Activate" runat="server" ValidationGroup="A"
                                                                                                GroupName="RDgrp" />

                                                                                            </td>
                                                                                            <td>
                                                                                                &#160;&#160;
                                                                                            </td>
                                                                                            <td>
                                                                                                &#160;&#160;
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td colspan="2">
                                                                                                <asp:Label ID="lblRemark" runat="server" CssClass="Label" Text="Remark :"></asp:Label>
                                                                                            </td>
                                                                                            <td colspan="2">
                                                                                                <asp:TextBox ID="txtRemark" runat="server" CssClass="TextBox" Height="25px" MaxLength="50"
                                                                                                    Width="212px"></asp:TextBox>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </tr>
                                                                                </table>
                                                                            </ContentTemplate>
                                                                        </asp:TabPanel>
                                                                    </asp:TabContainer>
                                                                </td>
                                                                <td style="width: 50%;" valign="top">
                                                                    <asp:Panel ID="pnlImport" runat="server" ScrollBars="Auto" Height="400px">
                                                                        <asp:GridView ID="grPlant1" runat="server" AllowPaging="True" AllowSorting="True"
                                                                            AutoGenerateColumns="False" CaptionAlign="Left" CellPadding="4" CssClass="style22"
                                                                            EmptyDataText="There are no items to show in this view." Height="16px" PageSize="10"
                                                                            ShowFooter="True" Width="825px" OnPageIndexChanging="grPlant1_PageIndexChanging"
                                                                            OnRowDataBound="grPlant1_RowDataBound">
                                                                            <Columns>
                                                                                <asp:BoundField DataField="PlantCode" HeaderText="Plant Code" SortExpression="PlantCode" />
                                                                               <%-- <asp:BoundField DataField="DepartmentCode" HeaderText="Department Code" SortExpression="DepartmentCode" />--%>
                                                                                <asp:BoundField DataField="DepartmentDesc" HeaderText="Department Desc" SortExpression="DepartmentDesc" />
                                                                                <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status" />
                                                                                <asp:BoundField DataField="REMARK" HeaderText="REMARK" SortExpression="REMARK" />
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
                                                            <asp:ListItem Selected="True" Value="PlantCode">Plant Code</asp:ListItem>
                                                            <asp:ListItem Value="DepartmentCode">Department Code</asp:ListItem>
                                                            <asp:ListItem Value="DepartmentDesc">Department Desc.</asp:ListItem>
                                                            <asp:ListItem Value="STATUS">Status</asp:ListItem>
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
                                            <asp:GridView ID="GrDept" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False"
                                                CaptionAlign="Left" CellPadding="4" CssClass="GridViewStyle" EmptyDataText="There are no items to show in this view."
                                                PageSize="20" ShowFooter="True" OnRowDataBound="GrDept_RowDataBound" DataKeyNames="Refno"
                                                OnPageIndexChanging="GrDept_PageIndexChanging1" OnRowCommand="GrDept_RowCommand"
                                                OnSorting="GrDept_Sorting" Width="100%">
                                                <Columns>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="LinkButton1" CommandName="Select" Text="Select" runat="server" /></ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="PlantCode" HeaderText="PLANT Code" SortExpression="Plant_Code" />
                                                    <asp:BoundField DataField="DepartmentCode" HeaderText="DEPARTMENT ID" SortExpression="Department_Code" />
                                                    <asp:BoundField DataField="DepartmentDesc" HeaderText="DEPARTMENT NAME" SortExpression="Department_Desc" />
                                                    <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status" />
                                                    <asp:BoundField DataField="REMARK" HeaderText="Remark" SortExpression="REMARK" />

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
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>


