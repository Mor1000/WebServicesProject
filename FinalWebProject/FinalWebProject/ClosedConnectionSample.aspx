<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ClosedConnectionSample.aspx.cs" Inherits="FinalWebProject.ClosedConnectionSample" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:DropDownList ID="arenaDropDownList" runat="server"></asp:DropDownList>
        <asp:Button ID="showArenaButton" runat="server" Text="Show" OnClick="onShowButton" />
    </div>
    </form>
</body>
</html>
