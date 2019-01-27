<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CardsInsert.aspx.cs" Inherits="FinalWebProject.App_Aspx.CardsInsert" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <%--    <link rel="stylesheet" href="../CSS/CardsInsertStyle.css" />--%>
    <style type="text/css">
        .auto-style1 {
            margin-top: 0px;
        }
    </style>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="Label1" runat="server" Text="Magic Insert"></asp:Label>
            <br />
            <asp:Label ID="nameLabel" runat="server" Text="Card name:" CssClass="Labels"></asp:Label>
            <asp:TextBox ID="cardNameTextBox" runat="server" Height="20px" CssClass="Fields"></asp:TextBox>
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
            <asp:Label ID="kindLabel" runat="server" Text="Type:" CssClass="Labels"></asp:Label>
            <asp:CheckBoxList ID="kindsList" runat="server" AutoPostBack="True" CssClass="Fields" DataSourceID="kindsDataSource" DataTextField="kindName" DataValueField="kindId" OnSelectedIndexChanged="KindSelectionChanged"></asp:CheckBoxList>
            <asp:AccessDataSource ID="kindsDataSource" runat="server" DataFile="~/App_Data/projectDatabase1.accdb" SelectCommand="SELECT * FROM [CardKinds]"></asp:AccessDataSource>
            <asp:Label ID="powerLabel" Visible="false" runat="server" Text="Power"></asp:Label>
            <asp:TextBox ID="powerTextBox" Visible="false" runat="server"></asp:TextBox>
            <asp:Label ID="toughnessLabel" Visible="false" runat="server" Text="toughnes"></asp:Label>
            <asp:TextBox ID="toughnesBox" Visible="false" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="powerRequiredFieldValidator" runat="server" ErrorMessage="card power required" ControlToValidate="powerTextBox" EnableClientScript="False" CssClass="auto-style1"></asp:RequiredFieldValidator>
            <asp:RequiredFieldValidator ID="toughnessRequiredFieldValidator" runat="server" ErrorMessage="card toughness required" ControlToValidate="toughnesBox" EnableClientScript="False" CssClass="auto-style1"></asp:RequiredFieldValidator>
            <br />
            <asp:Label ID="cardimageLabel" runat="server" Text="Image:" CssClass="Labels"></asp:Label>
            <asp:FileUpload ID="cardImage" runat="server" CssClass="Fields" />
            <br />
            <br />
            <asp:CheckBoxList ID="colorsList" runat="server" DataSourceID="accessDataColors" DataTextField="colorName" DataValueField="colorId" OnDataBound="OnColorBound">
            </asp:CheckBoxList>
            <asp:AccessDataSource ID="accessDataColors" runat="server" DataFile="~/App_Data/projectDatabase1.accdb" SelectCommand="SELECT * FROM [Colors]"></asp:AccessDataSource>
            <br />
        </div>
        <p>
            <asp:Button ID="insertButton" runat="server" Text="Insert Card"
                OnClick="insertClick" />
        </p>
    </form>
</body>
</html>
