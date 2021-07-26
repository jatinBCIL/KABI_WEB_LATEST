<%@ Control Language="C#" AutoEventWireup="true" CodeFile="LogIn.ascx.cs" Inherits="Controls_LogIn" %>
<body style="margin:0;width:300px; height:40">
<table style="width:300px; height:40" cellspacing="0">
        <tr>
            <td style="text-align: center; background-image: url('Styles/Images/User_Back.png'); background-repeat: no-repeat;" 
                align="center">
                <asp:Label ID="Label7" runat="server" CssClass="TitleLabel" 
                    Text="Welcome Administrator"></asp:Label>
            </td>
            <td style="text-align: center; background-color: #FF0000;">
                <asp:LinkButton ID="LinkButton1" runat="server" CssClass="LinkButton">Logout</asp:LinkButton>
            </td>
        </tr>
    </table>
    </body>