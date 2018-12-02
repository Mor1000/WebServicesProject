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
        <br />
        <asp:Label ID="abilityLabel" runat="server" Text="Card ability:"></asp:Label>
        <asp:TextBox ID="abilityTextBox" runat="server" TextMode="MultiLine" Height="20px"></asp:TextBox>
        <asp:RequiredFieldValidator ID="abilityRequiredFieldValidator" runat="server" ErrorMessage="card ability required" ControlToValidate="abilityTextBox" EnableClientScript="False"></asp:RequiredFieldValidator>
        <br />
        <br />
        <asp:Label ID="manaCostLabel" runat="server" Text="Mana cost:"></asp:Label>
        <asp:TextBox ID="manaCostTextBox" TextMode="Number" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="manaCostRequiredFieldValidator" runat="server" ErrorMessage="mana cost required" ControlToValidate="manaCostTextBox" EnableClientScript="False"></asp:RequiredFieldValidator>
        <br />
        <br />
        <asp:Label ID="rarityLabel" runat="server" Text="Rarity:"></asp:Label>
        <asp:DropDownList ID="raritiesDropDownList" runat="server"></asp:DropDownList>
        <br />
        <br />
        <asp:Label ID="cardimageLabel" runat="server" Text="Image:"></asp:Label>
        <asp:FileUpload ID="cardImage" runat="server" />
        <br />
        <br />
        <asp:Button ID="insertButton" runat="server" Text="Insert Card" 
            onclick="insertClick" />
        <br />
    </div>
        <asp:RangeValidator ID="manaCostRangeValidator" runat="server" ErrorMessage="Mana cost is out of range." MaximumValue="100" MinimumValue="0" ControlToValidate="manaCostTextBox" EnableClientScript="False"></asp:RangeValidator>
    </form>
</body>
</html>
