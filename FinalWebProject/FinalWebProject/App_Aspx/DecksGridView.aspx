<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DecksGridView.aspx.cs" Inherits="FinalWebProject.App_Aspx.DecksGridView" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Label ID="nameLabel" runat="server" Text="Name:"></asp:Label>
        <asp:DropDownList ID="nameDropDownList" runat="server">
            <asp:ListItem>None</asp:ListItem>
        </asp:DropDownList>
        <asp:Label ID="formatLabel" runat="server" Text="Format:"></asp:Label>
        <asp:DropDownList ID="formatDropDownList" runat="server">
            <asp:ListItem>None</asp:ListItem>
        </asp:DropDownList>
        <asp:Label ID="minDateLabel" runat="server" Text="From:"></asp:Label>
        <asp:TextBox ID="minDateTextBox" TextMode="Date" runat="server"></asp:TextBox>
         <asp:Label ID="maxDateLabel" runat="server" Text="To:"></asp:Label>
        <asp:TextBox ID="maxDateTextBox" TextMode="Date" runat="server"></asp:TextBox>
        <asp:Label ID="descriptionLabel" runat="server" Text="Description"></asp:Label>
        <asp:TextBox ID="descriptionTextBox" runat="server"></asp:TextBox>
        <asp:GridView ID="deckGridView" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4">
            <Columns>
                <asp:BoundField DataField="deckId" HeaderText="Deck ID" />
                <asp:BoundField DataField="deckName" HeaderText="Deck Name" />
                <asp:BoundField DataField="deckFormat" HeaderText="Format" />
                <asp:BoundField DataField="deckCreationDate" HeaderText="Creation Date" />
                <asp:BoundField DataField="deckDescription" HeaderText="Description:" />
            </Columns>
            <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
            <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="#FFFFCC" />
            <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
            <RowStyle BackColor="White" ForeColor="#330099" />
            <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
            <SortedAscendingCellStyle BackColor="#FEFCEB" />
            <SortedAscendingHeaderStyle BackColor="#AF0101" />
            <SortedDescendingCellStyle BackColor="#F6F0C0" />
            <SortedDescendingHeaderStyle BackColor="#7E0000" />
        </asp:GridView>
        <asp:Button ID="searchButton" runat="server" OnClick="OnSearch" Text="Search" />
        <asp:Button ID="resetButton" runat="server" OnClick="OnReset" Text="Reset" />
    </div>
    </form>
</body>
</html>
