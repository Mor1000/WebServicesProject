<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserCardsGridView.aspx.cs" Inherits="FinalWebProject.App_Aspx.UserCardsGridView" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="userLabel" runat="server" Text="User:"></asp:Label>
            <asp:TextBox ID="userTextBox" runat="server"></asp:TextBox>
            <asp:Label ID="cardLabel" runat="server" Text="Card:"></asp:Label>
            <asp:DropDownList ID="cardsDropDownList" runat="server">
                <asp:ListItem Value="None">None</asp:ListItem>
            </asp:DropDownList>
            <asp:Label ID="amountLabel" runat="server" Text="Amount:"></asp:Label>
            <asp:TextBox ID="amountTextBox" runat="server" TextMode="Number"></asp:TextBox>
            <asp:GridView ID="usersCardsGridView" runat="server" AutoGenerateColumns="False">
                <Columns>
                    <asp:BoundField DataField="userName" HeaderText="User" />
                    <asp:BoundField DataField="userCard" HeaderText="Card" />
                    <asp:BoundField DataField="cardAmount" HeaderText="Amount" />
                </Columns>
            </asp:GridView>
        </div>
        <asp:Button ID="Button1" runat="server" OnClick="OnSearch" Text="Search" />
        <asp:Button ID="resetButton" runat="server" OnClick="OnReset" Text="Reset" />
    </form>
</body>
</html>
