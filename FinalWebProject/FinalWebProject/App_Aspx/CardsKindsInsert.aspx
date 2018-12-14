﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CardsKindsInsert.aspx.cs" Inherits="FinalWebProject.App_Aspx.CardsKindsInsert" %>

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
        <asp:Label ID="kindLabel" runat="server" Text="Kind:"></asp:Label>
        <asp:DropDownList ID="kindDropDownList" runat="server"></asp:DropDownList>
        <br />
        <asp:Button ID="insertButton" runat="server" Text="Insert" OnClick="OnInsert" />
    </div>
    </form>
</body>
</html>