<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="FinalWebProject.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="LoginStyle.css" />
</head>
<body>
    <form id="form1" runat="server">
        <div>

            <asp:Label ID="userLabel" runat="server" Text="Username:"></asp:Label>
            <asp:TextBox ID="userTextBox" runat="server" Height="16px" Style="margin-bottom: 0px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="userRequired" ControlToValidate="userTextBox" EnableClientScript="false" runat="server" ErrorMessage="please enter user" ValidationGroup="loginValidation"></asp:RequiredFieldValidator>
            <br />
            <asp:Label ID="passwordLabel" runat="server" Text="Password:"></asp:Label>
            <asp:TextBox ID="passwordTextBox" runat="server" TextMode="Password"></asp:TextBox>
            <asp:RequiredFieldValidator ID="passwordRequired" ControlToValidate="passwordTextBox" EnableClientScript="false" runat="server" ErrorMessage="please enter password" ValidationGroup="loginValidation"></asp:RequiredFieldValidator>
            <br />
            <asp:Button ID="loginButton" runat="server" OnClick="LoginClick" Text="Login" ValidationGroup="loginValidation" CssClass="buttons" />
            <asp:Button ID="signUpButton" runat="server" OnClick="SignUpClick" Text="Sign Up" CssClass="buttons" />

        </div>
    </form>
</body>
</html>
