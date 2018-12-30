<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CardsGridView.aspx.cs" Inherits="FinalWebProject.App_Aspx.CardsGridView" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="../CSS/CardsGridStyle.css" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:Label ID="nameLabel" runat="server" Text="name"></asp:Label>
        <asp:TextBox ID="nameTextBox" runat="server"></asp:TextBox>
        <asp:Label ID="abilityLabel" runat="server" Text="ability"></asp:Label>
        <asp:TextBox ID="abilityTextBox" runat="server"></asp:TextBox>
        <asp:Label ID="manaCostLabel" runat="server" Text="mana cost"></asp:Label>
        <asp:TextBox ID="manaCostTextBox" runat="server" TextMode="Number"></asp:TextBox>
        <asp:Label ID="rarityLabel" runat="server" Text="rarity"></asp:Label>
        <asp:DropDownList ID="rarityDropDownList" runat="server">
            <asp:ListItem>None</asp:ListItem>
        </asp:DropDownList>
        <asp:CheckBoxList ID="colorsList" runat="server" DataSourceID="ColorsDs" DataTextField="colorName" DataValueField="colorId" OnDataBound="OnColorBound">
        </asp:CheckBoxList>
           <asp:SqlDataSource ID="ColorsDs" runat="server" ConnectionString="<%$ ConnectionStrings:DBconnection %>" ProviderName="<%$ ConnectionStrings:DBconnection.ProviderName %>" SelectCommand="SELECT [colorId], [colorName] FROM [Colors]"></asp:SqlDataSource>
            <br />
        <br />
        <asp:GridView ID="magicCardsGridView" runat="server" BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="2" AutoGenerateColumns="False" OnRowCommand="CardsGridClick">
            <Columns>
                <asp:BoundField DataField="cardId" HeaderText="Card Id" ItemStyle-CssClass="CardIdCol" HeaderStyle-CssClass="CardIdCol" />
                <asp:BoundField DataField="cardName" HeaderText="Card Name" />
                <asp:BoundField DataField="cardAbility" HeaderText="Card Ability" />
                <asp:BoundField DataField="cardManaCost" HeaderText="Card Mana Cost" />
                <asp:BoundField DataField="cardRarity" Visible="false" HeaderText="Card Rarity" />
                <asp:BoundField DataField="rarityCardName" HeaderText="Card Rarity Name" />
                <asp:BoundField DataField="cardColorName" HeaderText="Colors" />
                <asp:ButtonField ButtonType="Button" CommandName="showColors" HeaderText="Show colors" Text="Show colors" />
                <asp:TemplateField HeaderText="Card Image">
                    <ItemTemplate>
                        <asp:Image Height="70px" Width="70px" runat="server" ImageUrl='<%# Bind("cardImage") %>' AlternateText="Image not found" ID="cardImage" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
            <HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="White" />
            <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
            <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
            <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#FFF1D4" />
            <SortedAscendingHeaderStyle BackColor="#B95C30" />
            <SortedDescendingCellStyle BackColor="#F1E5CE" />
            <SortedDescendingHeaderStyle BackColor="#93451F" />
        </asp:GridView>
        <asp:Button ID="searchButton" runat="server" OnClick="OnSearch" Text="Search" />
        <asp:Button ID="resetButton" runat="server" OnClick="OnReset" Text="Reset" />
    </form>
</body>
</html>
