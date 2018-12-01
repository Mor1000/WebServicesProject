<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CardsInsert.aspx.cs" Inherits="FinalWebProject.App_Aspx.CardsInsert" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
      <asp:Label ID="cardNameLabel" runat="server" Text="Card name:"></asp:Label>
        <asp:TextBox ID="cardNameTextBox" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="nameRequiredFieldValidator" runat="server" ErrorMessage="card name required" ControlToValidate="cardNameTextBox" EnableClientScript="False"></asp:RequiredFieldValidator>
        <br />
        <asp:Label ID="abilityLabel" runat="server" Text="Card ability:"></asp:Label>
        <asp:TextBox ID="abilityTextBox" runat="server" TextMode="MultiLine" Height="20px"></asp:TextBox>
        <asp:RequiredFieldValidator ID="abilityRequiredFieldValidator0" runat="server" ErrorMessage="card ability required" ControlToValidate="abilityTextBox" EnableClientScript="False"></asp:RequiredFieldValidator>
        <br />
        <asp:Label ID="manaCostLabel" runat="server" Text="Mana cost:"></asp:Label>
        <asp:TextBox ID="manaCostTextBox" TextMode="Number" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="abilityRequiredFieldValidator1" runat="server" ErrorMessage="mana cost required" ControlToValidate="manaCostTextBox" EnableClientScript="False"></asp:RequiredFieldValidator>
        <br />
        <asp:Label ID="rarityLabel" runat="server" Text="Rarity:"></asp:Label>
        <asp:TextBox ID="rarityTextBox" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="abilityRequiredFieldValidator2" runat="server" ErrorMessage="rarity required" ControlToValidate="rarityTextBox" EnableClientScript="False"></asp:RequiredFieldValidator>
        <br />
        <asp:Label ID="cardimageLabel" runat="server" Text="Image:"></asp:Label>
        <asp:FileUpload ID="cardImage" runat="server" />
        <br />
        <asp:Button ID="insertButton" runat="server" Text="Insert Card" 
            onclick="insertClick" />
    </div>
    </form>
</body>
</html>
