<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FormatsPage.aspx.cs" Inherits="FinalWebProject.App_Aspx.FormatsPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Label ID="formatNameLabel" runat="server" Text="Format Name"></asp:Label>
        <asp:TextBox ID="formatNameTextBox" runat="server"></asp:TextBox>
        <asp:Button ID="searchButton" runat="server" Text="Search" OnClick="OnSearch" />
        <asp:GridView ID="formatsGridView" runat="server" AutoGenerateColumns="False" OnRowCancelingEdit="OnCancelingEdit" OnRowDeleting="OnRowDeleting" OnRowEditing="RowEditingMode" OnRowUpdating="OnRowUpdating">
            <Columns>
                <asp:BoundField DataField="formatId" HeaderText="Format ID" />
                <asp:BoundField DataField="formatName" HeaderText="Format Name" />
                <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
            </Columns>
        </asp:GridView>
    </div>
        <asp:Button ID="resetButton" runat="server" OnClick="OnReset" Text="Reset" />
    </form>
</body>
</html>
