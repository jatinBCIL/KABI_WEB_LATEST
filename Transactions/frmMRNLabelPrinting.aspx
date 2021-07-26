<%@ Page Title="" Language="C#" MasterPageFile="~/Modules/MasterPage.master" AutoEventWireup="true"
    CodeFile="frmMRNLabelPrinting.aspx.cs" Inherits="frmMRNLabelPrinting" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../App_Themes/Styles/Style_Design.css" rel="stylesheet" type="text/css" />
    <link href="../App_Themes/Styles/Style_Controls.css" rel="stylesheet" type="text/css" />
    <link href="../App_Themes/Styles/Style_Grid.css" rel="stylesheet" type="text/css" />
    <script src="../App_Themes/Styles/jquery.min.js" type="text/javascript"></script>
    <script type="text/javascript">

        function isNumber(evt) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            return true;
        }

        function numericOnly(elementRef) {
            var keyCodeEntered = (event.which) ? event.which : (window.event.keyCode) ? window.event.keyCode : -1;
            if ((keyCodeEntered >= 48) && (keyCodeEntered <= 57)) {
                return true;
            }
            else if (keyCodeEntered == 46) {
                if ((elementRef.value) && (elementRef.value.indexOf('.') >= 0))
                    return false;
                else
                    return true;
            }
            return false;
        }
    </script>
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td height="30" style="height: 30px; background-image: url('../App_Themes/Styles/Images/Menu_Out.png');
                        background-repeat: repeat-x">
                        <asp:Label ID="Label8" runat="server" CssClass="HeadingLabel" Text=" Label Printing"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table cellpadding="5">
                            <tr>
                                <td>
                                    <table width="100%" style="border: 1px solid grey; padding: 5px">
                                        <tr valign="top">
                                            <td>
                                                <table>
                                                    <tr>
                                                        <td style="width: 30%; text-align: right; padding-bottom:20px">
                                                            <asp:CheckBox ID="chkMRN" runat="server" Text=" MRN" CssClass="Label" Style="margin-right: 120px;
                                                                font-size: 15px" OnCheckedChanged="chkMRN_CheckedChanged" AutoPostBack="true" />
                                                        </td>
                                                        <td style="padding-bottom:20px">
                                                            <asp:CheckBox ID="chkINTERMEDIATE" runat="server" Text=" INTERMEDIAE" CssClass="Label" 
                                                                Font-Size="15px" OnCheckedChanged="chkINTERMEDIATE_CheckedChanged" AutoPostBack="true" />
                                                        </td>
                                                        <td colspan="2" style="padding-bottom:20px">
                                                            <asp:CheckBox ID="chkCodetocode" runat="server" Text=" CODE TO CODE " CssClass="Label" 
                                                                Font-Size="15px" OnCheckedChanged="chkCodetocode_CheckedChanged" AutoPostBack="true" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label14" runat="server" CssClass="Label" Text="Material Doc No. :"
                                                                Width="220px"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtMaterialDoc" runat="server" SkinID="NAME" CssClass="TextBox"
                                                                Width="220px" MaxLength="500"></asp:TextBox>
                                                        </td>
                                                        <td valign="top" style="padding-left: 40px">
                                                            <asp:Button ID="btnGet" runat="server" CssClass="Button" Text="GET DATA FROM SAP"
                                                                Width="200px" ValidationGroup="Get" OnClick="btnGet_Click" />
                                                        </td>
                                                        <td>
                                                            <asp:RequiredFieldValidator ID="rfvPlantcode" runat="server" ControlToValidate="txtMaterialDoc"
                                                                ErrorMessage="Batch No. is required!!!" Text="&lt;img src='../App_Themes/Styles/Images/err1.png' title='Batch No.e is required!!!' /&gt;"
                                                                ToolTip="Batch No. is required!!!" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                                        </td>
                                                        <td>
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                </table>
                                                <table runat="server" id="MaterialData">
                                                    <tr>
                                                        <td style="padding-top: 5px">
                                                            <asp:Label ID="Label3" runat="server" CssClass="Label" Text="Select Material :" Width="220px"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlMaterialCode" runat="server" CssClass="Combobox" Width="220px"
                                                                InitialValue="Select">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td>
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="padding-top: 5px">
                                                            <asp:Label ID="Label1" runat="server" CssClass="Label" Text="Select Material Batch :"
                                                                Width="220px"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlMaterialBatch" runat="server" CssClass="Combobox" InitialValue="Select"
                                                                Width="220px" OnSelectedIndexChanged="ddlMaterialBatch_SelectedIndexChanged"
                                                                AutoPostBack="true">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td>
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="padding-top: 5px">
                                                            <asp:Label ID="Label10" runat="server" CssClass="Label" Text="UOM :" Width="220px"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="lblUom" runat="server" CssClass="Combobox" Width="220px">
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="lblUom"
                                                                ErrorMessage="RequiredFieldValidator" InitialValue="Select" ValidationGroup="PrintLabel"> </asp:RequiredFieldValidator>
                                                        </td>
                                                        <td>
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="padding-top: 5px">
                                                            <asp:Label ID="Label5" runat="server" CssClass="Label" Text="Total/Net Quantity :"
                                                                Width="220px"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="lblTotalQuantity" runat="server" CssClass="Combobox" Width="220px">
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="lblTotalQuantity"
                                                                ErrorMessage="RequiredFieldValidator" InitialValue="Select" ValidationGroup="PrintLabel"> </asp:RequiredFieldValidator>
                                                        </td>
                                                        <td>
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="padding-top: 5px">
                                                            <asp:Label ID="Label11" runat="server" CssClass="Label" Text="No. of Containers :"
                                                                Width="220px"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtNoofContainers" runat="server" CssClass="Label" Width="210px"
                                                                Text="1" onkeypress="return numericOnly(this);" ></asp:TextBox>
                                                            <asp:Image ID="Image5" runat="server" Height="10px" ImageUrl="../App_Themes/Styles/Images/img_mand.png"
                                                                ToolTip="Mandatory Field!!!" Width="10px" />
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtNoofContainers"
                                                                ErrorMessage="RequiredFieldValidator" ToolTip="NoofContainers is required" ValidationGroup="DataSave"><img 
                                                                src="../App_Themes/Styles/Images/err1.png" />
                                                            </asp:RequiredFieldValidator>
                                                        </td>
                                                        <td>
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="padding-top: 5px">
                                                            <asp:Label ID="Label7" runat="server" CssClass="Label" Text="Container Qty :" Width="220px"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtContainerQty" runat="server" CssClass="Label" Width="210px" Text=""
                                                                onkeypress="return numericOnly(this);"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtNoofContainers"
                                                                ErrorMessage="RequiredFieldValidator" ToolTip="ContainerQty is required" ValidationGroup="DataSave"><img 
                                                                src="../App_Themes/Styles/Images/err1.png"  />
                                                            </asp:RequiredFieldValidator>
                                                        </td>
                                                        <td>
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                     <tr>
                                                        <td style="padding-top: 5px">
                                                            <asp:Label ID="Label19" runat="server" CssClass="Label" Text="Vendor Batch No :" Width="220px"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtVendor" runat="server" CssClass="Label" Width="210px" Text=""
                                                              ></asp:TextBox>
                                                          
                                                        </td>
                                                        <td>
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="padding-top: 5px">
                                                            <asp:Label ID="Label17" runat="server" CssClass="Label" Text="Manufacture Name :" Width="220px"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtManufacture" runat="server" CssClass="Label" Width="210px" Text=""
                                                              ></asp:TextBox>
                                                          
                                                        </td>
                                                        <td>
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="padding-top: 5px">
                                                            <asp:Label ID="Label18" runat="server" CssClass="Label" Text="Supplier Name :" Width="220px"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtSupplier" runat="server" CssClass="Label" Width="210px" Text=""
                                                               ></asp:TextBox>
                                                           
                                                        </td>
                                                        <td>
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="padding-top: 5px">
                                                            <asp:Label ID="Label13" runat="server" CssClass="Label" Text="Mgf Date :" Width="220px"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtmfgdate" runat="server" CssClass="Label" Width="210px" Text="">
                                                            </asp:TextBox>
                                                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" TargetControlID="txtmfgdate"
                                                                Format="dd-MM-yyyy">
                                                            </asp:CalendarExtender>
                                                            <asp:Label ID="Label65" runat="server" CssClass="Label" Text="(If not Required Keep BLANK) "
                                                                BackColor="Yellow"></asp:Label>
                                                        </td>
                                                        <td>
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="padding-top: 5px">
                                                            <asp:Label ID="Label15" runat="server" CssClass="Label" Text="Exp Date :" Width="220px"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtexpate" runat="server" CssClass="Label" Width="210px" Text="">
                                                            </asp:TextBox>
                                                            <asp:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True" TargetControlID="txtexpate"
                                                                Format="dd-MM-yyyy">
                                                            </asp:CalendarExtender>
                                                            <asp:Label ID="Label16" runat="server" CssClass="Label" Text="(If not Required Keep BLANK) "
                                                                BackColor="Yellow"></asp:Label>
                                                        </td>
                                                        <td>
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="padding-top: 5px">
                                                            <asp:Label ID="Label4" runat="server" CssClass="Label" Text="AR No :" Width="220px"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="lblARNo" runat="server" Width="210px" CssClass="Label">
                                                            </asp:TextBox>
                                                        </td>
                                                        <td>
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr id="divdetails" runat="server" visible="false">
                                                        <td colspan="3">
                                                            <table>
                                                                <tr>
                                                                    <td style="padding-top: 5px">
                                                                        <asp:Label ID="Label6" runat="server" CssClass="Label" Text="Gross Wt :" Width="220px"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtgrossWt" runat="server" CssClass="Label" Width="210px" Text=""
                                                                            onkeypress="return numericOnly(this);"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        &nbsp;
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="padding-top: 5px">
                                                                        <asp:Label ID="Label12" runat="server" CssClass="Label" Text="Tare Wt :" Width="220px"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtTarewt" runat="server" CssClass="Label" Text="" Width="210px"
                                                                            onkeypress="return numericOnly(this);"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        &nbsp;
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="padding-top: 5px">
                                                                        <asp:Label ID="Label9" runat="server" CssClass="Label" Text="Storage Location :"
                                                                            Width="220px"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="lblStrgLoc" runat="server" CssClass="Label" Width="210px" Text=""></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        &nbsp;
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="padding-top: 5px">
                                                                        <asp:Label ID="product" runat="server" CssClass="Label" Text="Product Code :" Width="220px"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="lblProductCode" runat="server" CssClass="Label" Width="210px" Text=""></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        &nbsp;
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="padding-top: 5px">
                                                                        <asp:Label ID="Label2" runat="server" CssClass="Label" Text="Product Description:"
                                                                            Width="220px"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="lblProdDescription" runat="server" CssClass="Label" Width="210px"
                                                                            Text=""></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        &nbsp;
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="padding-top: 5px">
                                                            <asp:Label ID="lblPrintername" runat="server" CssClass="Label" Text="Printer&amp;nbsp;Name:"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlPrinterName" runat="server" CssClass="Combobox" Width="220px">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td class="style18">
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlPrinterName"
                                                                ErrorMessage="RequiredFieldValidator" InitialValue="Select" ValidationGroup="PrintLabel">
                                                                <img src="../App_Themes/Styles/Images/err1.png" 
                                                                title="Printer Name is required!!!" /></asp:RequiredFieldValidator>
                                                        </td>
                                                        <td valign="top">
                                                        </td>
                                                        <td>
                                                        </td>
                                                        <td>
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <td>
                                                                <asp:Button ID="btnPrint" runat="server" CssClass="Button" Text="Print" Width="75px"
                                                                    ValidationGroup="PrintLabel" OnClick="btnPrint_Click" />
                                                            </td>
                                                        </td>
                                                        <td>
                                                        </td>
                                                        <td>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnGet" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
