<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DecksInsert.aspx.cs" Inherits="FinalWebProject.App_Aspx.DecksInsert" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="nameLabel" runat="server" Text="Deck name:"></asp:Label>
            <asp:TextBox ID="nameTextBox" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="nameRequiredFieldValidator" runat="server" ErrorMessage="Deck Name Required" ControlToValidate="nameTextBox" EnableClientScript="False"></asp:RequiredFieldValidator>
            <br />
            <br />
            <asp:Label ID="formatLabel" runat="server" Text="Format:"></asp:Label>
            <asp:DropDownList ID="formatsDropDownList" runat="server"></asp:DropDownList>
            <br />
            <br />
            <asp:Label ID="descriptionLabel" runat="server" Text="Description:"></asp:Label>
            <asp:TextBox ID="descriptionTextBox" TextMode="MultiLine" runat="server"></asp:TextBox>
            <br />
        </div>
        <asp:Button ID="insertButton" runat="server" OnClick="OnInsert" Text="Insert" />
    </form>
</body>
</html>
