<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CardAttributes.aspx.cs" Inherits="FinalWebProject.App_Aspx.CardAttributes" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="cardLabel" runat="server" Text="Card:"></asp:Label>
            <asp:DropDownList ID="cardDropDownList" runat="server"></asp:DropDownList>
            <br />
            <asp:Label ID="powerLabel" runat="server" Text="Power:"></asp:Label>
            <asp:TextBox ID="powerTextBox" TextMode="Number" runat="server"></asp:TextBox>
            <br />
            <asp:Label ID="toughnessLabel" runat="server" Text="Toughness:"></asp:Label>
            <asp:TextBox ID="toughnessTextBox" TextMode="Number" runat="server"></asp:TextBox>
            <br />
            <asp:Button ID="insertButton" runat="server" Text="Button" OnClick="OnInsert" />
        </div>
    </form>
</body>
</html>
