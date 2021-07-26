<%@ Page Title="" Language="C#" MasterPageFile="~/Modules/MasterPage.master" AutoEventWireup="true" CodeFile="frmPrinter.aspx.cs" 
Inherits="Modules_frmPrinter" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="Server">
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
            text-align: left;
            font-family: calibri;
            font-style: normal;
            font-size: 14px;
            color: #000000;
            border: 1px solid black;
        }
        .style24
        {
            width: 485px;
            height: 179px;
        }
        .style26
        {
            width: 103%;
        }
        .style28
        {
            width: 199px;
        }
    </style>
    <script src="../Modules/Javascript/dw_tooltip_c.js" type="text/javascript"></script>
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
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table cellpadding="0" cellspacing="0" width="100%">
             <tr>
                    <td style="height: 30px; background-image: url('../App_Themes/Styles/Images/Form_Title.JPG');
                        background-repeat: repeat-x">
                        <asp:Label ID="Label35" runat="server" CssClass="HeadingLabel" Text="&amp;nbsp;Printer Master"></asp:Label>
                    </td>
             </tr>
             <tr>
                    <td>
                        <asp:MultiView ID="MultiView1" runat="server">
                            <asp:View ID="View2" runat="server">
                                <table cellpadding="0" cellspacing="0" class="style26">
                                <tr>
                                        <td height="30" style="height: 30px; background-image: url('../App_Themes/Styles/Images/Menu_Out.png');
                                            background-repeat: repeat-x">
                                            <table cellpadding="0" cellspacing="0" width="100%">
                                                <tr>
                                                    <td height="30" style="height: 30px; background-image: url('../App_Themes/Styles/Images/Menu_Out.png');
                                                        background-repeat: repeat-x">
                                                        <table cellpadding="5">
                                                            <tr>
                                                                <td>
                                                                    <asp:ImageButton ID="imgCloseV1" runat="server" Height="24px" ImageUrl="~/App_Themes/Styles/Images/exit.ico"
                                                                        ToolTip="Close Page" Width="24px" PostBackUrl="~/Modules/frmMain.aspx" />
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
                                                                <td>
                                                                </td>
                                                            </tr>
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
                                                                        Height="100%" Width="500px">
                                                                        <asp:TabPanel ID="TabPanel1" runat="server" HeaderText="TabPanel1">
                                                                            <HeaderTemplate>
                                                                                General
                                                                            </HeaderTemplate>
                                                                            <ContentTemplate>
                                                                                <table style="width: 100%">
                                                                                    <tr>
                                                                                        <td colspan="3" style="height: 10px; background-image: url('../App_Themes/Styles/Images/Form_Title.JPG');
                                                                                            background-repeat: repeat-x">
                                                                                            <asp:Label ID="Label5" runat="server" CssClass="HeadingLabel" Font-Size="15px" Text="&amp;nbsp;Import Excel"></asp:Label>
                                                                                        </td>
                                                                                    </tr>
                                                                                 
                                                                                    <tr>
                                                                                        <td colspan="3" class="style18">
                                                                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                                                                <ContentTemplate>
                                                                                                    <table>
                                                                                                        <tr>
                                                                                                            <td class="style21">
                                                                                                                &nbsp;&nbsp;
                                                                                                            </td>
                                                                                                            <td class="style18">
                                                                                                                <asp:Label ID="lblBrowse" runat="server" CssClass="Label" Text="Browse:"></asp:Label>
                                                                                                            </td>
                                                                                                            <td class="style18">
                                                                                                                <asp:FileUpload ID="FlUpload" runat="server" ForeColor="#009900" style="width: 200px;" />
                                                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="FlUpload"
                                                                                                                    ErrorMessage="User ID is required!!!" Text="&lt;img src='../App_Themes/Styles/Images/err1.png' title='User ID is required!!!' /&gt;"
                                                                                                                    ToolTip="User ID is required!!!" ValidationGroup="Import1"></asp:RequiredFieldValidator>
                                                                                                            </td>
                                                                                                          
                                                                                                            <td class="style21">
                                                                                                                <asp:Button ID="btnImp" runat="server" CssClass="Button" Text="Import" Width="60px"
                                                                                                                    OnClick="btnImp_Click" ValidationGroup="Import1" /></td>
                                                                                                                  <td> <asp:Button ID="btnTemplate" runat="server" CssClass="Button" style=" margin-left:10px"
                                                                                                        Text="Template" Width="65px" onclick="btnTemplate_Click" />
                                                                                                            </td>
                                                                                                    </table>
                                                                                                </ContentTemplate>
                                                                                                <Triggers>
                                                                                                    <asp:PostBackTrigger ControlID="btnImp" />
                                                                                                </Triggers>
                                                                                            </asp:UpdatePanel>
                                                                                        </td>
                                                                                        <tr>
                                                                                            <td colspan="3" style="height: 10px; background-image: url('../App_Themes/Styles/Images/Form_Title.JPG');
                                                                                                background-repeat: repeat-x">
                                                                                            </td>
                                                                                        </tr>
                                                                                    
                                                                                        <tr>
                                                                                            <td class="style19">
                                                                                                <asp:Label ID="lblItemCode14" runat="server" CssClass="Label" Text="Plant Code :"></asp:Label>
                                                                                            </td>
                                                                                            <td class="style19">
                                                                                                <asp:DropDownList ID="ddlPlantCode" runat="server" AutoPostBack="True" CssClass="Combobox"
                                                                                                    Height="31px" Width="224px" >
                                                                                                    <asp:ListItem>Select</asp:ListItem>
                                                                                                </asp:DropDownList>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Image ID="img_Str" runat="server" Height="10px" ImageUrl="~/App_Themes/Styles/Images/img_mand.png"
                                                                                                    title="Plant Name is required!!!" ToolTip="Mandatory Field!!!" Width="10px" />
                                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" InitialValue="Select" runat="server"
                                                                                                    ControlToValidate="ddlPlantCode" ErrorMessage="Select Plant code" ToolTip="Plz select plant code is required!!!"
                                                                                                    ValidationGroup="Save"><img src="../App_Themes/Styles/Images/err1.png" 
                                                                                                    title="Plant Name is required!!!" /></asp:RequiredFieldValidator>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <asp:Label ID="lblItemCode13" runat="server" CssClass="Label" Text="Printer Code:"></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:TextBox ID="txtPrinterCode" runat="server" CssClass="TextBox" Height="27px"
                                                                                                    MaxLength="500" SkinID="NAME" Width="223px"></asp:TextBox>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Image ID="img_Str0" runat="server" Height="10px" ImageUrl="~/App_Themes/Styles/Images/img_mand.png"
                                                                                                    title="Printer Code is required!!!" ToolTip="Mandatory Field!!!" Width="10px" />
                                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtPrinterCode"
                                                                                                    ErrorMessage="Enter Printer Code" ToolTip="Printer Code is required!!!"
                                                                                                    ValidationGroup="Save"><img src="../App_Themes/Styles/Images/err1.png" 
                                                                                                     title="Printer Code is required!!!" /></asp:RequiredFieldValidator>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <asp:Label ID="Label1" runat="server" CssClass="Label" Text="Printer IP :"></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:TextBox ID="txtPrinterIP" runat="server" CssClass="TextBox" Height="27px" MaxLength="500"
                                                                                                    SkinID="NAME" Width="223px"></asp:TextBox>
                                                                                                    <asp:FilteredTextBoxExtender
                                                                                                    ID="filterPrinterIp" runat="server" FilterType="Custom, Numbers" ValidChars="."
                                                                                                    TargetControlID="txtPrinterIP" Enabled="True" >
                                                                                                </asp:FilteredTextBoxExtender>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Image ID="img_Str1" runat="server" Height="10px" ImageUrl="~/App_Themes/Styles/Images/img_mand.png"
                                                                                                    title="Printer IP is required!!!" ToolTip="Mandatory Field!!!" Width="10px" />
                                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtPrinterIP"
                                                                                                    ErrorMessage="Enter Printer IP" ToolTip="Printer IP is required!!!"
                                                                                                    ValidationGroup="Save"><img src="../App_Themes/Styles/Images/err1.png" title="Printer IP is required!!!" /></asp:RequiredFieldValidator>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td class="style28">
                                                                                                <asp:Label ID="Label36" runat="server" CssClass="Label" Text="Printer Port :"></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:TextBox ID="txtPrinterPort" runat="server" CssClass="TextBox" Height="27px"
                                                                                                    MaxLength="500" SkinID="NAME" Width="223px"></asp:TextBox>
                                                                                                <asp:FilteredTextBoxExtender
                                                                                                    ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers" 
                                                                                                    TargetControlID="txtPrinterPort"  Enabled="True" >
                                                                                                </asp:FilteredTextBoxExtender>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Image ID="img_Str2" runat="server" Height="10px" ImageUrl="~/App_Themes/Styles/Images/img_mand.png"
                                                                                                    title="Printer Port is required!!!" ToolTip="Mandatory Field!!!" Width="10px" />
                                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtPrinterPort"
                                                                                                    ErrorMessage="Printer Port is required" ToolTip="Printer Port is required!!!"
                                                                                                    ValidationGroup="Save"><img 
                                                                                                src="../App_Themes/Styles/Images/err1.png" title="Printer Port is required!!!" /></asp:RequiredFieldValidator>
                                                                                            </td>
                                                                                            <tr>
                                                                                                <td class="style28">
                                                                                                    <asp:Label ID="Label38" runat="server" CssClass="Label" Text="Booth/Area Code :"></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:DropDownList ID="ddlBooth" runat="server" AutoPostBack="True" CssClass="Combobox"
                                                                                                        Height="31px" Width="224px">
                                                                                                        <asp:ListItem>Select</asp:ListItem>
                                                                                                        
                                                                                                    </asp:DropDownList>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:Image ID="img_Str4" runat="server" Height="10px" ImageUrl="~/App_Themes/Styles/Images/img_mand.png"
                                                                                                        title="Cubicle Code is required!!!" ToolTip="Mandatory Field!!!" Width="10px" />
                                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" InitialValue="Select" runat="server" ControlToValidate="ddlBooth"
                                                                                                        ErrorMessage="Enter booth Code" ToolTip="Booth Code is required!!!"
                                                                                                        ValidationGroup="Save"><img 
                                                                                                        src="../App_Themes/Styles/Images/err1.png" title="Booth Code is required!!!" /></asp:RequiredFieldValidator>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td class="style28">
                                                                                                    &nbsp;<asp:Label ID="Label37" runat="server" CssClass="Label" Text="Status :"></asp:Label>
                                                                                                    &nbsp;
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:RadioButton ID="RBactivate" Checked="True" Text="Activate"  runat="server" ValidationGroup="A" 
                                                                                                GroupName="RDgrp" />&nbsp;&nbsp;
                                                                                            <asp:RadioButton ID="RBdeactivate" Text="De-Activate" runat="server" ValidationGroup="A"
                                                                                                GroupName="RDgrp" />

                                                                                                </td>
                                                                                                <td>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td class="style28">
                                                                                                    <asp:Label ID="lblRemark" runat="server" CssClass="Label" Text="Remark :"></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:TextBox ID="txtRemark" runat="server" CssClass="TextBox" Height="27px" MaxLength="500"
                                                                                                        SkinID="NAME" Width="223px"></asp:TextBox>
                                                                                                </td>
                                                                                            </tr>
                                                                                </table>
                                                                            </ContentTemplate>
                                                                        </asp:TabPanel>
                                                                    </asp:TabContainer>
                                                                </td>
                                                                <td style="width: 60%">
                                                                    <asp:Panel ID="pnlImport" runat="server" ScrollBars="Auto" Height="400px" style="margin-top:20px">
                                                                        <asp:GridView ID="GrPrinter_Grid" runat="server" AllowPaging="True" AllowSorting="True"
                                                                            AutoGenerateColumns="False" CaptionAlign="Left" CellPadding="4" CssClass="style22"
                                                                            EmptyDataText="There are no items to show in this view." PageSize="20" ShowFooter="True"
                                                                            Height="16px" Width="825px" OnPageIndexChanging="GrPrinter_Grid_PageIndexChanging1"
                                                                            OnRowDataBound="GrPrinter_Grid_RowDataBound1">
                                                                            <Columns>
                                                                                <asp:BoundField DataField="PLANTCODE" HeaderText="PLANT CODE" SortExpression="PlantID" />
                                                                                <asp:BoundField DataField="PRINTERCODE" HeaderText="PRINTER CODE" SortExpression="PRINTER CODE" />
                                                                                <asp:BoundField DataField="PRINTERIP" HeaderText="PRINTER IP" SortExpression="PRINTER IP" />
                                                                                <asp:BoundField DataField="PRINTERPORT" HeaderText="PRINTER PORT" SortExpression="PRINTER PORT" />
                                                                                <asp:BoundField DataField="BOOTHCODE" HeaderText="BOOTHCODE" SortExpression="BOOTHCODE" />
                                                                                <asp:BoundField DataField="REMARK" HeaderText="REMARK" SortExpression="REMARK" />
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
                                                                        <br />
                                                                        <asp:Button ID="btnSave" runat="server" CssClass="Button" Text="Save"  Width="60px"
                                                                            OnClick="btnSave_Click" />
                                                                        &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;
                                                                        <asp:Button ID="btnCancel" runat="server" CssClass="Button" Text="Cancel"  Width="60px"
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
                                            <table cellpadding="0" cellspacing="0" width="100%">
                                                <tr>
                                                    <td height="30" style="height: 30px; background-image: url('../App_Themes/Styles/Images/Menu_Out.png');
                                                        background-repeat: repeat-x">
                                                        <table cellpadding="5">
                                                            <tr>
                                                                <td>
                                                                    <asp:ImageButton ID="imgCLose" runat="server" Height="24px" ImageUrl="~/App_Themes/Styles/Images/exit.ico"
                                                                        ToolTip="Close Page" Width="24px" OnClick="imgCLose_Click" />
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
                                                                        <asp:ListItem Selected="True" Value="PLANTCODE">Plant Code</asp:ListItem>
                                                                        <asp:ListItem Value="PRINTERCODE">Printer Code</asp:ListItem>
                                                                  
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
                                                                    <asp:TextBox ID="txtSearch" runat="server" CssClass="TextBox" ValidationGroup="Search"
                                                                        Height="20px"></asp:TextBox>
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
                                            </table>
                                            <table cellpadding="5" width="100%">
                                                <tr>
                                                    <td style="width: 100%;">
                                                        <table width="100%">
                                                            <tr valign="top">
                                                                <td style="width: 100%">
                                                                    <asp:GridView ID="GrPrinter" runat="server" AllowPaging="True" AllowSorting="True"
                                                                        AutoGenerateColumns="False" CaptionAlign="Left" CellPadding="4" CssClass="style22"
                                                                        EmptyDataText="There are no items to show in this view." DataKeyNames="REFNO"
                                                                        Width="100%" PageSize="20" ShowFooter="True" Height="16px" OnPageIndexChanging="GrPrinter_PageIndexChanging"
                                                                        OnRowCommand="GrPrinter_RowCommand" 
                                                                        OnRowDataBound="GrPrinter_RowDataBound">
                                                                        <Columns>
                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton ID="LinkButton1" CommandName="Select" Text="Select" runat="server" />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:BoundField DataField="PLANTCODE" HeaderText="PLANTCODE" SortExpression="PlantID" />
                                                                            <asp:BoundField DataField="PRINTERCODE" HeaderText="PRINTER CODE" SortExpression="PrinterCode" />
                              
                                                                            <asp:BoundField DataField="PRINTERIP" HeaderText="PRINTER IP" SortExpression="IPAddress" />
                                                                            <asp:BoundField DataField="PRINTERPORT" HeaderText="PRINTER PORT" SortExpression="PortNo" />
                                                                            <asp:BoundField DataField="BOOTHCODE" HeaderText="BOOTH CODE" SortExpression="BOOTH" />
                                                                            <asp:BoundField DataField="STATUS" HeaderText="STATUS" SortExpression="Status" />
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
                                                                    &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;
                                                                </td>
                                                            </tr>
                                                        </table>
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
            </td> </tr> </table> </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="imgExport" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>

