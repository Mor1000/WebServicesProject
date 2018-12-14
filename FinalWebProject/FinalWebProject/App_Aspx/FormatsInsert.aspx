<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FormatsInsert.aspx.cs" Inherits="FinalWebProject.App_Aspx.FormatsInsert" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Label ID="nameLabel" runat="server" Text="Format name"></asp:Label>
        <asp:TextBox ID="nameTextBox" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="nameRequiredFieldValidator" runat="server" ErrorMessage="name required" ControlToValidate="nameTextBox" EnableClientScript="False"></asp:RequiredFieldValidator>
        <asp:Button ID="insertButton" runat="server" Text="Insert" OnClick="OnInsert" />
    </div>
    </form>
</body>
</html>
