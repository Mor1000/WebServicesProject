<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CardsGridView.aspx.cs" Inherits="FinalWebProject.App_Aspx.CardsGridView" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="../CSS/CardsGridStyle.css" />
    <style type="text/css">
        .auto-style1 {
            margin-right: 0px;
        }
    </style>
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
        <asp:CheckBoxList ID="colorsList" runat="server" DataSourceID="colorsDataSource" DataTextField="colorName" DataValueField="colorId" OnDataBound="OnColorBound" RepeatDirection="Horizontal">
        </asp:CheckBoxList>
        <asp:AccessDataSource ID="colorsDataSource" runat="server" DataFile="~/App_Data/projectDatabase1.accdb" SelectCommand="SELECT * FROM [Colors]"></asp:AccessDataSource>
        <br />
        <asp:Label ID="kindLabel" runat="server" Text="Type:"></asp:Label>
        <asp:CheckBoxList ID="kindsList" runat="server" AutoPostBack="True" DataSourceID="kindsDataSource" DataTextField="kindName" DataValueField="kindId" OnSelectedIndexChanged="KindSelectionChanged" RepeatDirection="Horizontal"></asp:CheckBoxList>
        <asp:AccessDataSource ID="kindsDataSource" runat="server" DataFile="~/App_Data/projectDatabase1.accdb" SelectCommand="SELECT * FROM [CardKinds]"></asp:AccessDataSource>
        <br />
        <asp:GridView ID="magicCardsGridView" runat="server" BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="2" AutoGenerateColumns="False" OnRowCommand="CardsGridClick" OnRowCancelingEdit="OnCancelingEditing" OnRowEditing="OnEditMode" OnRowDeleting="OnRowDeleting" OnRowUpdating="OnRowUpdating" CssClass="auto-style1" OnRowDataBound="OnRowDataBound">
            <Columns>
                <asp:BoundField DataField="cardId" ReadOnly="true" HeaderText="Card Id" ItemStyle-CssClass="CardIdCol" HeaderStyle-CssClass="CardIdCol">
                    <HeaderStyle CssClass="CardIdCol"></HeaderStyle>

                    <ItemStyle CssClass="CardIdCol"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="cardName" HeaderText="Card Name" />
                <asp:BoundField DataField="cardAbility" HeaderText="Card Ability" />
                <asp:BoundField DataField="cardManaCost" HeaderText="Card Mana Cost" />
                <asp:TemplateField HeaderText="Card Rarity" SortExpression="cardRarities">
                    <EditItemTemplate>

                        <asp:DropDownList ID="DropDownList1" runat="server">
                        </asp:DropDownList>

                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("rarityCardName") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:ButtonField HeaderText="Info" ButtonType="Button" Text="Show" CommandName="showColors" />
                <asp:TemplateField HeaderText="Card Image">
                    <EditItemTemplate>
                        <asp:FileUpload ID="cardImageFileUpload" runat="server" />
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Image Height="70px" Width="70px" runat="server" ImageUrl='<%# Bind("cardImage") %>' AlternateText="Image not found" ID="cardImage" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:CommandField HeaderText="Edit" ShowDeleteButton="True" ShowEditButton="True" ShowHeader="True" />

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
