<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="FinalWebProject.App_Aspx.Register" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
      <title></title>
 <link rel="stylesheet" href="../CSS/RegisterStyle.css" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:TextBox ID="userTextBox" placeHolder="user name" runat="server" CssClass="accountTextboxes"></asp:TextBox>
            <asp:RequiredFieldValidator ID="userRequiredFieldValidator" EnableClientScript="false" ControlToValidate="userTextBox" runat="server" ValidationGroup="signUp" CssClass="requirements">*user required</asp:RequiredFieldValidator>
            <br />

            <asp:TextBox ID="passwordTextBox" placeHolder="password" TextMode="Password" runat="server" CssClass="accountTextboxes"></asp:TextBox>
            <asp:RequiredFieldValidator ID="passwordRequiredFieldValidator" EnableClientScript="false" ControlToValidate="passwordTextBox" runat="server" ValidationGroup="signUp" CssClass="requirements">*password required</asp:RequiredFieldValidator>
            <br />

            <asp:TextBox ID="confirmPasswordTextBox" placeHolder="confirm password" TextMode="Password" runat="server" CssClass="accountTextboxes"></asp:TextBox>
            <asp:RequiredFieldValidator ID="confirmPasswordRequiredFieldValidator" EnableClientScript="false" ControlToValidate="confirmPasswordTextBox" runat="server" ValidationGroup="signUp" CssClass="requirements">*confirm password required</asp:RequiredFieldValidator>
            <br />

            <asp:TextBox ID="emailTextBox" placeHolder="email" runat="server" CssClass="accountTextboxes" TextMode="Email"></asp:TextBox>
            <asp:RequiredFieldValidator ID="emailRequiredFieldValidator" EnableClientScript="false" ControlToValidate="emailTextBox" runat="server" ValidationGroup="signUp" CssClass="requirements">*email required</asp:RequiredFieldValidator>
            <br />

            <asp:TextBox ID="birthDateTextBox" placeHolder="birthdate" TextMode="Date" runat="server" CssClass="accountTextboxes"></asp:TextBox>
            <asp:RequiredFieldValidator ID="birthdateRequiredFieldValidator" EnableClientScript="false" ControlToValidate="birthDateTextBox" runat="server" ValidationGroup="signUp" CssClass="requirements">*birthdate required</asp:RequiredFieldValidator>
            <br />
            <asp:DropDownList ID="countriesDropDownList" runat="server" CssClass="accountTextboxes"></asp:DropDownList>
            <asp:RequiredFieldValidator ID="countryRequiredFieldValidator" runat="server" ControlToValidate="countriesDropDownList" EnableClientScript="False" CssClass="requirements">*Country Required</asp:RequiredFieldValidator>
            <br />

            <asp:TextBox ID="arenaNameTextBox" placeHolder="MTG arena name" runat="server" CssClass="accountTextboxes"></asp:TextBox>
            <br />
            <asp:RangeValidator ID="rangeValidator" EnableClientScript="false" ControlToValidate="birthDateTextBox" runat="server" ErrorMessage="date is out of range" Type="Date" MinimumValue="02/10/1999" MaximumValue="24/10/2007" ValidationGroup="signUp" CssClass="validations"></asp:RangeValidator>
            <br />
            <asp:CustomValidator ID="customValidatorPassword" ControlToValidate="passwordTextBox" EnableClientScript="false" OnServerValidate="CheckChars" runat="server" ErrorMessage="password should have more than 4 chars" ControlToCompare="passwordTextBox" ValidationGroup="signUp" CssClass="validations"></asp:CustomValidator>
            <br />
            <asp:CustomValidator ID="customValidatorChars" runat="server" OnServerValidate="CheckChars" ErrorMessage="user should have more than 4 chars" EnableClientScript="False" ControlToValidate="userTextBox" ValidationGroup="signUp" CssClass="validations"></asp:CustomValidator>
            <br />
            <asp:CompareValidator ID="passwordsCompareValidator" runat="server" ControlToValidate="confirmPasswordTextBox" ControlToCompare="passwordTextBox" EnableClientScript="False" ValidationGroup="signUp" ErrorMessage="passwords don't match" CssClass="validations"></asp:CompareValidator>
            <br />
            <asp:RegularExpressionValidator ID="emailRegularExpressionValidator" EnableClientScript="false" ControlToValidate="emailTextBox" runat="server" ErrorMessage="email not valid" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="signUp" CssClass="validations"></asp:RegularExpressionValidator>
            <br />
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
            <asp:Button ID="signUpButton" runat="server" OnClick="SignUpClick" Text="Sign Up!" ValidationGroup="signUp" Height="30px" Style="margin-top: 0px" UseSubmitBehavior="False" Width="80px" CssClass="buttons" />
            <asp:Button ID="clearButton" runat="server" OnClick="OnClearClick" Text="Clear All" Height="30px"  Width="80px" CssClass="buttons" />
        </div>
    </form>
</body>
</html>
