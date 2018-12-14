<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CardsInsert.aspx.cs" Inherits="FinalWebProject.App_Aspx.CardsInsert" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
<%--    <link rel="stylesheet" href="../CSS/CardsInsertStyle.css" />--%>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="nameLabel" runat="server" Text="Card name:" CssClass="Labels"></asp:Label>
            <asp:TextBox ID="cardNameTextBox" runat="server"  Height="20px" CssClass="Fields"></asp:TextBox>
            <asp:RequiredFieldValidator ID="cardNameRequiredFieldValidator" runat="server" ErrorMessage="card name required" ControlToValidate="cardNameTextBox" EnableClientScript="False" CssClass="auto-style1"></asp:RequiredFieldValidator>
            <br />
            <br />
            <asp:Label ID="abilityLabel" runat="server" Text="Card ability:" CssClass="Labels"></asp:Label>
            <asp:TextBox ID="abilityTextBox" runat="server" TextMode="MultiLine" Height="20px" CssClass="Fields"></asp:TextBox>
            <asp:RequiredFieldValidator ID="abilityRequiredFieldValidator" runat="server" ErrorMessage="card ability required" ControlToValidate="abilityTextBox" EnableClientScript="False" CssClass="auto-style1"></asp:RequiredFieldValidator>
            <br />
            <br />
            <asp:Label ID="manaCostLabel" runat="server" Text="Mana cost:" CssClass="Labels"></asp:Label>
            <asp:TextBox ID="manaCostTextBox" TextMode="Number" runat="server" CssClass="Fields"></asp:TextBox>
            <asp:RequiredFieldValidator ID="manaCostRequiredFieldValidator" runat="server" ErrorMessage="mana cost required" ControlToValidate="manaCostTextBox" EnableClientScript="False" CssClass="Validators"></asp:RequiredFieldValidator>
            <br />
            <br />
            <asp:Label ID="rarityLabel" runat="server" Text="Rarity:" CssClass="Labels"></asp:Label>
            <asp:DropDownList ID="raritiesDropDownList" runat="server" CssClass="Fields"></asp:DropDownList>
            <br />
            <br />
            <asp:Label ID="cardimageLabel" runat="server" Text="Image:" CssClass="Labels"></asp:Label>
            <asp:FileUpload ID="cardImage" runat="server" CssClass="Fields" />
            <br />
            <br />
            <asp:Button ID="insertButton" runat="server" Text="Insert Card"
                OnClick="insertClick" />
            <br />
        </div>
    </form>
</body>
</html>
