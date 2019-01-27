<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShowCardColors.aspx.cs" Inherits="FinalWebProject.App_Aspx.ShowCardColors" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="colorsLabel" runat="server" Text="This card colors:"></asp:Label>
            <asp:CheckBoxList ID="colorsList" runat="server" DataSourceID="accessDataColors" DataTextField="colorName" AutoPostBack="true" DataValueField="colorId" OnDataBound="OnColorBound" OnSelectedIndexChanged="OnColorIndexChanged">
            </asp:CheckBoxList>
            <asp:AccessDataSource ID="accessDataColors" runat="server" DataFile="~/App_Data/projectDatabase1.accdb" SelectCommand="SELECT * FROM [Colors]"></asp:AccessDataSource>
            <asp:Label ID="kindLabel" runat="server" Text="Type:" CssClass="Labels"></asp:Label>
            <asp:CheckBoxList ID="kindsList" runat="server" AutoPostBack="True" CssClass="Fields" DataSourceID="kindsDataSource" DataTextField="kindName" DataValueField="kindId" OnSelectedIndexChanged="KindSelectionChanged" OnDataBound="OnKindsBound"></asp:CheckBoxList>
            <asp:CustomValidator ID="kindsListCustomValidator" EnableClientScript="false" runat="server" ErrorMessage="CustomValidator" OnServerValidate="KindsListChecked" ></asp:CustomValidator>
            <asp:AccessDataSource ID="kindsDataSource" runat="server" DataFile="~/App_Data/projectDatabase1.accdb" SelectCommand="SELECT * FROM [CardKinds]"></asp:AccessDataSource>
            <asp:Label ID="powerLabel" Visible="false" runat="server" Text="Power"></asp:Label>
            <asp:TextBox ID="powerTextBox" Visible="false" runat="server" TextMode="Number"></asp:TextBox>
            <asp:Label ID="toughnessLabel" Visible="false" runat="server" Text="toughnes"></asp:Label>
            <asp:TextBox ID="toughnessTextBox" Visible="false" runat="server" TextMode="Number"></asp:TextBox>
            <asp:RequiredFieldValidator ID="powerRequiredFieldValidator" runat="server" ErrorMessage="card power required" ControlToValidate="powerTextBox" EnableClientScript="False" CssClass="auto-style1" ValidationGroup="AttributesValidation"></asp:RequiredFieldValidator>
            <asp:RequiredFieldValidator ID="toughnessRequiredFieldValidator" runat="server" ErrorMessage="card toughness required" ControlToValidate="toughnessTextBox" EnableClientScript="False" CssClass="auto-style1" ValidationGroup="AttributesValidation"></asp:RequiredFieldValidator>
            <asp:Button ID="returnButton" runat="server" Text="Return" OnClick="OnReturn" />
            <asp:Button ID="saveButton" runat="server" Text="Save" OnClick="OnSave" />

        </div>
    </form>
</body>
</html>
