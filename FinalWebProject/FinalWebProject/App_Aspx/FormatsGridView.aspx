<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FormatsGridView.aspx.cs" Inherits="FinalWebProject.App_Aspx.FormatsGridView" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="../CSS/FormatsGridStyle.css"/>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:GridView ID="formatsGrid" runat="server" AutoGenerateColumns="False" OnRowCancelingEdit="OnEditCanceling" OnRowDeleting="OnRowDeleting" OnRowEditing="OnRowEditing" Width="183px" OnRowUpdating="OnRowUpdating" OnRowDataBound="OnRowDataBound">
                <Columns>
                    <asp:BoundField DataField="formatId" HeaderText="Format ID" /><%-- ItemStyle-CssClass="FormatIdCol" HeaderStyle-CssClass="FormatIdCol"  />--%>
                    <asp:BoundField DataField="formatName" HeaderText="Format Name" />
                    <asp:CommandField HeaderText="Edit" ShowEditButton="True" ShowDeleteButton="true" ShowCancelButton="true" />
                </Columns>
            </asp:GridView>
        </div>
    </form>
</body>
</html>
