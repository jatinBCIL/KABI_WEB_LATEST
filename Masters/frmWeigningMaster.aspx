<%@ Page Title="" Language="C#" MasterPageFile="~/Modules/MasterPage.master" AutoEventWireup="true"
    CodeFile="frmWeigningMaster.aspx.cs" Inherits="Masters_frmWeigningMaster" %>

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
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td style="height: 30px; background-image: url('../App_Themes/Styles/Images/Form_Title.JPG');
                        background-repeat: repeat-x">
                        <asp:Label ID="Label35" runat="server" CssClass="HeadingLabel" Text="&amp;nbsp;Weighing Scale Master"></asp:Label>
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
                                                            ToolTip="Close Page" Width="24px" />
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
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                            </table>
                                            <table width="100%">
                                                <tr valign="top">
                                                    <td style="width: 50%;">
                                                        <asp:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" EnableTheming="True"
                                                            Height="80%" Width="700px">
                                                            <asp:TabPanel ID="TabPanel1" runat="server" HeaderText="TabPanel1">
                                                                <HeaderTemplate>
                                                                    General</HeaderTemplate>
                                                                <ContentTemplate>
                                                                    <table style="width: 100%">
                                                                        <tr>
                                                                            <td colspan="3" style="height: 10px; background-image: url('../App_Themes/Styles/Images/Form_Title.JPG');
                                                                                background-repeat: repeat-x">
                                                                                <asp:Label ID="Label17" runat="server" CssClass="HeadingLabel" Font-Size="15px" Text="&amp;nbsp;Import Excel"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="3" class="style18">
                                                                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                                                    <ContentTemplate>
                                                                                        <table>
                                                                                            <tr>
                                                                                                <td>
                                                                                                </td>
                                                                                                <td class="style18">
                                                                                                    <asp:Label ID="lblBrowse" runat="server" CssClass="Label" Text="Browse:"></asp:Label>
                                                                                                </td>
                                                                                                <td class="style18">
                                                                                                    <asp:FileUpload ID="FlUpload" runat="server" ForeColor="#009900" Style="width: 200px;" /><asp:RequiredFieldValidator
                                                                                                        ID="RequiredFieldValidator4" runat="server" ControlToValidate="FlUpload" ErrorMessage="User ID is required!!!"
                                                                                                        Text="&lt;img src='../App_Themes/Styles/Images/err1.png' title='User ID is required!!!' /&gt;"
                                                                                                        ToolTip="User ID is required!!!" ValidationGroup="Import1"></asp:RequiredFieldValidator>
                                                                                                </td>
                                                                                                <td class="style21">
                                                                                                    <asp:Button ID="btnImp" runat="server" CssClass="Button" Text="Import" Width="60px"
                                                                                                        OnClick="btnImp_Click" ValidationGroup="Import1" />
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:Button ID="btnTemplate" runat="server" CssClass="Button" Style="margin-left: 10px"
                                                                                                        Text="Template" Width="65px" OnClick="btnTemplate_Click" />
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
                                                                            <td colspan="3" style="height: 10px; background-image: url('../App_Themes/Styles/Images/Form_Title.JPG');
                                                                                background-repeat: repeat-x">
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                            </td>
                                                                            <td class="style19">
                                                                                <asp:Label ID="Label14" runat="server" CssClass="Label" Text="Plant Code"></asp:Label>
                                                                            </td>
                                                                            <td class="style19">
                                                                                <asp:DropDownList ID="ddPlantcode" runat="server" CssClass="Combobox" Height="31px"
                                                                                    Width="224px">
                                                                                </asp:DropDownList>
                                                                                <asp:Image ID="Image10" runat="server" Height="10px" Width="10px" ToolTip="Mandatory Field!!!"
                                                                                    ImageUrl="../App_Themes/Styles/Images/img_mand.png" /><asp:RequiredFieldValidator
                                                                                        ID="rfvPlantcode" runat="server" ControlToValidate="ddPlantcode" InitialValue="Select"
                                                                                        ErrorMessage="Plant Code is required!!!" Text="&lt;img src='../App_Themes/Styles/Images/err1.png' title='Plant Code is required!!!' /&gt;"
                                                                                        ToolTip="Plant Code is required!!!" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                                                            </td>
                                                                            <td>
                                                                                &#160;&#160;
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lblItemCode13" runat="server" CssClass="Label" Text="Weighing Code:"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtWeighingCode" runat="server" SkinID="NAME" CssClass="TextBox"
                                                                                    Width="220px" MaxLength="500"></asp:TextBox><asp:Image ID="Image11" runat="server"
                                                                                        Height="10px" Width="10px" ToolTip="Mandatory Field!!!" ImageUrl="../App_Themes/Styles/Images/img_mand.png" /><asp:RequiredFieldValidator
                                                                                            ID="rfvPlantnm" runat="server" ControlToValidate="txtWeighingCode" ErrorMessage="Weighing code is required!!!"
                                                                                            Text="&lt;img src='../App_Themes/Styles/Images/err1.png' title='Weighing code is required!!!' /&gt;"
                                                                                            ToolTip="Weighing code is required!!!" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label class="style19" ID="lblItemCode16" runat="server" CssClass="Label" Text="Weighing IP :"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox class="style19" ID="txtWeighingIP" runat="server" SkinID="NAME" CssClass="TextBox"
                                                                                    Width="220px" MaxLength="500"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                &#160;&#160;
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="Label8" runat="server" CssClass="Label" Text="Weighing Port :"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtWeighingPort" runat="server" SkinID="NAME" CssClass="TextBox"
                                                                                    Width="220px" MaxLength="500"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="Label1" runat="server" CssClass="Label" Text="Weighing Capacity :"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtWeighingCapacity" runat="server" SkinID="NAME" CssClass="TextBox"
                                                                                    Width="220px" MaxLength="500"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                &#160;&#160;
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="Label7" runat="server" CssClass="Label" Text="Weighing UOM :"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:DropDownList ID="ddlUom" runat="server" CssClass="Combobox" Height="25px" Width="224px">
                                                                                    <asp:ListItem>Select</asp:ListItem>
                                                                                    <asp:ListItem>KG</asp:ListItem>
                                                                                    <asp:ListItem>G</asp:ListItem>
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                &#160;&#160;
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="Label10" runat="server" CssClass="Label" Text="Least Count :"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtLeastCount" runat="server" SkinID="NAME" CssClass="TextBox" Width="220px"
                                                                                    MaxLength="500"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                &#160;&#160;
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="Label11" runat="server" CssClass="Label" Text="Min Value :"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtMinValue" runat="server" SkinID="NAME" CssClass="TextBox" Width="220px"
                                                                                    MaxLength="500"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                &#160;&#160;
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="Label38" runat="server" CssClass="Label" Text="Max Value :"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtMaxValue" runat="server" CssClass="TextBox" MaxLength="500" SkinID="NAME"
                                                                                    Width="220px"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <asp:Panel ID="Panel1" Visible="False" runat="server">
                                                                            <tr>
                                                                                <td>
                                                                                    &#160;&#160;
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="Label2" runat="server" CssClass="Label" Text="Booth"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:DropDownList ID="ddlBooth" runat="server" AutoPostBack="True" CssClass="Combobox"
                                                                                        Height="25px" Width="224px">
                                                                                    </asp:DropDownList>
                                                                                    <asp:Image ID="Image1" runat="server" Height="10px" Width="10px" ToolTip="Mandatory Field!!!"
                                                                                        ImageUrl="../App_Themes/Styles/Images/img_mand.png" /><asp:RequiredFieldValidator
                                                                                            ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlBooth" InitialValue="Select"
                                                                                            ErrorMessage="Booth is required!!!" Text="&lt;img src='../App_Themes/Styles/Images/err1.png' title='Plant Code is required!!!' /&gt;"
                                                                                            ToolTip="Booth Code is required!!!" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                                                                </td>
                                                                            </tr>
                                                                        </asp:Panel>
                                                                        <tr>
                                                                            <td>
                                                                                &#160;&#160;
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="Label6" runat="server" CssClass="Label" Text="Weight Box ID No:"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox runat="server" MaxLength="500" CssClass="TextBox" Height="25px" SkinID="NAME"
                                                                                    Width="220px" ID="txtWeightboxId"></asp:TextBox><asp:Image ID="Image7" runat="server"
                                                                                        Height="10px" Width="10px" ToolTip="Mandatory Field!!!" ImageUrl="../App_Themes/Styles/Images/img_mand.png" /><asp:RequiredFieldValidator
                                                                                            ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtWeightboxId"
                                                                                            InitialValue="Select" ErrorMessage="WeightboxId is required!!!" Text="&lt;img src='../App_Themes/Styles/Images/err1.png' title='Plant Code is required!!!' /&gt;"
                                                                                            ToolTip="WeightboxId is required!!!" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                &#160;&#160;
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="Label3" runat="server" CssClass="Label" Text="Monthly Calibration Due Days"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox runat="server" MaxLength="500" CssClass="TextBox" Height="25px" SkinID="NAME"
                                                                                    Width="220px" ID="txtDueDays"></asp:TextBox><asp:Image ID="Image2" runat="server"
                                                                                        Height="10px" Width="10px" ToolTip="Mandatory Field!!!" ImageUrl="../App_Themes/Styles/Images/img_mand.png" /><asp:RequiredFieldValidator
                                                                                            ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtDueDays" InitialValue="Select"
                                                                                            ErrorMessage="txtDueDays is required!!!" Text="&lt;img src='../App_Themes/Styles/Images/err1.png' title='Plant Code is required!!!' /&gt;"
                                                                                            ToolTip="txtDueDays is required!!!" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                &#160;&#160;
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="Label4" runat="server" CssClass="Label" Text="Tolerance Days"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox runat="server" MaxLength="500" CssClass="TextBox" Height="25px" SkinID="NAME"
                                                                                    Width="220px" ID="txtTolarance"></asp:TextBox><asp:Image ID="Image3" runat="server"
                                                                                        Height="10px" Width="10px" ToolTip="Mandatory Field!!!" ImageUrl="../App_Themes/Styles/Images/img_mand.png" /><asp:RequiredFieldValidator
                                                                                            ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtTolarance"
                                                                                            InitialValue="Select" ErrorMessage="Tolarance is required!!!" Text="&lt;img src='../App_Themes/Styles/Images/err1.png' title='Plant Code is required!!!' /&gt;"
                                                                                            ToolTip="Tolarance is required!!!" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                &#160;&#160;
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="Label13" runat="server" CssClass="Label" Text="Serial No."></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtSerialNo" runat="server" MaxLength="500" CssClass="TextBox" Height="25px"
                                                                                    SkinID="NAME" Width="220px"></asp:TextBox><asp:Image ID="Image4" runat="server" Height="10px"
                                                                                        Width="10px" ToolTip="Mandatory Field!!!" ImageUrl="../App_Themes/Styles/Images/img_mand.png" /><asp:RequiredFieldValidator
                                                                                            ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtWorkingRange"
                                                                                            InitialValue="Select" ErrorMessage="SerialNo is required!!!" Text="&lt;img src='../App_Themes/Styles/Images/err1.png' title='Plant Code is required!!!' /&gt;"
                                                                                            ToolTip="SerialNo is required!!!" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                &#160;&#160;
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="Label15" runat="server" CssClass="Label" Text="Operating Range."></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtWorkingRange" runat="server" MaxLength="500" CssClass="TextBox"
                                                                                    Height="25px" SkinID="NAME" Width="220px"></asp:TextBox><asp:Image ID="Image5" runat="server"
                                                                                        Height="10px" Width="10px" ToolTip="Mandatory Field!!!" ImageUrl="../App_Themes/Styles/Images/img_mand.png" /><asp:RequiredFieldValidator
                                                                                            ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtWorkingRange"
                                                                                            InitialValue="Select" ErrorMessage="WorkingRange is required!!!" Text="&lt;img src='../App_Themes/Styles/Images/err1.png' title='Plant Code is required!!!' /&gt;"
                                                                                            ToolTip="WorkingRange is required!!!" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                &#160;&#160;
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="Label5" runat="server" CssClass="Label" Text="Certificate No."></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtCertificateNo" runat="server" MaxLength="500" CssClass="TextBox"
                                                                                    Height="25px" SkinID="NAME" Width="220px"></asp:TextBox><asp:Image ID="Image8" runat="server"
                                                                                        Height="10px" Width="10px" ToolTip="Mandatory Field!!!" ImageUrl="../App_Themes/Styles/Images/img_mand.png" /><asp:RequiredFieldValidator
                                                                                            ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtCertificateNo"
                                                                                            InitialValue="Select" ErrorMessage="CertificateNo is required!!!" Text="&lt;img src='../App_Themes/Styles/Images/err1.png' title='Plant Code is required!!!' /&gt;"
                                                                                            ToolTip="CertificateNo is required!!!" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                &#160;&#160;
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="Label12" runat="server" CssClass="Label" Text="Department."></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:DropDownList ID="ddDepartment" runat="server" AutoPostBack="True" CssClass="Combobox"
                                                                                    Width="210px">
                                                                                </asp:DropDownList>
                                                                                <asp:Image ID="Image9" runat="server" Height="10px" Width="10px" ToolTip="Mandatory Field!!!"
                                                                                    ImageUrl="../App_Themes/Styles/Images/img_mand.png" />
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                &#160;&#160;
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="Label16" runat="server" CssClass="Label" Text="Sop No."></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtsopno" runat="server" MaxLength="500" CssClass="TextBox" Height="25px"
                                                                                    SkinID="NAME" Width="220px"></asp:TextBox><asp:Image ID="Image6" runat="server" Height="10px"
                                                                                        Width="10px" ToolTip="Mandatory Field!!!" ImageUrl="../App_Themes/Styles/Images/img_mand.png" />
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                &#160;&#160;
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="Label9" runat="server" CssClass="Label" Text="Status"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:RadioButton ID="RBactivate" Checked="True" Text="Activate" runat="server" ValidationGroup="A"
                                                                                    GroupName="RDgrp" />&#160;&#160;
                                                                                <asp:RadioButton ID="RBdeactivate" Text="De-Activate" runat="server" ValidationGroup="A"
                                                                                    GroupName="RDgrp" />&#160;&#160;
                                                                                <asp:Image ID="Image14" runat="server" Height="10px" Width="10px" ToolTip="Mandatory Field!!!"
                                                                                    ImageUrl="../App_Themes/Styles/Images/img_mand.png" />
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                &#160;&#160;
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lblRemark" runat="server" Visible="False" CssClass="Label" Text="Remark :"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtRemark" runat="server" Visible="False" CssClass="TextBox" Height="25px"
                                                                                    MaxLength="500" SkinID="NAME" Width="220px"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                            </td>
                                                                            <td colspan="3">
                                                                                <asp:Panel ID="Panel2" runat="server">
                                                                                    <div>
                                                                                        <asp:TabContainer ID="TabContainer2" runat="server" ActiveTabIndex="0" EnableTheming="True"
                                                                                            Width="950px" Style="height: auto">
                                                                                            <asp:TabPanel ID="TabPanel2" runat="server" HeaderText="TabPanel1">
                                                                                                <HeaderTemplate>
                                                                                                    4 Point Calibration</HeaderTemplate>
                                                                                                <ContentTemplate>
                                                                                                    <asp:GridView ID="GridView1" runat="server" AllowPaging="True" Width="700px" Height="100px"
                                                                                                        AllowSorting="True" AutoGenerateColumns="False" CssClass="GridViewStyle" CaptionAlign="Left"
                                                                                                        EmptyDataText="There are no items to show in this view." ShowFooter="True" Style="text-align: center">
                                                                                                        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                                                                                                        <HeaderStyle HorizontalAlign="Center" CssClass="GridViewHeaderStyle" ForeColor="White"
                                                                                                            Height="40px" VerticalAlign="Middle" />
                                                                                                        <Columns>
                                                                                                            <asp:TemplateField HeaderText="Weight" ItemStyle-HorizontalAlign="Center">
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:Label ID="lblPoint" runat="server" CssClass="Label" Style="text-align: center"
                                                                                                                        Text='<%#Eval("Point") %>'></asp:Label></ItemTemplate>
                                                                                                                <HeaderStyle ForeColor="White" HorizontalAlign="Center" />
                                                                                                                <ItemStyle HorizontalAlign="Center" />
                                                                                                            </asp:TemplateField>
                                                                                                            <asp:TemplateField HeaderText="Standard Weight" ItemStyle-HorizontalAlign="Center">
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:TextBox ID="txtstdwt" Width="80px" Text='<%#Eval("STANDARDWEIGHT") %>' runat="server"
                                                                                                                        onkeypress="return isDecimal(event)" Style="text-align: center" BackColor="BlanchedAlmond"
                                                                                                                        ValidationGroup="Save"></asp:TextBox></ItemTemplate>
                                                                                                                <HeaderStyle ForeColor="White" HorizontalAlign="Center" />
                                                                                                                <ItemStyle HorizontalAlign="Center" />
                                                                                                            </asp:TemplateField>
                                                                                                            <asp:TemplateField HeaderText="Range (From)" ItemStyle-HorizontalAlign="Center">
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:TextBox ID="txtRangeFrom" Width="80px" Text='<%#Eval("RangeFrom") %>' runat="server"
                                                                                                                        onkeypress="return isDecimal(event)" Style="text-align: center" BackColor="BlanchedAlmond"
                                                                                                                        ValidationGroup="Save"></asp:TextBox></ItemTemplate>
                                                                                                                <HeaderStyle ForeColor="White" HorizontalAlign="Center" />
                                                                                                                <ItemStyle HorizontalAlign="Center" />
                                                                                                            </asp:TemplateField>
                                                                                                            <asp:TemplateField HeaderText="Range (To)" ItemStyle-HorizontalAlign="Center">
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:TextBox ID="txtRangeTo" Width="80px" runat="server" Text='<%#Eval("RangeTo") %>'
                                                                                                                        onkeypress="return isDecimal(event)" Style="text-align: center" BackColor="BlanchedAlmond"
                                                                                                                        ValidationGroup="Save"></asp:TextBox></ItemTemplate>
                                                                                                                <HeaderStyle ForeColor="White" HorizontalAlign="Center" />
                                                                                                                <ItemStyle HorizontalAlign="Center" />
                                                                                                            </asp:TemplateField>
                                                                                                        </Columns>
                                                                                                        <EditRowStyle CssClass="GridViewEditRowStyle" />
                                                                                                        <EmptyDataRowStyle CssClass="EmptyDataRowStyle" />
                                                                                                        <FooterStyle CssClass="GridViewFooterStyle" />
                                                                                                        <PagerStyle CssClass="GridViewPagerStyle" />
                                                                                                        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                                                                                                    </asp:GridView>
                                                                                                </ContentTemplate>
                                                                                            </asp:TabPanel>
                                                                                            <asp:TabPanel ID="TabPanel3" runat="server" HeaderText="TabPanel1">
                                                                                                <HeaderTemplate>
                                                                                                    Corner Calibration</HeaderTemplate>
                                                                                                <ContentTemplate>
                                                                                                    <asp:GridView ID="GridView2" runat="server" AllowPaging="True" Width="700px" Height="100px"
                                                                                                        AllowSorting="True" AutoGenerateColumns="False" CssClass="GridViewStyle" CaptionAlign="Left"
                                                                                                        EmptyDataText="There are no items to show in this view." HeaderStyle-ForeColor="White"
                                                                                                        ShowFooter="True" Style="text-align: center">
                                                                                                        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                                                                                                        <Columns>
                                                                                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-ForeColor="white"
                                                                                                                HeaderText="Weight" HeaderStyle-HorizontalAlign="Center">
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:Label ID="lblPoint" runat="server" CssClass="Label" Style="text-align: center"
                                                                                                                        Text='<%#Eval("Point") %>'></asp:Label></ItemTemplate>
                                                                                                            </asp:TemplateField>
                                                                                                            <asp:TemplateField HeaderStyle-ForeColor="white" ItemStyle-HorizontalAlign="Center"
                                                                                                                HeaderText="Standard Weight" HeaderStyle-HorizontalAlign="Center">
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:TextBox ID="txtstdwt" Width="80px" Text='<%#Eval("STANDARDWEIGHT") %>' runat="server"
                                                                                                                        Style="text-align: center" ValidationGroup="Save" onkeypress="return isDecimal(event)"
                                                                                                                        BackColor="BlanchedAlmond"></asp:TextBox></ItemTemplate>
                                                                                                            </asp:TemplateField>
                                                                                                        </Columns>
                                                                                                        <EditRowStyle CssClass="GridViewEditRowStyle" />
                                                                                                        <EmptyDataRowStyle CssClass="EmptyDataRowStyle" />
                                                                                                        <FooterStyle CssClass="GridViewFooterStyle" />
                                                                                                        <HeaderStyle CssClass="GridViewHeaderStyle" ForeColor="White" Height="40px" />
                                                                                                        <PagerStyle CssClass="GridViewPagerStyle" />
                                                                                                        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                                                                                                    </asp:GridView>
                                                                                                </ContentTemplate>
                                                                                            </asp:TabPanel>
                                                                                            <asp:TabPanel ID="TabPanel4" runat="server" HeaderText="TabPanel1">
                                                                                                <HeaderTemplate>
                                                                                                    Reproducibility</HeaderTemplate>
                                                                                                <ContentTemplate>
                                                                                                    <asp:GridView ID="GridView3" runat="server" AllowPaging="True" Width="700px" Height="100px"
                                                                                                        AllowSorting="True" AutoGenerateColumns="False" CssClass="GridViewStyle" CaptionAlign="Left"
                                                                                                        EmptyDataText="There are no items to show in this view." HeaderStyle-ForeColor="White"
                                                                                                        ShowFooter="True" Style="text-align: center">
                                                                                                        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                                                                                                        <Columns>
                                                                                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-ForeColor="white"
                                                                                                                HeaderText="Weight" HeaderStyle-HorizontalAlign="Center">
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:Label ID="lblPoint" runat="server" CssClass="Label" Text='<%#Eval("Point") %>'
                                                                                                                        Style="text-align: center"></asp:Label></ItemTemplate>
                                                                                                            </asp:TemplateField>
                                                                                                            <asp:TemplateField HeaderStyle-ForeColor="white" ItemStyle-HorizontalAlign="Center"
                                                                                                                HeaderText="Standard Weight" HeaderStyle-HorizontalAlign="Center">
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:TextBox ID="txtstdwt" Width="80px" Text='<%#Eval("STANDARDWEIGHT") %>' runat="server"
                                                                                                                        Style="text-align: center" ValidationGroup="Save" onkeypress="return isDecimal(event)"
                                                                                                                        BackColor="BlanchedAlmond"></asp:TextBox></ItemTemplate>
                                                                                                            </asp:TemplateField>
                                                                                                        </Columns>
                                                                                                        <EditRowStyle CssClass="GridViewEditRowStyle" />
                                                                                                        <EmptyDataRowStyle CssClass="EmptyDataRowStyle" />
                                                                                                        <FooterStyle CssClass="GridViewFooterStyle" />
                                                                                                        <HeaderStyle CssClass="GridViewHeaderStyle" ForeColor="White" Height="40px" />
                                                                                                        <PagerStyle CssClass="GridViewPagerStyle" />
                                                                                                        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                                                                                                    </asp:GridView>
                                                                                                </ContentTemplate>
                                                                                            </asp:TabPanel>
                                                                                            <asp:TabPanel ID="TabPanel5" runat="server" HeaderText="TabPanel1">
                                                                                                <HeaderTemplate>
                                                                                                    Daily</HeaderTemplate>
                                                                                                <ContentTemplate>
                                                                                                    <asp:GridView ID="GridView4" runat="server" AllowPaging="True" Width="700px" Height="100px"
                                                                                                        AllowSorting="True" AutoGenerateColumns="False" CssClass="GridViewStyle" CaptionAlign="Left"
                                                                                                        EmptyDataText="There are no items to show in this view." HeaderStyle-ForeColor="White"
                                                                                                        ShowFooter="True" Style="text-align: center">
                                                                                                        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                                                                                                        <Columns>
                                                                                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-ForeColor="white"
                                                                                                                HeaderText="Weight" HeaderStyle-HorizontalAlign="Center">
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:Label ID="lblPoint" runat="server" CssClass="Label" Text='<%#Eval("Point") %>'
                                                                                                                        Style="text-align: center"></asp:Label></ItemTemplate>
                                                                                                            </asp:TemplateField>
                                                                                                            <asp:TemplateField HeaderStyle-ForeColor="white" ItemStyle-HorizontalAlign="Center"
                                                                                                                HeaderText="Range (From)" HeaderStyle-HorizontalAlign="Center">
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:TextBox ID="txtRangeFrom" Width="80px" Text='<%#Eval("RangeFrom") %>' runat="server"
                                                                                                                        Style="text-align: center" ValidationGroup="Save" onkeypress="return isDecimal(event)"
                                                                                                                        BackColor="BlanchedAlmond"></asp:TextBox></ItemTemplate>
                                                                                                            </asp:TemplateField>
                                                                                                            <asp:TemplateField HeaderStyle-ForeColor="white" ItemStyle-HorizontalAlign="Center"
                                                                                                                HeaderText="Range (To)" HeaderStyle-HorizontalAlign="Center">
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:TextBox ID="txtRangeTo" Width="80px" runat="server" Text='<%#Eval("RangeTo") %>'
                                                                                                                        Style="text-align: center" ValidationGroup="Save" onkeypress="return isDecimal(event)"
                                                                                                                        BackColor="BlanchedAlmond"></asp:TextBox></ItemTemplate>
                                                                                                            </asp:TemplateField>
                                                                                                        </Columns>
                                                                                                        <EditRowStyle CssClass="GridViewEditRowStyle" />
                                                                                                        <EmptyDataRowStyle CssClass="EmptyDataRowStyle" />
                                                                                                        <FooterStyle CssClass="GridViewFooterStyle" />
                                                                                                        <HeaderStyle CssClass="GridViewHeaderStyle" ForeColor="White" Height="40px" />
                                                                                                        <PagerStyle CssClass="GridViewPagerStyle" />
                                                                                                        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                                                                                                    </asp:GridView>
                                                                                                </ContentTemplate>
                                                                                            </asp:TabPanel>
                                                                                        </asp:TabContainer></div>
                                                                                </asp:Panel>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </ContentTemplate>
                                                            </asp:TabPanel>
                                                        </asp:TabContainer>
                                                    </td>
                                                    <td style="width: 50%">
                                                        <asp:Panel ID="pnlImport" runat="server" ScrollBars="Auto" Height="400px" Style="margin-top: 20px">
                                                            <asp:GridView ID="grWeigning1" runat="server" AllowPaging="True" AllowSorting="True"
                                                                AutoGenerateColumns="False" CaptionAlign="Left" CellPadding="4" CssClass="style22"
                                                                EmptyDataText="There are no items to show in this view." Height="16px" ShowFooter="True"
                                                                Width="825px" OnPageIndexChanging="grWeigning_PageIndexChanging" OnRowDataBound="grWeigning_RowDataBound">
                                                                <Columns>
                                                                    <asp:BoundField DataField="WEIGHINGCODE" HeaderText="WEIGHING CODE" SortExpression="WEIGHINGCODE" />
                                                                    <asp:BoundField DataField="PLANTCODE" HeaderText="PLANT CODE" SortExpression="PLANTCODE" />
                                                                    <asp:BoundField DataField="WEIGHINGIP" HeaderText="WEIGHING IP" SortExpression="WEIGHINGIP" />
                                                                    <asp:BoundField DataField="WEIGHINGPORT" HeaderText="WEIGHING PORT" SortExpression="WEIGHINGPORT" />
                                                                    <asp:BoundField DataField="WEIGHINGCAPACITY" HeaderText="WEIGHING CAPACITY" SortExpression="WEIGHINGCAPACITY" />
                                                                    <asp:BoundField DataField="WEIGHINGUOM" HeaderText="WEIGHINGUOM" SortExpression="WEIGHINGUOM" />
                                                                    <asp:BoundField DataField="LEASTCOUNT" HeaderText="LEAST COUNT" SortExpression="LEASTCOUNT" />
                                                                    <asp:BoundField DataField="MINVALUE" HeaderText="MIN VALUE" SortExpression="MINVALUE" />
                                                                    <asp:BoundField DataField="MAXVALUE" HeaderText="MAX VALUE" SortExpression="MAXVALUE" />
                                                                    <asp:BoundField DataField="WEIGHTBOXID" HeaderText="WEIGHTBOXID" SortExpression="WEIGHTBOXID" />
                                                                    <asp:BoundField DataField="CERTIFICATENO" HeaderText="CERTIFICATENO" SortExpression="CERTIFICATENO" />
                                                                    <asp:BoundField DataField="DUEDAYS" HeaderText="DUEDAYS" SortExpression="DUEDAYS" />
                                                                    <asp:BoundField DataField="TOLARANCEDAYS" HeaderText="TOLARANCE DAYS" SortExpression="TOLARANCEDAYS" />
                                                                    <asp:BoundField DataField="DEPARTMENT" HeaderText="DEPARTMENT" SortExpression="DEPARTMENT" />
                                                                    <asp:BoundField DataField="OPERATINGRange" HeaderText="OPERATING Range" SortExpression="OPERATINGRange" />
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
                                                            <asp:Button ID="btnSave" runat="server" CssClass="Button" Text="Save" Width="60px"
                                                                OnClick="btnSave_Click" />
                                                            &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;
                                                            <asp:Button ID="btnCancel" runat="server" CssClass="Button" Text="Cancel" Width="60px"
                                                                OnClick="btnCancel_Click" />
                                                        </asp:Panel>
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
                                        <td height="30" style="height: 30px; background-image: url('file:///D:/Sayali/WIP%2018-07-2015/wip/App/App_Themes/Styles/Images/Menu_Out.png');
                                            background-repeat: repeat-x">
                                            <table cellpadding="5">
                                                <tr>
                                                    <td>
                                                        <asp:ImageButton ID="imgCloseV1" runat="server" Height="24px" ImageUrl="~/App_Themes/Styles/Images/exit.ico"
                                                            PostBackUrl="~/Modules/frmMain.aspx" ToolTip="Close Page" Width="24px" OnClick="imgCloseV1_Click" />
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
                                        <td height="30" style="height: 30px; background-image: url('file:///D:/Sayali/WIP%2018-07-2015/wip/App/App_Themes/Styles/Images/Menu_Out.png');
                                            background-repeat: repeat-x;">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblSearch" runat="server" CssClass="Label" Text="Search Criteria"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="cboSearch" runat="server" CssClass="Combobox">
                                                            <asp:ListItem Selected="True" Value="GS1_PREFIX">GS1 PREFIX</asp:ListItem>
                                                            <asp:ListItem Value="NAME">NAME</asp:ListItem>
                                                            <asp:ListItem Value="ADDRESS1">ADDRESS</asp:ListItem>
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
                                            <asp:GridView ID="GrPlant" runat="server" AllowPaging="True" AllowSorting="True"
                                                AutoGenerateColumns="False" CaptionAlign="Left" CellPadding="4" CssClass="GridViewStyle"
                                                EmptyDataText="There are no items to show in this view." DataKeyNames="REFNO"
                                                PageSize="20" ShowFooter="True" OnPageIndexChanging="GrPlant_PageIndexChanging"
                                                OnRowCommand="GrPlant_RowCommand" Width="100%">
                                                <Columns>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="LinkButton1" CommandName="Select" Text="Select" runat="server" /></ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="WEIGHINGCODE" HeaderText="WEIGHING CODE" SortExpression="WEIGHINGCODE" />
                                                    <asp:BoundField DataField="WEIGHINGIP" HeaderText="WEIGHING IP" SortExpression="WEIGHINGIP" />
                                                    <asp:BoundField DataField="WEIGHINGPORT" HeaderText="WEIGHING PORT" SortExpression="WEIGHINGPORT" />
                                                    <asp:BoundField DataField="WEIGHINGCAPACITY" HeaderText="WEIGHING CAPACITY" SortExpression="WEIGHINGCAPACITY" />
                                                    <asp:BoundField DataField="LEASTCOUNT" HeaderText="LEAST COUNT" SortExpression="LEASTCOUNT" />
                                                    <asp:BoundField DataField="MINVALUE" HeaderText="MIN VALUE" SortExpression="MINVALUE" />
                                                    <asp:BoundField DataField="MAXVALUE" HeaderText="MAX VALUE" SortExpression="MAXVALUE" />
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
            </td> </tr> </table> </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
