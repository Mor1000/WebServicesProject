<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserCardsInsert.aspx.cs" Inherits="FinalWebProject.App_Aspx.UserCardsInsert" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            margin-bottom: 0px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Label ID="userLabel" runat="server" Text="User name:"></asp:Label>
        <asp:DropDownList ID="userDropDownList" runat="server"></asp:DropDownList>
        <br />
        <asp:Label ID="cardLabel" runat="server" Text="Card:"></asp:Label>
        <asp:DropDownList ID="cardDropDownList" runat="server"></asp:DropDownList>
        <br />
        <asp:Label ID="amountLabel" runat="server" Text="Amount:"></asp:Label>
        <asp:TextBox ID="amountTextBox" TextMode="Number" runat="server"></asp:TextBox>
        <br />
        <asp:Button ID="insertButton" runat="server" Text="Insert" OnClick="OnInsert" CssClass="auto-style1" />
    </form>
</body>
</html>
